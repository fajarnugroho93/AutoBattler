using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public static class SkillTargetFarthestRowImplementation
    {
        public static void Process(
            UnitModel unitModel,
            SimulatorMappingModel simulatorMappingModel,
            SkillTargetPriorityModel skillTargetPriorityModel)
        {
            var sourceFlags = simulatorMappingModel.UnitModelToBattleTargetFlagsDictionary[unitModel];
            var sourceRow = sourceFlags.GetRowIndex();

            foreach (var keyValuePair in skillTargetPriorityModel.SkillTargetPriorities)
            {
                var targetFlags = simulatorMappingModel.UnitModelToBattleTargetFlagsDictionary[keyValuePair.Key];
                var targetRow = targetFlags.GetRowIndex();
                
                var distance = Mathf.Abs(sourceRow - targetRow);
                var priority = distance / SimulatorConstants.MaxRow;
                skillTargetPriorityModel.SkillTargetPriorities[keyValuePair.Key] = keyValuePair.Value * priority;
            }
        }
    }
}