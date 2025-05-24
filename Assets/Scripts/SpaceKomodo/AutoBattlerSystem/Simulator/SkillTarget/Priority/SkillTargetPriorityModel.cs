using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Player.Squad;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority
{
    public class SkillTargetPriorityModel
    {
        public Dictionary<UnitModel, float> SkillTargetPriorities;

        public SkillTargetPriorityModel()
        {
            SkillTargetPriorities = new Dictionary<UnitModel, float>();

            ResetModel();
        }

        public void ResetModel()
        {
            SkillTargetPriorities.Clear();
        }

        public void AssignMapping(Dictionary<UnitModel, BattleTargetFlags> unitModelToBattleTargetFlagsDictionary)
        {
            foreach (var keyValuePair in unitModelToBattleTargetFlagsDictionary)
            {
                SkillTargetPriorities.Add(keyValuePair.Key, 1f);
            }
        }
    }
}