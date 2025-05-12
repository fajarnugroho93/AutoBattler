using System;
using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills.Effects;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills
{
    [Serializable]
    public class SkillModel : ICloneable
    {
        public SkillType Type;
        public List<EffectContainer> EffectContainers;
        
        public SkillModel(SkillModel skillModel)
        {
            
        }
        
        public object Clone()
        {
            return new SkillModel(this);
        }
    }
}