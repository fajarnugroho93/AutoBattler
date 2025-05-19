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