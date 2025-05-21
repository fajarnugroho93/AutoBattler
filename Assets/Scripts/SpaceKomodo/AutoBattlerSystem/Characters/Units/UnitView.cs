using MoreMountains.Tools;
using R3;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using SpaceKomodo.AutoBattlerSystem.Simulator;
using SpaceKomodo.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units
{
    public class UnitView : MonoBehaviour, IInitializable<UnitModel>
    {
        public Image Portrait;
        public MMProgressBar LifeSlider;
        public MMProgressBar SkillSlider;

        private SkillAttribute _cachedCooldownAttribute;
        
        public void Initialize(UnitModel unitModel)
        {
            Portrait.sprite = unitModel.Portrait;
            
            unitModel.Attributes[UnitAttributeType.Life].Value.Subscribe(OnLifeValueChanged);
            unitModel.Attributes[UnitAttributeType.Life].MaxValue.Subscribe(OnLifeMaxValueChanged);

            void OnLifeValueChanged(int value)
            {
                LifeSlider.UpdateBar01(unitModel.Attributes[UnitAttributeType.Life].ValuePercentage);
            }
            
            void OnLifeMaxValueChanged(int value)
            {
                LifeSlider.TextValueMultiplier = value;
                LifeSlider.UpdateText();
            }

            foreach (var skillModel in unitModel.Skills)
            {
                if (skillModel.Attributes.TryGetValue(SkillAttributeType.Cooldown, out _cachedCooldownAttribute))
                {
                    _cachedCooldownAttribute.Value.Subscribe(OnCooldownValueChanged);
                    _cachedCooldownAttribute.MaxValue.Subscribe(OnCooldownMaxValueChanged);
                }
            }
            
            void OnCooldownValueChanged(int value)
            {
                SkillSlider.UpdateBar01(_cachedCooldownAttribute.ValuePercentage);
            }
            
            void OnCooldownMaxValueChanged(int value)
            {
                SkillSlider.TextValueMultiplier = Mathf.RoundToInt(value / (float)SimulatorConstants.SecondTick);
                SkillSlider.UpdateText();
            }

            SkillSlider.transform.parent.gameObject.SetActive(_cachedCooldownAttribute != null);
        }
    }
}