using SpaceKomodo.AutoBattlerSystem.Characters;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Core
{
    public class AutoBattlerModel : MonoBehaviour
    {
        public CharacterScriptableObject PlayerCharacterScriptableObject;
        public CharacterScriptableObject EnemyCharacterScriptableObject;
        
        [HideInInspector] public CharacterModel PlayerCharacterModel = new();
        [HideInInspector] public CharacterModel EnemyCharacterModel = new();

        public void Setup()
        {
            SetupCharacterModel(PlayerCharacterScriptableObject, PlayerCharacterModel);
            SetupCharacterModel(EnemyCharacterScriptableObject, EnemyCharacterModel);
        }

        private void SetupCharacterModel(
            CharacterScriptableObject characterScriptableObject,
            CharacterModel characterModel)
        {
            characterModel.Setup(characterScriptableObject);
        }
    }
}