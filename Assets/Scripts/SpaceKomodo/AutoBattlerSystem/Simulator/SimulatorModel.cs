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
            
            var playerUnits = playerModel.CharacterModel.Units;
            var enemyUnits = enemyModel.CharacterModel.Units;
            
            SyncPositionRange(playerUnits, 0, 3, BattleTargetFlags.PlayerFieldTopFront);
            SyncPositionRange(enemyUnits, 0, 3, BattleTargetFlags.OpponentFieldFront_0);
            SyncPositionRange(playerUnits, 4, 7, BattleTargetFlags.PlayerFieldTopCenter);
            SyncPositionRange(enemyUnits, 4, 7, BattleTargetFlags.OpponentFieldCenter_0);
            SyncPositionRange(playerUnits, 8, 11, BattleTargetFlags.PlayerFieldTopBack);
            SyncPositionRange(enemyUnits, 8, 11, BattleTargetFlags.OpponentFieldBack_0);
            
            SyncUnitModelCollections(PlayerUnitModels, playerUnits);
            SyncUnitModelCollections(EnemyUnitModels, enemyUnits);
        }

        private void SyncPositionRange(UnitModel[] units, int startIndex, int endIndex, BattleTargetFlags startingFlag)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (units[i] == null) continue;
                
                var slotOffset = i - startIndex;
                var battleTargetFlag = (BattleTargetFlags)(uint)((int)startingFlag << slotOffset);
                BattleTargetFlagsToUnitModelDictionary[battleTargetFlag] = units[i];
                UnitModelToBattleTargetFlagsDictionary[units[i]] = 
                    UnitModelToBattleTargetFlagsDictionary.TryGetValue(units[i], out var existingFlags) ? 
                    (BattleTargetFlags)(uint)((int)existingFlags + (int)battleTargetFlag) : 
                    battleTargetFlag;
            }
        }

        private void SyncUnitModelCollections(ISet<UnitModel> unitModels, UnitModel[] sourceUnits)
        {
            unitModels.Clear();
            for (int i = 0; i < sourceUnits.Length; i++)
            {
                if (sourceUnits[i] != null)
                {
                    unitModels.Add(sourceUnits[i]);
                }
            }
        }
    }
}