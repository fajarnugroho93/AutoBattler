using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.Utilities;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Player
{
    public class PlayerWorldView : MonoBehaviour
    {
        public WorldLayoutGroup BackLayoutGroup;
        public WorldLayoutGroup CenterLayoutGroup;
        public WorldLayoutGroup FrontLayoutGroup;

        public WorldLayoutGroup GetPositionParent(int index) 
        {
            if (index >= 0 && index <= 3) return FrontLayoutGroup;
            if (index >= 4 && index <= 7) return CenterLayoutGroup;
            if (index >= 8 && index <= 11) return BackLayoutGroup;
            return null;
        }

        public WorldLayoutGroup GetPositionParent(UnitPosition unitPosition)
        {
            return unitPosition switch
            {
                UnitPosition.Back => BackLayoutGroup,
                UnitPosition.Center => CenterLayoutGroup,
                UnitPosition.Front => FrontLayoutGroup,
                _ => null
            };
        }
    }
}