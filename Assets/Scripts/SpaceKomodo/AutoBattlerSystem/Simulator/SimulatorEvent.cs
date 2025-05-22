using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using SpaceKomodo.AutoBattlerSystem.Player;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorEvent
    {
        public SimulatorEventType Type;
        
        public PlayerModel TargetPlayer;
        public UnitModel SourceUnit;
        public UnitModel TargetUnit;
        public SkillModel SourceSkill;
        public SkillModel TargetSkill;
        public SimulatorSourceType SourceType;
        public SimulatorOperationType OperationType;
        public float ValueBefore;
        public float ValueAfter;
    }
}