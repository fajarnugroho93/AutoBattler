using System;
using System.Collections.Generic;
using MessagePipe;
using SpaceKomodo.AutoBattlerSystem.Core;
using SpaceKomodo.AutoBattlerSystem.Events;
using SpaceKomodo.AutoBattlerSystem.Player;
using VContainer.Unity;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorController : IStartable
    {
        private readonly AutoBattlerModel _autoBattlerModel;
        private readonly SimulatorModel _simulatorModel;
        private readonly ISubscriber<SimulateButtonClickedEvent> _simulateButtonClickedSubscriber;

        private PlayerModel _playerModel;
        private PlayerModel _enemyModel;
        
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
            ISubscriber<SimulateButtonClickedEvent> simulateButtonClickedSubscriber)
        {
            _autoBattlerModel = autoBattlerModel;
            _simulatorModel = simulatorModel;
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
            
            foreach (var FrameEventList in _simulatorModel.Events)
            {
                _currentFrameEventList = FrameEventList;
                
                EvaluateEntropy();
                
                if (_playerModel.IsDead())
                {
                    break;
                }
                if (_enemyModel.IsDead())
                {
                    break;
                }
                
                _tickCounter += SimulatorConstants.FrameTick;
            }
        }

        private void PrepareBattle()
        {
            _autoBattlerModel.ResetModel();
            
            _playerModel = _autoBattlerModel.PlayerModel;
            _enemyModel = _autoBattlerModel.EnemyModel;
            _simulatorModel.ClearEvents();
            _tickCounter = 0;
            
            _isEntropyActive = false;
            _entropyDamage = SimulatorConstants.EntropyBaseDamage;
            _entropyDamageAdditiveScalar = SimulatorConstants.EntropyDamageAdditiveScalar;
            _entropyTickCounter = 0;
            _minEntropyTick = SimulatorConstants.EntropyMinDurationTick;
            _maxEntropyTick = SimulatorConstants.EntropyBaseDurationTick;
            _entropyTickAdditiveScalar = SimulatorConstants.EntropyDurationAdditiveScalar;
        }

        #region Entropy
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
                        AddNewSimulatorEvent(SimulatorEventType.StartEntropy);
                        TakeEntropyDamage(_playerModel);
                        TakeEntropyDamage(_enemyModel);
                        break;
                    }
                }
            }

            if (_isEntropyActive)
            {
                _entropyTickCounter += SimulatorConstants.FrameTick;

                if (_entropyTickCounter >= _maxEntropyTick)
                {
                    TakeEntropyDamage(_playerModel);
                    TakeEntropyDamage(_enemyModel);
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

        private void TakeEntropyDamage(PlayerModel targetPlayerModel)
        {
            TakeLifeDamage(targetPlayerModel, SimulatorSourceType.Entropy, _entropyDamage);
        }
        #endregion

        private void TakeLifeDamage(
            PlayerModel targetPlayerModel, 
            SimulatorSourceType sourceType,
            float deltaValue)
        {
            var roundedValue = (int)Math.Round(Math.Abs(deltaValue));

            if (roundedValue <= 0)
            {
                return;
            }
            
            var newSimulatorEvent = new SimulatorEvent
            {
                Type = SimulatorEventType.Life,
                TargetPlayer = targetPlayerModel,
                SourceType = sourceType,
                OperationType = SimulatorOperationType.ApplyToCurrentValue,
            };
            newSimulatorEvent.ValueBefore = targetPlayerModel.Attributes[PlayerAttributeType.Life].Value.Value;
            targetPlayerModel.AdjustPlayerAttribute(PlayerAttributeType.Life, SimulatorOperationType.ApplyToCurrentValue, -roundedValue);
            newSimulatorEvent.ValueAfter = targetPlayerModel.Attributes[PlayerAttributeType.Life].Value.Value;
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