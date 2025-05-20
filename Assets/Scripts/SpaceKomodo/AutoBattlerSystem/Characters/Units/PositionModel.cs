using System;
using System.Collections.Generic;
using R3;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units
{
    [Serializable]
    public class PositionModel
    {
        public ReactiveProperty<List<UnitModel>> Units = new(new List<UnitModel>());
    }
}