using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "TurnBasedSystem/Unit", order = -8000)]
    public class UnitScriptableObject : ScriptableObject
    {
        public UnitModel UnitModel;
    }
}