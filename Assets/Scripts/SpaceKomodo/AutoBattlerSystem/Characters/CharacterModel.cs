using System;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Characters
{
    [Serializable]
    public class CharacterModel : ICloneable
    {
        public UnitScriptableObject[] UnitScriptableObjects;
        public int Life;

        [HideInInspector] public UnitModel[] Units;
        public CharacterModel(CharacterModel characterModel)
        {
            Units = new UnitModel[12];
            for (int i = 0; i < 12; i++)
            {
                if (characterModel.UnitScriptableObjects[i] != null)
                {
                    Units[i] = new UnitModel(characterModel.UnitScriptableObjects[i].UnitModel);
                }
            }
            Life = characterModel.Life;
        }

        public UnitModel[] GetFrontUnits() 
        { 
            var result = new UnitModel[4];
            Array.Copy(Units, 0, result, 0, 4);
            return result;
        }
        
        public UnitModel[] GetCenterUnits() 
        { 
            var result = new UnitModel[4];
            Array.Copy(Units, 4, result, 0, 4);
            return result;
        }
        
        public UnitModel[] GetBackUnits() 
        { 
            var result = new UnitModel[4];
            Array.Copy(Units, 8, result, 0, 4);
            return result;
        }

        public object Clone()
        {
            return new CharacterModel(this);
        }
    }
}