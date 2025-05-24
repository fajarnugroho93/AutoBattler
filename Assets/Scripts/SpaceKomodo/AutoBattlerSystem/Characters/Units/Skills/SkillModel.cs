using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using R3;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills
{
    [Serializable]
    public class SkillModel : ICloneable
    {
        public SkillType Type;
        public List<SkillTarget> Targets;
        public SerializedDictionary<SkillAttributeType, SkillAttribute> Attributes;

        private SkillModel _cachedReference;
        
        public SkillModel(SkillModel skillModel)
        {
            Type = skillModel.Type;
            Targets = skillModel.Targets.Select(target => (SkillTarget)target.Clone()).ToList();
            
            Attributes = new SerializedDictionary<SkillAttributeType, SkillAttribute>();

            var attributeTypes = Enum.GetValues(typeof(SkillAttributeType));
            foreach (SkillAttributeType attributeType in attributeTypes)
            {
                if (!skillModel.Attributes.ContainsKey(attributeType))
                {
                    continue;
                }
                
                Attributes.Add(attributeType, new SkillAttribute
                {
                    Value = new ReactiveProperty<int>(),
                    MaxValue = new ReactiveProperty<int>(),
                });
            }
            
            _cachedReference = skillModel;
            ResetModel();
        }

        public void ResetModel()
        {
            foreach (var keyValuePair in Attributes)
            {
                switch (keyValuePair.Key)
                {
                    case SkillAttributeType.Cooldown:
                    case SkillAttributeType.Target:
                        Attributes[keyValuePair.Key].Value.Value = _cachedReference.Attributes[keyValuePair.Key].value;
                        Attributes[keyValuePair.Key].MaxValue.Value = _cachedReference.Attributes[keyValuePair.Key].value;           
                        break;
                    
                    case SkillAttributeType.Damage:
                    case SkillAttributeType.Heal:
                        Attributes[keyValuePair.Key].Value.Value = _cachedReference.Attributes[keyValuePair.Key].value;
                        break;
                }
            }
        }
        
        public object Clone()
        {
            return new SkillModel(this);
        }

        public bool IsActiveSkill => Attributes.ContainsKey(SkillAttributeType.Cooldown);
    }
}