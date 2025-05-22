namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public enum SimulatorEventType
    {
        Invalid,
        Lose,

        Life,
        Armour,
        Spirit,
        Aura,
        
        Death,
        
        Cooldown,
        
        StartEntropyCountdown,
        StartEntropy,
    }
}