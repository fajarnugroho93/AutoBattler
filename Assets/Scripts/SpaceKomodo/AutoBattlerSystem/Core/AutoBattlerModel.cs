using SpaceKomodo.AutoBattlerSystem.Characters;
using SpaceKomodo.AutoBattlerSystem.Player;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Core
{
    public class AutoBattlerModel : MonoBehaviour
    {
        public CharacterScriptableObject PlayerCharacterScriptableObject;
        public CharacterScriptableObject EnemyCharacterScriptableObject;
        
        [HideInInspector] public PlayerModel PlayerModel;
        [HideInInspector] public PlayerModel EnemyModel;

        public void Setup()
        {
            SetupCharacterModel(PlayerCharacterScriptableObject, out PlayerModel);
            SetupCharacterModel(EnemyCharacterScriptableObject, out EnemyModel);
            
            ResetModel();
        }

        public void ResetModel()
        {
            PlayerModel.ResetModel();
            EnemyModel.ResetModel();
        }

        private static void SetupCharacterModel(
            CharacterScriptableObject characterScriptableObject,
            out PlayerModel playerModel)
        {
            var characterModel = (CharacterModel) characterScriptableObject.CharacterModel.Clone();
            playerModel = new PlayerModel(characterModel);
        }
    }
}