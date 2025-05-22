using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using SpaceKomodo.AutoBattlerSystem.Player;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public static class SimulatorHelpers
    {
        public static void SetSkillAttribute(
            this SkillModel skillModel, 
            SkillAttributeType attributeType,
            SimulatorOperationType operationType,
            int value)
        {
            switch (operationType)
            {
                case SimulatorOperationType.None:
                    break;
                case SimulatorOperationType.ApplyToValue:
                    skillModel.SetSkillAttributeApplyToValue(attributeType, value);
                    break;
            }
        }

        private static void SetSkillAttributeApplyToValue(
            this SkillModel skillModel, 
            SkillAttributeType attributeType,
            int targetValue)
        {
            var attribute = skillModel.Attributes[attributeType];
            targetValue = Mathf.Clamp(targetValue, int.MinValue, attribute.MaxValue.Value);
            attribute.Value.Value = targetValue;
        }
        
        public static void AdjustSkillAttribute(
            this SkillModel skillModel, 
            SkillAttributeType attributeType,
            SimulatorOperationType operationType,
            int value)
        {
            switch (operationType)
            {
                case SimulatorOperationType.None:
                    break;
                case SimulatorOperationType.ApplyToValue:
                    skillModel.AdjustSkillAttributeApplyToValue(attributeType, value);
                    break;
            }
        }

        private static void AdjustSkillAttributeApplyToValue(
            this SkillModel skillModel, 
            SkillAttributeType attributeType,
            int deltaValue)
        {
            var attribute = skillModel.Attributes[attributeType];
            var valueBefore = attribute.Value.Value;
            var valueAfter = deltaValue + valueBefore;

            valueAfter = Mathf.Clamp(valueAfter, int.MinValue, attribute.MaxValue.Value);
            attribute.Value.Value = valueAfter;
        }
        
        public static void AdjustUnitAttribute(
            this UnitModel unitModel, 
            UnitAttributeType attributeType,
            SimulatorOperationType operationType,
            int value)
        {
            switch (operationType)
            {
                case SimulatorOperationType.None:
                    break;
                case SimulatorOperationType.ApplyToValue:
                    unitModel.AdjustUnitAttributeApplyToValue(attributeType, value);
                    break;
            }
        }

        private static void AdjustUnitAttributeApplyToValue(
            this UnitModel unitModel, 
            UnitAttributeType attributeType,
            int deltaValue)
        {
            var attribute = unitModel.Attributes[attributeType];
            var valueBefore = attribute.Value.Value;
            var valueAfter = deltaValue + valueBefore;

            valueAfter = Mathf.Clamp(valueAfter, int.MinValue, attribute.MaxValue.Value);
            attribute.Value.Value = valueAfter;
        }
        
        public static bool IsDead(
            this PlayerModel playerModel)
        {
            return playerModel.Attributes[PlayerAttributeType.Life].Value.Value <= 0;
        }
        
        public static void AdjustPlayerAttribute(
            this PlayerModel playerModel, 
            PlayerAttributeType attributeType,
            SimulatorOperationType operationType,
            int value)
        {
            switch (operationType)
            {
                case SimulatorOperationType.None:
                    break;
                case SimulatorOperationType.ApplyToValue:
                    playerModel.AdjustPlayerAttributeApplyToValue(attributeType, value);
                    break;
            }
        }

        private static void AdjustPlayerAttributeApplyToValue(
            this PlayerModel playerModel, 
            PlayerAttributeType attributeType,
            int deltaValue)
        {
            var playerAttribute = playerModel.Attributes[attributeType];
            var valueBefore = playerAttribute.Value.Value;
            var valueAfter = deltaValue + valueBefore;

            valueAfter = Mathf.Clamp(valueAfter, int.MinValue, playerAttribute.MaxValue.Value);
            playerAttribute.Value.Value = valueAfter;
        }
    }
}