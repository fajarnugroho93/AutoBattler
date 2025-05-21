using System;
using System.Collections.Generic;
using R3;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;

namespace SpaceKomodo.AutoBattlerSystem.Characters
{
    [Serializable]
    public class PositionModel
    {
        public ReactiveProperty<List<UnitModel>> Units = new(new List<UnitModel>());
    }
}