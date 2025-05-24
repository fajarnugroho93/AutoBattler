using System;

namespace SpaceKomodo.AutoBattlerSystem.Player.Squad
{
    [Flags]
    public enum BattleTargetFlags : ulong
    {
        None = 0UL,
        
        Self = 1UL << 1,
        
        SelfFieldTopFront = 1UL << 2,
        SelfFieldUpperFront = 1UL << 3,
        SelfFieldLowerFront = 1UL << 4,
        SelfFieldBottomFront = 1UL << 5,
        
        SelfFieldTopCenter = 1UL << 6,
        SelfFieldUpperCenter = 1UL << 7,
        SelfFieldLowerCenter = 1UL << 8,
        SelfFieldBottomCenter = 1UL << 9,
        
        SelfFieldTopBack = 1UL << 10,
        SelfFieldUpperBack = 1UL << 11,
        SelfFieldLowerBack = 1UL << 12,
        SelfFieldBottomBack = 1UL << 13,
        
        SelfFieldFront = SelfFieldTopFront | SelfFieldUpperFront | SelfFieldLowerFront | SelfFieldBottomFront,
        SelfFieldCenter = SelfFieldTopCenter | SelfFieldUpperCenter | SelfFieldLowerCenter | SelfFieldBottomCenter,
        SelfFieldBack = SelfFieldTopBack | SelfFieldUpperBack | SelfFieldLowerBack | SelfFieldBottomBack,
        
        SelfFieldTop = SelfFieldTopFront | SelfFieldTopCenter | SelfFieldTopBack,
        SelfFieldUpper = SelfFieldUpperFront | SelfFieldUpperCenter | SelfFieldUpperBack,
        SelfFieldLower = SelfFieldLowerFront | SelfFieldLowerCenter | SelfFieldLowerBack,
        SelfFieldBottom = SelfFieldBottomFront | SelfFieldBottomCenter | SelfFieldBottomBack,
        
        SelfField = SelfFieldFront | SelfFieldCenter | SelfFieldBack,
        
        OpponentFieldTopFront = 1UL << (2 + 20),
        OpponentFieldUpperFront = 1UL << (3 + 20),
        OpponentFieldLowerFront = 1UL << (4 + 20),
        OpponentFieldBottomFront = 1UL << (5 + 20),

        OpponentFieldTopCenter = 1UL << (6 + 20),
        OpponentFieldUpperCenter = 1UL << (7 + 20),
        OpponentFieldLowerCenter = 1UL << (8 + 20),
        OpponentFieldBottomCenter = 1UL << (9 + 20),

        OpponentFieldTopBack = 1UL << (10 + 20),
        OpponentFieldUpperBack = 1UL << (11 + 20),
        OpponentFieldLowerBack = 1UL << (12 + 20),
        OpponentFieldBottomBack = 1UL << (13 + 20),

        OpponentFieldFront = OpponentFieldTopFront | OpponentFieldUpperFront | OpponentFieldLowerFront | OpponentFieldBottomFront,
        OpponentFieldCenter = OpponentFieldTopCenter | OpponentFieldUpperCenter | OpponentFieldLowerCenter | OpponentFieldBottomCenter,
        OpponentFieldBack = OpponentFieldTopBack | OpponentFieldUpperBack | OpponentFieldLowerBack | OpponentFieldBottomBack,

        OpponentFieldTop = OpponentFieldTopFront | OpponentFieldTopCenter | OpponentFieldTopBack,
        OpponentFieldUpper = OpponentFieldUpperFront | OpponentFieldUpperCenter | OpponentFieldUpperBack,
        OpponentFieldLower = OpponentFieldLowerFront | OpponentFieldLowerCenter | OpponentFieldLowerBack,
        OpponentFieldBottom = OpponentFieldBottomFront | OpponentFieldBottomCenter | OpponentFieldBottomBack,

        OpponentField = OpponentFieldFront | OpponentFieldCenter | OpponentFieldBack,
        
        Field = SelfField | OpponentField
    }
}