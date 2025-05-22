using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Player;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public static class SimulatorHelpers
    {
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
                case SimulatorOperationType.ApplyToCurrentValue:
                    unitModel.AdjustUnitAttributeApplyToCurrentValue(attributeType, value);
                    break;
            }
        }

        private static void AdjustUnitAttributeApplyToCurrentValue(
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
                case SimulatorOperationType.ApplyToCurrentValue:
                    playerModel.AdjustPlayerAttributeApplyToCurrentValue(attributeType, value);
                    break;
            }
        }

        private static void AdjustPlayerAttributeApplyToCurrentValue(
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