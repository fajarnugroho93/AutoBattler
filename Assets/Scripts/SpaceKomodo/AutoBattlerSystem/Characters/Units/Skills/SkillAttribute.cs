using System;
using R3;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills
{
    [Serializable]
    public class SkillAttribute
    {
        public ReactiveProperty<int> Value;
        public ReactiveProperty<int> MaxValue;

        public float ValuePercentage => Value.Value / (float) MaxValue.Value;

        public int value;
    }
}