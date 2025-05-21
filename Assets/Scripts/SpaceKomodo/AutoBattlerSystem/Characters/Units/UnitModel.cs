using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using R3;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units
{
    [Serializable]
    public class UnitModel : ICloneable
    {
        public int Tier;
        public int Index;
        public string Name;
        public Sprite Portrait;
        public List<SkillModel> Skills;

        public SerializedDictionary<UnitAttributeType, UnitAttribute> Attributes;

        private UnitModel _cachedReference;
        
        public UnitModel(UnitModel unitModel)
        {
            Tier = unitModel.Tier;
            Index = unitModel.Index;
            Name = unitModel.Name;
            Portrait = unitModel.Portrait;
            
            Attributes = new SerializedDictionary<UnitAttributeType, UnitAttribute>();

            var attributeTypes = Enum.GetValues(typeof(UnitAttributeType));
            foreach (UnitAttributeType attributeType in attributeTypes)
            {
                Attributes.Add(attributeType, new UnitAttribute
                {
                    Value = new ReactiveProperty<int>(),
                    MaxValue = new ReactiveProperty<int>(),
                });
            }

            Skills = new List<SkillModel>();
            foreach (var skillModel in unitModel.Skills)
            {
                var newSkillModel = (SkillModel)skillModel.Clone();
                Skills.Add(newSkillModel);
            }

            _cachedReference = unitModel;
            ResetModel();
        }

        public void ResetModel()
        {
            Attributes[UnitAttributeType.Life].Value.Value = _cachedReference.Attributes[UnitAttributeType.Life].value;
            Attributes[UnitAttributeType.Life].MaxValue.Value = _cachedReference.Attributes[UnitAttributeType.Life].value;

            foreach (var skillModel in Skills)
            {
                skillModel.ResetModel();
            }
        }

        public object Clone()
        {
            return new UnitModel(this);
        }
    }
}