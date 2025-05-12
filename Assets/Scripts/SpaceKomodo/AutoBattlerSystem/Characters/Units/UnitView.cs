using SpaceKomodo.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units
{
    public class UnitView : MonoBehaviour, IInitializable<UnitModel>
    {
        public Image Portrait;
        public Slider ProgressSlider;
        
        public void Initialize(UnitModel unitModel)
        {
            Portrait.sprite = unitModel.Portrait;
        }
    }
}