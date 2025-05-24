using SpaceKomodo.AutoBattlerSystem.Player.Squad;

namespace SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget
{
    public static class SkillTargetHelper
    {
        public static int GetColumnIndex(this BattleTargetFlags flags)
        {
            if ((flags & BattleTargetFlags.SelfFieldBack) != 0)
            {
                return 0;
            }
            if ((flags & BattleTargetFlags.SelfFieldCenter) != 0)
            {
                return 1;
            }
            if ((flags & BattleTargetFlags.SelfFieldFront) != 0)
            {
                return 2;
            }
            if ((flags & BattleTargetFlags.OpponentFieldFront) != 0)
            {
                return 3;
            }
            if ((flags & BattleTargetFlags.OpponentFieldCenter) != 0)
            {
                return 4;
            }
            if ((flags & BattleTargetFlags.OpponentFieldBack) != 0)
            {
                return 5;
            }

            return 0;
        }
        
        public static int GetRowIndex(this BattleTargetFlags flags)
        {
            if ((flags & (BattleTargetFlags.SelfFieldTop | BattleTargetFlags.OpponentFieldTop)) != 0)
            {
                return 0;
            }
            if ((flags & (BattleTargetFlags.SelfFieldUpper | BattleTargetFlags.OpponentFieldUpper)) != 0)
            {
                return 1;
            }
            if ((flags & (BattleTargetFlags.SelfFieldLower | BattleTargetFlags.OpponentFieldLower)) != 0)
            {
                return 2;
            }
            if ((flags & (BattleTargetFlags.SelfFieldBottom | BattleTargetFlags.OpponentFieldBottom)) != 0)
            {
                return 3;
            }
            
            return 0;
        }
    }
}