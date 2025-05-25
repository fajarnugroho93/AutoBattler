using System;
using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using SpaceKomodo.AutoBattlerSystem.Core;
using SpaceKomodo.AutoBattlerSystem.Events;
using SpaceKomodo.AutoBattlerSystem.Player;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;
using VContainer.Unity;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorController : IStartable
    {
        private readonly AutoBattlerModel _autoBattlerModel;
        private readonly SimulatorModel _simulatorModel;
        private readonly SkillTargetPriorityProcessor _skillTargetPriorityProcessor;
        private readonly ISubscriber<SimulateButtonClickedEvent> _simulateButtonClickedSubscriber;
        
        private uint _tickCounter;
        
        private bool _isEntropyActive;
        private float _entropyDamage;
        private float _entropyDamageAdditiveScalar;
        private uint _entropyTickCounter;
        private uint _entropyTickAdditiveScalar;
        private uint _maxEntropyTick;
        private uint _minEntropyTick;
        
        private List<SimulatorEvent> _currentFrameEventList;

        public SimulatorController(
            AutoBattlerModel autoBattlerModel,
            SimulatorModel simulatorModel,
            SkillTargetPriorityProcessor skillTargetPriorityProcessor,
            ISubscriber<SimulateButtonClickedEvent> simulateButtonClickedSubscriber)
        {
            _autoBattlerModel = autoBattlerModel;
            _simulatorModel = simulatorModel;
            _skillTargetPriorityProcessor = skillTargetPriorityProcessor;
            _simulateButtonClickedSubscriber = simulateButtonClickedSubscriber;
        }
        
        public void Start()
        {
            _simulateButtonClickedSubscriber.Subscribe(OnSimulateButtonClicked);

            void OnSimulateButtonClicked(SimulateButtonClickedEvent _)
            {
                SimulateBattle();
            }
        }

        private void SimulateBattle()
        {
            PrepareBattle();
            
            foreach (var frameEventList in _simulatorModel.Events)
            {
                _currentFrameEventList = frameEventList;

                EvaluateUnitSkills(_simulatorModel.PlayerSimulatorMappingModel.UnitModels);
                EvaluateUnitSkills(_simulatorModel.EnemySimulatorMappingModel.UnitModels);
                
                EvaluateEntropy();

                EvaluateUnitDeaths(_simulatorModel.PlayerSimulatorMappingModel.UnitModels);
                EvaluateUnitDeaths(_simulatorModel.EnemySimulatorMappingModel.UnitModels);
                
                if (_simulatorModel.IsEnemyLose())
                {
                    LosePlayer(_autoBattlerModel.EnemyModel);
                    break;
                }
                if (_simulatorModel.IsPlayerLose())
                {
                    LosePlayer(_autoBattlerModel.PlayerModel);
                    break;
                }
                
                _tickCounter += SimulatorConstants.FrameTick;
            }

            ConcludeBattle();
        }

        private void PrepareBattle()
        {
            _tickCounter = 0;
            
            _simulatorModel.SyncMapping(_autoBattlerModel.PlayerModel, _autoBattlerModel.EnemyModel);
            
            _autoBattlerModel.ResetModel();
            _simulatorModel.ResetModel();
            _simulatorModel.ClearEvents();
            
            _isEntropyActive = false;
            _entropyDamage = SimulatorConstants.EntropyBaseDamage;
            _entropyDamageAdditiveScalar = SimulatorConstants.EntropyDamageAdditiveScalar;
            _entropyTickCounter = 0;
            _minEntropyTick = SimulatorConstants.EntropyMinDurationTick;
            _maxEntropyTick = SimulatorConstants.EntropyBaseDurationTick;
            _entropyTickAdditiveScalar = SimulatorConstants.EntropyDurationAdditiveScalar;
        }

        private void ConcludeBattle()
        {
            _autoBattlerModel.ResetModel();
            _simulatorModel.ResetModel();
        }

        private void EvaluateUnitSkills(HashSet<UnitModel> unitModels)
        {
            foreach (var unitModel in unitModels)
            {
                if (unitModel.IsDead)
                {
                    continue;
                }

                foreach (var skillModel in unitModel.Skills)
                {
                    if (!skillModel.IsActiveSkill)
                    {
                        continue;
                    }
                    
                    var attribute = skillModel.Attributes[SkillAttributeType.Cooldown];
                    var newSimulatorEvent = new SimulatorEvent
                    {
                        Type = SimulatorEventType.Cooldown,
                        SourceUnit = unitModel,
                        TargetUnit = unitModel,
                        TargetSkill = skillModel,
                        OperationType = SimulatorOperationType.ApplyToValue,
                        ValueBefore = attribute.Value.Value
                    };
                    var deltaValue = CalculateCooldownFrameValue();
                    skillModel.AdjustSkillAttribute(SkillAttributeType.Cooldown, SimulatorOperationType.ApplyToValue, -deltaValue);
                    newSimulatorEvent.ValueAfter = attribute.Value.Value;
                    AddNewSimulatorEvent(newSimulatorEvent);

                    if (attribute.Value.Value <= 0)
                    {
                        newSimulatorEvent = new SimulatorEvent
                        {
                            Type = SimulatorEventType.Cooldown,
                            SourceUnit = unitModel,
                            TargetUnit = unitModel,
                            TargetSkill = skillModel,
                            OperationType = SimulatorOperationType.ApplyToValue,
                            ValueBefore = attribute.Value.Value
                        };
                        skillModel.SetSkillAttribute(SkillAttributeType.Cooldown, SimulatorOperationType.ApplyToValue, attribute.MaxValue.Value);
                        newSimulatorEvent.ValueAfter = attribute.Value.Value;
                        AddNewSimulatorEvent(newSimulatorEvent);
                        InvokeUnitSkill(unitModel, skillModel);
                    }
                }
            }
        }

        private int CalculateCooldownFrameValue()
        {
            return (int)SimulatorConstants.FrameTick;
        }

        private void InvokeUnitSkill(UnitModel sourceUnitModel, SkillModel skillModel)
        {
            if (sourceUnitModel.IsDead)
            {
                return;
            }
            
            var simulatorMappingModel = _simulatorModel.GetSimulatorMappingModel(sourceUnitModel);
            var targetCount = skillModel.Attributes[SkillAttributeType.Target].Value.Value;
            var sortedTargetModels = _skillTargetPriorityProcessor.CalculateSkillTargetPriority(sourceUnitModel, skillModel, simulatorMappingModel).Take(targetCount);

            foreach (var targetUnitModel in sortedTargetModels)
            {
                ApplySkillEffects(sourceUnitModel, skillModel, targetUnitModel);
            }
        }

        private void ApplySkillEffects(UnitModel sourceUnitModel, SkillModel skillModel, UnitModel targetUnitModel)
        {
            if (skillModel.Attributes.TryGetValue(SkillAttributeType.Damage, out var damageAttribute))
            {
                var damageValue = damageAttribute.Value.Value;
                TakeLifeDamage(sourceUnitModel, targetUnitModel, SimulatorSourceType.Life, damageValue);
            }
        }

        private void EvaluateUnitDeaths(HashSet<UnitModel> unitModels)
        {
            foreach (var unitModel in unitModels)
            {
                if (unitModel.IsDead)
                {
                    continue;
                }

                if (!unitModel.EvaluateDeath())
                {
                    continue;
                }
                
                var newSimulatorEvent = new SimulatorEvent
                {
                    Type = SimulatorEventType.Death,
                    TargetUnit = unitModel,
                };
                AddNewSimulatorEvent(newSimulatorEvent);
                unitModel.IsDead = true;
            }
        }
        
        private void EvaluateEntropy()
        {
            if (!_isEntropyActive)
            {
                switch (_tickCounter)
                {
                    case SimulatorConstants.EntropyCountdownStartTick:
                    {
                        AddNewSimulatorEvent(SimulatorEventType.StartEntropyCountdown);
                        break;
                    }
                    case SimulatorConstants.EntropyStartTick:
                    {
                        _isEntropyActive = true;
                        AddNewSimulatorEvent(SimulatorEventType.StartEntropy);
                        TakeEntropyDamage(_simulatorModel.EnemySimulatorMappingModel.UnitModels);
                        TakeEntropyDamage(_simulatorModel.PlayerSimulatorMappingModel.UnitModels);
                        break;
                    }
                }
            }

            if (_isEntropyActive)
            {
                _entropyTickCounter += SimulatorConstants.FrameTick;

                if (_entropyTickCounter >= _maxEntropyTick)
                {
                    TakeEntropyDamage(_simulatorModel.EnemySimulatorMappingModel.UnitModels);
                    TakeEntropyDamage(_simulatorModel.PlayerSimulatorMappingModel.UnitModels);
                    _entropyTickCounter = 0;

                    if (_maxEntropyTick > _minEntropyTick)
                    {
                        _maxEntropyTick -= _entropyTickAdditiveScalar;   
                    }
                    else
                    {
                        _entropyDamage += _entropyDamageAdditiveScalar;
                    }
                }
            }
        }

        private void TakeEntropyDamage(HashSet<UnitModel> unitModels)
        {
            foreach (var unitModel in unitModels)
            {
                if (unitModel.IsDead)
                {
                    continue;
                }
                
                TakeLifeDamage(unitModel, unitModel, SimulatorSourceType.Entropy, _entropyDamage);   
            }
        }

        private void TakeLifeDamage(
            UnitModel sourceUnitModel, 
            UnitModel targetUnitModel, 
            SimulatorSourceType sourceType,
            float deltaValue)
        {
            var roundedValue = (int)Math.Round(Math.Abs(deltaValue));

            if (roundedValue <= 0)
            {
                return;
            }

            var attributeValue = targetUnitModel.Attributes[UnitAttributeType.Life].Value;
            var newSimulatorEvent = new SimulatorEvent
            {
                Type = SimulatorEventType.Life,
                SourceUnit = sourceUnitModel,
                TargetUnit = targetUnitModel,
                SourceType = sourceType,
                OperationType = SimulatorOperationType.ApplyToValue,
                ValueBefore = attributeValue.Value
            };
            targetUnitModel.AdjustUnitAttribute(UnitAttributeType.Life, SimulatorOperationType.ApplyToValue, -roundedValue);
            newSimulatorEvent.ValueAfter = attributeValue.Value;
            AddNewSimulatorEvent(newSimulatorEvent);
        }

        private void LosePlayer(PlayerModel targetPlayerModel)
        {
            var newSimulatorEvent = new SimulatorEvent
            {
                Type = SimulatorEventType.Lose,
                TargetPlayer = targetPlayerModel,
            };
            AddNewSimulatorEvent(newSimulatorEvent);
        }

        private void AddNewSimulatorEvent(SimulatorEvent newSimulatorEvent)
        {
            _currentFrameEventList.Add(newSimulatorEvent);
        }

        private void AddNewSimulatorEvent(SimulatorEventType simulatorEventType)
        {
            var newSimulatorEvent = new SimulatorEvent
            {
                Type = simulatorEventType
            };
            AddNewSimulatorEvent(newSimulatorEvent);
        }
    }
}