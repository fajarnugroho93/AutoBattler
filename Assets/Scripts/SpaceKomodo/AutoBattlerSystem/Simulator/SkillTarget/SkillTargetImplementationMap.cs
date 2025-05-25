using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public static class SkillTargetImplementationMap
    {
        private static readonly Dictionary<UnitModel, float> ProcessedDictionary = new();

        public static void Process(
            SkillTargetType skillTargetType,
            UnitModel unitModel,
            SimulatorMappingModel simulatorMappingModel, 
            SkillTargetPriorityModel skillTargetPriorityModel)
        {
            ProcessedDictionary.Clear();
            
            switch (skillTargetType)
            {
                case SkillTargetType.None:
                    SkillTargetNoneImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.Self:
                    SkillTargetSelfImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.Ally:
                    SkillTargetAllyImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.Enemy:
                    SkillTargetEnemyImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.Alive:
                    SkillTargetAliveImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.Dead:
                    SkillTargetDeadImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.ClosestColumn:
                    SkillTargetClosestColumnImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.FarthestColumn:
                    SkillTargetFarthestColumnImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.ClosestRow:
                    SkillTargetClosestRowImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.FarthestRow:
                    SkillTargetFarthestRowImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
                case SkillTargetType.SameRow:
                    SkillTargetSameRowImplementation.Process(unitModel, simulatorMappingModel, skillTargetPriorityModel, ProcessedDictionary);
                    break;
            }
            
            foreach (var keyValuePair in ProcessedDictionary)
            {
                skillTargetPriorityModel.SkillTargetPriorities[keyValuePair.Key] *= keyValuePair.Value;
            }
        }
    }
}