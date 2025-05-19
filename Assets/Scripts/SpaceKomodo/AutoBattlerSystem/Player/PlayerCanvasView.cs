using MoreMountains.Tools;
using R3;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Player
{
    public class PlayerCanvasView : MonoBehaviour
    {
        public MMProgressBar LifeSlider;
        public MMProgressBar SpiritSlider;

        public void Setup(
            PlayerModel playerModel)
        {
            playerModel.Attributes[PlayerAttributeType.Life].Value.Subscribe(OnPlayerLifeValueChanged);

            void OnPlayerLifeValueChanged(int value)
            {
                LifeSlider.BarTarget = playerModel.Attributes[PlayerAttributeType.Life].ValuePercentage;
            }
            
            playerModel.Attributes[PlayerAttributeType.Life].MaxValue.Subscribe(OnPlayerLifeMaxValueChanged);

            void OnPlayerLifeMaxValueChanged(int value)
            {
                LifeSlider.TextValueMultiplier = value;
            }

            playerModel.Attributes[PlayerAttributeType.Spirit].Value.Subscribe(OnPlayerSpiritValueChanged);
            
            void OnPlayerSpiritValueChanged(int value)
            {
                SpiritSlider.BarTarget = playerModel.Attributes[PlayerAttributeType.Spirit].ValuePercentage;
            }

            playerModel.Attributes[PlayerAttributeType.Spirit].MaxValue.Subscribe(OnPlayerSpiritMaxValueChanged);
            
            void OnPlayerSpiritMaxValueChanged(int value)
            {
                SpiritSlider.TextValueMultiplier = value;
            }
        }
    }
}