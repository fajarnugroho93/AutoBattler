using System;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills.Effects.Models
{
    [Serializable]
    public class DamageModel
    {
        public int Damage;
        public float CriticalChance;
        public float CriticalMultiplier;
    }
}