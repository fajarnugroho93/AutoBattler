using System;
using R3;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units
{
    [Serializable]
    public class UnitAttribute
    {
        public ReactiveProperty<int> Value;
        public ReactiveProperty<int> MaxValue;

        public float ValuePercentage => Value.Value / (float) MaxValue.Value;

        public int value;
    }
}