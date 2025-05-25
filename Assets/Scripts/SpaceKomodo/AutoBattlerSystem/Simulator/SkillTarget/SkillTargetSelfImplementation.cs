using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public static class SkillTargetSelfImplementation
    {
        public static void Process(UnitModel unitModel,
            SimulatorMappingModel simulatorMappingModel,
            SkillTargetPriorityModel skillTargetPriorityModel, 
            Dictionary<UnitModel, float> processedDictionary)
        {
            foreach (var keyValuePair in skillTargetPriorityModel.SkillTargetPriorities)
            {
                if (unitModel == keyValuePair.Key)
                {
                    processedDictionary.Add(keyValuePair.Key, 1);
                }
                else
                {
                    processedDictionary.Add(keyValuePair.Key, 0);
                }
            }
        }
    }
}