namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public static class SimulatorConstants
    {
        private const uint MillisecondTick = 1;
        public const uint SecondTick = MillisecondTick * 1000;
        
        public const uint FrameTick = MillisecondTick * 50;
        public const float FrameSecond = FrameTick / (float) SecondTick; 
        
        public const uint BattleDurationTick = 120 * SecondTick;
        public const uint EntropyCountdownStartTick = 20 * SecondTick;
        public const uint EntropyStartTick = 30 * SecondTick;

        public const uint EntropyBaseDamage = 1;
        public const uint EntropyDamageAdditiveScalar = 1;
        public const uint EntropyBaseDurationTick = 2 * SecondTick;
        public const uint EntropyDurationAdditiveScalar = 200 * MillisecondTick;
        public const uint EntropyMinDurationTick = 200 * MillisecondTick;

        public const int MaxColumn = 6;
        public const int MaxRow = 4;
    }
}