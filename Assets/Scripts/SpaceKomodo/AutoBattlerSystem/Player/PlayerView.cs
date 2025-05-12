using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.Utilities;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Player
{
    public class PlayerView : MonoBehaviour
    {
        public WorldLayoutGroup QueueBackLayoutGroup;
        public WorldLayoutGroup QueueCenterLayoutGroup;
        public WorldLayoutGroup QueueFrontLayoutGroup;
        
        public WorldLayoutGroup DeployBackLayoutGroup;
        public WorldLayoutGroup DeployCenterLayoutGroup;
        public WorldLayoutGroup DeployFrontLayoutGroup;

        public WorldLayoutGroup GetQueueParent(UnitPosition unitPosition)
        {
            return unitPosition switch
            {
                UnitPosition.Back => QueueBackLayoutGroup,
                UnitPosition.Center => QueueCenterLayoutGroup,
                UnitPosition.Front => QueueFrontLayoutGroup,
                _ => null
            };
        }

        public WorldLayoutGroup GetDeployParent(UnitPosition unitPosition)
        {
            return unitPosition switch
            {
                UnitPosition.Back => DeployBackLayoutGroup,
                UnitPosition.Center => DeployCenterLayoutGroup,
                UnitPosition.Front => DeployFrontLayoutGroup,
                _ => null
            };
        }
    }
}