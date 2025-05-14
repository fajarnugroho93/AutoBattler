using SpaceKomodo.AutoBattlerSystem.Characters;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Core
{
    public class AutoBattlerModel : MonoBehaviour
    {
        public CharacterScriptableObject PlayerCharacterScriptableObject;
        public CharacterScriptableObject EnemyCharacterScriptableObject;
        
        [HideInInspector] public CharacterModel PlayerCharacterModel;
        [HideInInspector] public CharacterModel EnemyCharacterModel;

        public void Setup()
        {
            SetupCharacterModel(PlayerCharacterScriptableObject, out PlayerCharacterModel);
            SetupCharacterModel(EnemyCharacterScriptableObject, out EnemyCharacterModel);
        }

        private static void SetupCharacterModel(
            CharacterScriptableObject characterScriptableObject,
            out CharacterModel characterModel)
        {
            characterModel = (CharacterModel) characterScriptableObject.CharacterModel.Clone();
        }
    }
}