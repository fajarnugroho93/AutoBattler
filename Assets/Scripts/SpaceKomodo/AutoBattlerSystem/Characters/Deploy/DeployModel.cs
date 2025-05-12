using System;
using ObservableCollections;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Deploy
{
    [Serializable]
    public class DeployModel
    {
        public ObservableList<UnitModel> Units = new();
    }
}