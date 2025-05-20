using System;

namespace SpaceKomodo.AutoBattlerSystem.Player.Squad
{
    [Flags]
    public enum BattleTargetFlags : ulong
    {
        None = 0UL,
        
        Player = 1UL << 1,
        
        PlayerFieldFront_0 = 1UL << 2,
        PlayerFieldFront_1 = 1UL << 3,
        PlayerFieldFront_2 = 1UL << 4,
        PlayerFieldFront_3 = 1UL << 5,
        
        PlayerFieldCenter_0 = 1UL << 6,
        PlayerFieldCenter_1 = 1UL << 7,
        PlayerFieldCenter_2 = 1UL << 8,
        PlayerFieldCenter_3 = 1UL << 9,
        
        PlayerFieldBack_0 = 1UL << 10,
        PlayerFieldBack_1 = 1UL << 11,
        PlayerFieldBack_2 = 1UL << 12,
        PlayerFieldBack_3 = 1UL << 13,
        
        PlayerFieldFront = PlayerFieldFront_0 | PlayerFieldFront_1 | PlayerFieldFront_2 | PlayerFieldFront_3,
        PlayerFieldCenter = PlayerFieldCenter_0 | PlayerFieldCenter_1 | PlayerFieldCenter_2 | PlayerFieldCenter_3,
        PlayerFieldBack = PlayerFieldBack_0 | PlayerFieldBack_1 | PlayerFieldBack_2 | PlayerFieldBack_3,
        
        PlayerField = PlayerFieldFront | PlayerFieldCenter | PlayerFieldBack,
        
        Opponent = 1UL << (1 + 20),
        
        OpponentFieldFront_0 = 1UL << (2 + 20),
        OpponentFieldFront_1 = 1UL << (3 + 20),
        OpponentFieldFront_2 = 1UL << (4 + 20),
        OpponentFieldFront_3 = 1UL << (5 + 20),
        
        OpponentFieldCenter_0 = 1UL << (6 + 20),
        OpponentFieldCenter_1 = 1UL << (7 + 20),
        OpponentFieldCenter_2 = 1UL << (8 + 20),
        OpponentFieldCenter_3 = 1UL << (9 + 20),
        
        OpponentFieldBack_0 = 1UL << (10 + 20),
        OpponentFieldBack_1 = 1UL << (11 + 20),
        OpponentFieldBack_2 = 1UL << (12 + 20),
        OpponentFieldBack_3 = 1UL << (13 + 20),
        
        OpponentFieldFront = OpponentFieldFront_0 | OpponentFieldFront_1 | OpponentFieldFront_2 | OpponentFieldFront_3,
        OpponentFieldCenter = OpponentFieldCenter_0 | OpponentFieldCenter_1 | OpponentFieldCenter_2 | OpponentFieldCenter_3,
        OpponentFieldBack = OpponentFieldBack_0 | OpponentFieldBack_1 | OpponentFieldBack_2 | OpponentFieldBack_3,
        
        OpponentField = OpponentFieldFront | OpponentFieldCenter | OpponentFieldBack,
        
        Field = PlayerField | OpponentField
    }
}