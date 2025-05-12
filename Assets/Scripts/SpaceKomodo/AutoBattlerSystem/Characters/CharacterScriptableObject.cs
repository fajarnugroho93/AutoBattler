using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Characters
{
    [CreateAssetMenu(fileName = "New Character", menuName = "TurnBasedSystem/Character", order = -10000)]
    public class CharacterScriptableObject : ScriptableObject
    {
        public List<UnitScriptableObject> BackLoadouts;
        public List<UnitScriptableObject> CenterLoadouts;
        public List<UnitScriptableObject> FrontLoadouts;
    }
}