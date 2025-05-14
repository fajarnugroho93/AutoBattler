using System;
using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Deploy;
using SpaceKomodo.AutoBattlerSystem.Characters.Queue;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;

namespace SpaceKomodo.AutoBattlerSystem.Characters
{
    [Serializable]
    public class CharacterModel : ICloneable
    {
        public List<UnitScriptableObject> BackLoadouts;
        public List<UnitScriptableObject> CenterLoadouts;
        public List<UnitScriptableObject> FrontLoadouts;

        public int Health;
        
        public Dictionary<UnitPosition, QueueModel> Queues = new();
        public Dictionary<UnitPosition, DeployModel> Deploys = new();

        public CharacterModel(CharacterModel characterModel)
        {
            SetupLoadout(UnitPosition.Back, characterModel.BackLoadouts);
            SetupLoadout(UnitPosition.Center, characterModel.CenterLoadouts);
            SetupLoadout(UnitPosition.Front, characterModel.FrontLoadouts);

            void SetupLoadout(UnitPosition unitPosition, List<UnitScriptableObject> unitScriptableObjects)
            {
                var newQueueModel = new QueueModel();
                Queues.Add(unitPosition, newQueueModel);

                foreach (var unitScriptableObject in unitScriptableObjects)
                {
                    newQueueModel.Units.Add(unitScriptableObject.UnitModel);
                }
            }
        }

        public object Clone()
        {
            return new CharacterModel(this);
        }
    }
}