using System;
using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;

namespace SpaceKomodo.AutoBattlerSystem.Characters
{
    [Serializable]
    public class CharacterModel : ICloneable
    {
        public List<UnitScriptableObject> BackLoadouts;
        public List<UnitScriptableObject> CenterLoadouts;
        public List<UnitScriptableObject> FrontLoadouts;

        public int Life;
        public int Spirit;
        
        public Dictionary<UnitPosition, PositionModel> Positions = new();

        public CharacterModel(CharacterModel characterModel)
        {
            SetupLoadout(UnitPosition.Back, characterModel.BackLoadouts);
            SetupLoadout(UnitPosition.Center, characterModel.CenterLoadouts);
            SetupLoadout(UnitPosition.Front, characterModel.FrontLoadouts);

            void SetupLoadout(UnitPosition unitPosition, List<UnitScriptableObject> unitScriptableObjects)
            {
                var newPositionModel = new PositionModel();
                Positions.Add(unitPosition, newPositionModel);

                foreach (var unitScriptableObject in unitScriptableObjects)
                {
                    newPositionModel.Units.Add(unitScriptableObject.UnitModel);
                }
            }
        }

        public object Clone()
        {
            return new CharacterModel(this);
        }
    }
}