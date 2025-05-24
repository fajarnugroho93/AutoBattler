using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public static class SkillTargetDeadImplementation
    {
        public static void Process(
            UnitModel unitModel,
            SimulatorMappingModel simulatorMappingModel,
            SkillTargetPriorityModel skillTargetPriorityModel)
        {
            foreach (var keyValuePair in skillTargetPriorityModel.SkillTargetPriorities)
            {
                if (keyValuePair.Key.IsDead)
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