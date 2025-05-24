using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public static class SkillTargetImplementationMap
    {
        public static void Process(
            SkillTargetType skillTargetType,
            UnitModel unitModel,
            SimulatorMappingModel simulatorMappingModel, 
            SkillTargetPriorityModel skillTargetPriorityModel)
        {
            switch (skillTargetType)
            {
                case SkillTargetType.None:
                    SkillTargetNoneImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.Self:
                    SkillTargetSelfImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.Ally:
                    SkillTargetAllyImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.Enemy:
                    SkillTargetEnemyImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.Alive:
                    SkillTargetAliveImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.Dead:
                    SkillTargetDeadImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.ClosestColumn:
                    SkillTargetClosestColumnImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.FarthestColumn:
                    SkillTargetFarthestColumnImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.ClosestRow:
                    SkillTargetClosestRowImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.FarthestRow:
                    SkillTargetFarthestRowImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
                case SkillTargetType.SameRow:
                    SkillTargetSameRowImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel);
                    break;
            }
        }
    }
}