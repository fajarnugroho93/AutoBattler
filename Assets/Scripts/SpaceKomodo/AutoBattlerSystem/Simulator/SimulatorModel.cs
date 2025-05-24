using System.Collections.Generic;
using System.Linq;
using R3;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Player;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorModel
    {
        public readonly List<List<SimulatorEvent>> Events;
        public readonly ReactiveProperty<float> SimulatorProgress;
        private readonly ReactiveProperty<int> CurrentSimulatorFrameIndex;
        
        public List<SimulatorEvent> CurrentSimulatorFrame => Events[CurrentSimulatorFrameIndex.Value];

        public readonly SimulatorMappingModel PlayerSimulatorMappingModel;
        public readonly SimulatorMappingModel EnemySimulatorMappingModel;

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

            PlayerSimulatorMappingModel = new SimulatorMappingModel();
            EnemySimulatorMappingModel = new SimulatorMappingModel();

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
            foreach (var unitModel in PlayerSimulatorMappingModel.UnitModels)
            {
                unitModel.ResetModel();
            }

            foreach (var unitModel in EnemySimulatorMappingModel.UnitModels)
            {
                unitModel.ResetModel();
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
            return PlayerSimulatorMappingModel.UnitModels.All(unitModel => unitModel.IsDead);
        }
        
        public bool IsEnemyLose()
        {
            return EnemySimulatorMappingModel.UnitModels.All(unitModel => unitModel.IsDead);
        }

        public void SyncMapping(PlayerModel playerModel, PlayerModel enemyModel)
        {
            var playerUnits = playerModel.CharacterModel.Units;
            var enemyUnits = enemyModel.CharacterModel.Units;
            
            PlayerSimulatorMappingModel.SyncModel(playerUnits, enemyUnits);
            EnemySimulatorMappingModel.SyncModel(enemyUnits, playerUnits);
        }

        public SimulatorMappingModel GetSimulatorMappingModel(UnitModel unitModel)
        {
            if (PlayerSimulatorMappingModel.Contains(unitModel))
            {
                return PlayerSimulatorMappingModel;
            }

            return EnemySimulatorMappingModel;
        }
    }
}