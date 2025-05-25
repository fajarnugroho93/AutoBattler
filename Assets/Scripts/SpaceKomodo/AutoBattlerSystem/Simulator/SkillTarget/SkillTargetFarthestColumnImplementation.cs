using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public static class SkillTargetFarthestColumnImplementation
    {
        public static void Process(UnitModel unitModel,
            SimulatorMappingModel simulatorMappingModel,
            SkillTargetPriorityModel skillTargetPriorityModel, 
            Dictionary<UnitModel, float> processedDictionary)
        {
            var sourceFlags = simulatorMappingModel.UnitModelToBattleTargetFlagsDictionary[unitModel];
            var sourceColumn = sourceFlags.GetColumnIndex();

            foreach (var keyValuePair in skillTargetPriorityModel.SkillTargetPriorities)
            {
                var targetFlags = simulatorMappingModel.UnitModelToBattleTargetFlagsDictionary[keyValuePair.Key];
                var targetColumn = targetFlags.GetColumnIndex();
                
                var distance = Mathf.Abs(sourceColumn - targetColumn);
                var priority = distance / SimulatorConstants.MaxColumn;
                processedDictionary.Add(keyValuePair.Key, priority);
            }
        }
    }
}