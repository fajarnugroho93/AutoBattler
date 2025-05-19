using System.Collections.Generic;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorModel
    {
        public readonly List<List<SimulatorEvent>> Events;

        public SimulatorModel()
        {
            Events = new List<List<SimulatorEvent>>();

            for (uint i = 0; i < SimulatorConstants.BattleDurationTick; i += SimulatorConstants.FrameTick)
            {
                Events.Add(new List<SimulatorEvent>());
            }
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