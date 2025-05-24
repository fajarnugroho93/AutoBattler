using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Player.Squad;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public static class SkillTargetEnemyImplementation
    {
        public static void Process(
            UnitModel unitModel,
            SimulatorMappingModel simulatorMappingModel,
            SkillTargetPriorityModel skillTargetPriorityModel)
        {
            foreach (var keyValuePair in skillTargetPriorityModel.SkillTargetPriorities)
            {
                var battleTargetFlag = simulatorMappingModel.UnitModelToBattleTargetFlagsDictionary[keyValuePair.Key];
                
                if ((battleTargetFlag & BattleTargetFlags.OpponentField) != 0)
                {
                    skillTargetPriorityModel.SkillTargetPriorities[keyValuePair.Key] = keyValuePair.Value * 1;
                }
                else
                {
                    skillTargetPriorityModel.SkillTargetPriorities[keyValuePair.Key] = keyValuePair.Value * 0;
                }
            }
        }
    }
}