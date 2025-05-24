using System.Collections.Generic;
using System.Linq;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority
{
    public class SkillTargetPriorityProcessor
    {
        private readonly SkillTargetPriorityModel _skillTargetPriorityModel;

        public SkillTargetPriorityProcessor(
            SkillTargetPriorityModel skillTargetPriorityModel)
        {
            _skillTargetPriorityModel = skillTargetPriorityModel;
        }

        public IEnumerable<UnitModel> CalculateSkillTargetPriority(
            UnitModel unitModel, 
            SkillModel skillModel, 
            SimulatorMappingModel simulatorMappingModel)
        {
            _skillTargetPriorityModel.ResetModel();
            _skillTargetPriorityModel.AssignMapping(simulatorMappingModel.UnitModelToBattleTargetFlagsDictionary);

            foreach (var skillTargetType in skillModel.TargetTypes)
            {
                SkillTargetImplementationMap.Process(skillTargetType, unitModel, simulatorMappingModel, _skillTargetPriorityModel);
            }

            return _skillTargetPriorityModel.SkillTargetPriorities
                .Where(keyValuePair => keyValuePair.Value > 0f)
                .OrderByDescending(keyValuePair => keyValuePair.Value)
                .Select(keyValuePair => keyValuePair.Key);
        }
    }
}