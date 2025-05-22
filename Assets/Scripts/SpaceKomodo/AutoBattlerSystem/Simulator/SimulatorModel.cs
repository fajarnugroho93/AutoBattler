using System.Collections.Generic;
using System.Linq;
using R3;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Player;
using SpaceKomodo.AutoBattlerSystem.Player.Squad;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorModel
    {
        public readonly List<List<SimulatorEvent>> Events;
        public readonly ReactiveProperty<float> SimulatorProgress;
        private readonly ReactiveProperty<int> CurrentSimulatorFrameIndex;
        
        public List<SimulatorEvent> CurrentSimulatorFrame => Events[CurrentSimulatorFrameIndex.Value];

        private readonly Dictionary<BattleTargetFlags, UnitModel> BattleTargetFlagsToUnitModelDictionary = new();
        private readonly Dictionary<UnitModel, BattleTargetFlags> UnitModelToBattleTargetFlagsDictionary = new();
        public readonly HashSet<UnitModel> PlayerUnitModels = new();
        public readonly HashSet<UnitModel> EnemyUnitModels = new();

        public bool IsSimulating;
        public bool IsPaused;

        public SimulatorModel()
        {
            Events = new List<List<SimulatorEvent>>();

            for (uint i = 0; i < SimulatorConstants.BattleDurationTick; i += SimulatorConstants.FrameTick)
            {
                Events.Add(new List<SimulatorEvent>());
            }
            
            SimulatorProgress = new ReactiveProperty<float>(0);

            CurrentSimulatorFrameIndex = new ReactiveProperty<int>(0);
            CurrentSimulatorFrameIndex.Subscribe(OnCurrentSimulatorFrameIndexChanged);

            void OnCurrentSimulatorFrameIndexChanged(int value)
            {
                SimulatorProgress.Value = value / (float)Events.Count;
            }
        }

        public void ResetSimulatorPlayer()
        {
            CurrentSimulatorFrameIndex.Value = 0;
            IsPaused = false;
            IsSimulating = true;
        }

        public void TryToMoveNextSimulatorFrameIndex()
        {
            if (CurrentSimulatorFrameIndex.Value + 1 >= Events.Count)
            {
                IsSimulating = false;
            }

            CurrentSimulatorFrameIndex.Value += 1;
        }

        public void ResetModel()
        {
            foreach (var keyValuePair in UnitModelToBattleTargetFlagsDictionary)
            {
                keyValuePair.Key.ResetModel();
            }
        }
        
        public void ClearEvents()
        {
            foreach (var FrameEventList in Events)
            {
                FrameEventList.Clear();
            }
        }

        public bool IsPlayerLose()
        {
            return PlayerUnitModels.All(unitModel => unitModel.IsDead);
        }
        
        public bool IsEnemyLose()
        {
            return EnemyUnitModels.All(unitModel => unitModel.IsDead);
        }

        public void SyncBattleCollections(PlayerModel playerModel, PlayerModel enemyModel)
        {
            BattleTargetFlagsToUnitModelDictionary.Clear();
            UnitModelToBattleTargetFlagsDictionary.Clear();
            
            SyncField(BattleTargetFlags.PlayerFieldFront_0, playerModel.CharacterModel.Positions[UnitPosition.Front].Units.Value);
            SyncField(BattleTargetFlags.PlayerFieldCenter_0, playerModel.CharacterModel.Positions[UnitPosition.Center].Units.Value);
            SyncField(BattleTargetFlags.PlayerFieldBack_0, playerModel.CharacterModel.Positions[UnitPosition.Back].Units.Value);
            SyncField(BattleTargetFlags.OpponentFieldFront_0, enemyModel.CharacterModel.Positions[UnitPosition.Front].Units.Value);
            SyncField(BattleTargetFlags.OpponentFieldCenter_0, enemyModel.CharacterModel.Positions[UnitPosition.Center].Units.Value);
            SyncField(BattleTargetFlags.OpponentFieldBack_0, enemyModel.CharacterModel.Positions[UnitPosition.Back].Units.Value);
            
            void SyncField(BattleTargetFlags startingFlag, IReadOnlyList<UnitModel> unitModels)
            {
                for (var i = 0; i < unitModels.Count; ++i)
                {
                    var unitModel = unitModels[i];
                    var battleTargetFlag = (BattleTargetFlags)(uint)((int)startingFlag << i);
                    BattleTargetFlagsToUnitModelDictionary[battleTargetFlag] = unitModel;
                    UnitModelToBattleTargetFlagsDictionary[unitModel] = 
                        UnitModelToBattleTargetFlagsDictionary.TryGetValue(unitModel, out var existingBattleTargetFlags) ? 
                        (BattleTargetFlags)(uint)((int)existingBattleTargetFlags + (int)battleTargetFlag) : 
                        battleTargetFlag;
                }
            }

            SyncUnitModels(PlayerUnitModels, playerModel);
            SyncUnitModels(EnemyUnitModels, enemyModel);

            void SyncUnitModels(ISet<UnitModel> unitModels, PlayerModel targetPlayerModel)
            {
                unitModels.Clear();
                foreach (var unitModel in targetPlayerModel.CharacterModel.Positions[UnitPosition.Front].Units.Value)
                {
                    unitModels.Add(unitModel);
                }
                foreach (var unitModel in targetPlayerModel.CharacterModel.Positions[UnitPosition.Center].Units.Value)
                {
                    unitModels.Add(unitModel);
                }
                foreach (var unitModel in targetPlayerModel.CharacterModel.Positions[UnitPosition.Back].Units.Value)
                {
                    unitModels.Add(unitModel);
                }
            }
        }
    }
}