using System.Collections.Generic;
using R3;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorModel
    {
        public readonly List<List<SimulatorEvent>> Events;
        public readonly ReactiveProperty<float> SimulatorProgress;
        public readonly ReactiveProperty<int> CurrentSimulatorFrameIndex;
        public List<SimulatorEvent> CurrentSimulatorFrame => Events[CurrentSimulatorFrameIndex.Value];

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
        
        public void ClearEvents()
        {
            foreach (var FrameEventList in Events)
            {
                FrameEventList.Clear();
            }
        }
    }
}