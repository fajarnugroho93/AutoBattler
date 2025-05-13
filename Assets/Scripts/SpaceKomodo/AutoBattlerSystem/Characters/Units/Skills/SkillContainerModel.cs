using System;
using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills.Effects;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills
{
    [Serializable]
    public class SkillContainerModel : ICloneable
    {
        public SkillType Type;
        public string Data;
        public List<EffectContainerModel> Effects;
        
        public SkillContainerModel(SkillContainerModel skillContainerModel)
        {
            
        }
        
        public object Clone()
        {
            return new SkillContainerModel(this);
        }
    }
}