using MoreMountains.Tools;
using R3;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Player
{
    public class PlayerCanvasView : MonoBehaviour
    {
        public MMProgressBar LifeSlider;

        public void Setup(
            PlayerModel playerModel)
        {
            playerModel.Attributes[PlayerAttributeType.Life].Value.Subscribe(OnLifeValueChanged);

            void OnLifeValueChanged(int value)
            {
                LifeSlider.UpdateBar01(playerModel.Attributes[PlayerAttributeType.Life].ValuePercentage);
            }
            
            playerModel.Attributes[PlayerAttributeType.Life].MaxValue.Subscribe(OnLifeMaxValueChanged);

            void OnLifeMaxValueChanged(int value)
            {
                LifeSlider.TextValueMultiplier = value;
                LifeSlider.UpdateText();
            }
        }
    }
}