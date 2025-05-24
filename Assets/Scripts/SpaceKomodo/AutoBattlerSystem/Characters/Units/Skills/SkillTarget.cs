using System;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills
{
    [Serializable]
    public class SkillTarget : ICloneable
    {
        public SkillTargetType Type;
        [Range(0, 1f)] public float Weight;

        public SkillTarget(SkillTarget skillTarget)
        {
            Type = skillTarget.Type;
            Weight = skillTarget.Weight;
        }
        
        public object Clone()
        {
            return new SkillTarget(this);
        }
    }
}