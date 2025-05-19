using System;
using ObservableCollections;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units
{
    [Serializable]
    public class PositionModel
    {
        public ObservableList<UnitModel> Units = new();
    }
}