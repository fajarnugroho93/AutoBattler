using R3;

namespace SpaceKomodo.AutoBattlerSystem.Player
{
    public class PlayerAttribute
    {
        public ReactiveProperty<int> Value;
        public ReactiveProperty<int> MaxValue;

        public float ValuePercentage => Value.Value / (float) MaxValue.Value;
    }
}