using System;
using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Deploy;
using SpaceKomodo.AutoBattlerSystem.Characters.Queue;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;

namespace SpaceKomodo.AutoBattlerSystem.Characters
{
    [Serializable]
    public class CharacterModel
    {
        public Dictionary<UnitPosition, QueueModel> Queues = new();
        public Dictionary<UnitPosition, DeployModel> Deploys = new();

        public void Setup(CharacterScriptableObject characterScriptableObject)
        {
            SetupLoadout(UnitPosition.Back, characterScriptableObject.BackLoadouts);
            SetupLoadout(UnitPosition.Center, characterScriptableObject.CenterLoadouts);
            SetupLoadout(UnitPosition.Front, characterScriptableObject.FrontLoadouts);

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
    }
}