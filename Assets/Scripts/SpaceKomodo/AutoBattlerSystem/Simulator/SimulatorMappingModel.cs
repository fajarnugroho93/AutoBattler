using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Player.Squad;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorMappingModel
    {
        public Dictionary<BattleTargetFlags, UnitModel> BattleTargetFlagsToUnitModelDictionary;
        public Dictionary<UnitModel, BattleTargetFlags> UnitModelToBattleTargetFlagsDictionary;
        public HashSet<UnitModel> UnitModels;

        public SimulatorMappingModel()
        {
            BattleTargetFlagsToUnitModelDictionary = new Dictionary<BattleTargetFlags, UnitModel>();
            UnitModelToBattleTargetFlagsDictionary = new Dictionary<UnitModel, BattleTargetFlags>();
            UnitModels = new HashSet<UnitModel>();
        }

        public bool Contains(UnitModel unitModel)
        {
            return UnitModels.Contains(unitModel);
        }
        
        private void ResetModel()
        {
            BattleTargetFlagsToUnitModelDictionary.Clear();
            UnitModelToBattleTargetFlagsDictionary.Clear();
            UnitModels.Clear();
        }
        
        public void SyncModel(
            UnitModel[] selfUnits,
            UnitModel[] opponentUnits)
        {
            ResetModel();

            SyncPositionRange(selfUnits, 0, 3, BattleTargetFlags.SelfFieldTopFront);
            SyncPositionRange(selfUnits, 4, 7, BattleTargetFlags.SelfFieldTopCenter);
            SyncPositionRange(selfUnits, 8, 11, BattleTargetFlags.SelfFieldTopBack);

            SyncPositionRange(opponentUnits, 0, 3, BattleTargetFlags.OpponentFieldTopFront);
            SyncPositionRange(opponentUnits, 4, 7, BattleTargetFlags.OpponentFieldTopCenter);
            SyncPositionRange(opponentUnits, 8, 11, BattleTargetFlags.OpponentFieldTopBack);

            SyncRowMapping(selfUnits, true);
            SyncRowMapping(opponentUnits, false);

            foreach (var unit in selfUnits)
            {
                if (unit != null)
                {
                    UnitModels.Add(unit);
                }
            }
        }

        private void SyncPositionRange(IReadOnlyList<UnitModel> units, int startIndex, int endIndex, BattleTargetFlags startingFlag)
        {
            for (var i = startIndex; i <= endIndex; i++)
            {
                if (units[i] == null) continue;
                
                var slotOffset = i - startIndex;
                var battleTargetFlag = (BattleTargetFlags)(uint)((int)startingFlag << slotOffset);
                
                BattleTargetFlagsToUnitModelDictionary[battleTargetFlag] = units[i];
                UnitModelToBattleTargetFlagsDictionary[units[i]] = 
                    UnitModelToBattleTargetFlagsDictionary.TryGetValue(units[i], out var existingFlags) ? 
                    (BattleTargetFlags)(uint)((int)existingFlags + (int)battleTargetFlag) : 
                    battleTargetFlag;
            }
        }
        private void SyncRowMapping(IReadOnlyList<UnitModel> units, bool isSelfFlag)
        {
            var topPositions = new[] { 0, 4, 8 };
            var upperPositions = new[] { 1, 5, 9 };
            var lowerPositions = new[] { 2, 6, 10 };
            var bottomPositions = new[] { 3, 7, 11 };

            var topFlag = isSelfFlag ? BattleTargetFlags.SelfFieldTop : BattleTargetFlags.OpponentFieldTop;
            var upperFlag = isSelfFlag ? BattleTargetFlags.SelfFieldUpper : BattleTargetFlags.OpponentFieldUpper;
            var lowerFlag = isSelfFlag ? BattleTargetFlags.SelfFieldLower : BattleTargetFlags.OpponentFieldLower;
            var bottomFlag = isSelfFlag ? BattleTargetFlags.SelfFieldBottom : BattleTargetFlags.OpponentFieldBottom;

            MapRowUnits(units, topPositions, topFlag);
            MapRowUnits(units, upperPositions, upperFlag);
            MapRowUnits(units, lowerPositions, lowerFlag);
            MapRowUnits(units, bottomPositions, bottomFlag);
        }

        private void MapRowUnits(IReadOnlyList<UnitModel> units, IEnumerable<int> positions, BattleTargetFlags rowFlag)
        {
            foreach (var position in positions)
            {
                if (units[position] == null) continue;

                var unit = units[position];
                BattleTargetFlagsToUnitModelDictionary[rowFlag] = unit;

                if (UnitModelToBattleTargetFlagsDictionary.TryGetValue(unit, out var existingFlags))
                {
                    UnitModelToBattleTargetFlagsDictionary[unit] = existingFlags | rowFlag;
                }
                else
                {
                    UnitModelToBattleTargetFlagsDictionary[unit] = rowFlag;
                }
            }
        }
    }
}