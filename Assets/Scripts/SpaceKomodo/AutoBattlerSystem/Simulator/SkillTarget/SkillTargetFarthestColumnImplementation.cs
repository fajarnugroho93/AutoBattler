using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public class SkillTargetFarthestColumnImplementation
    {
        public static void Process(
            UnitModel unitModel,
            SimulatorMappingModel simulatorMappingModel,
            SkillTargetPriorityModel skillTargetPriorityModel)
        {
            foreach (var keyValuePair in skillTargetPriorityModel.SkillTargetPriorities)
            {
                var battleTargetFlag = simulatorMappingModel.UnitModelToBattleTargetFlagsDictionary[keyValuePair.Key];
                
                
            }
        }
    }
}