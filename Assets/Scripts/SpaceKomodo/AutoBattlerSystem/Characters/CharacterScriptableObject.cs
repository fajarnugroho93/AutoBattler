using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Characters
{
    [CreateAssetMenu(fileName = "New Character", menuName = "TurnBasedSystem/Character", order = -10000)]
    public class CharacterScriptableObject : ScriptableObject
    {
        public CharacterModel CharacterModel;
    }
}