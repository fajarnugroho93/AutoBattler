using System;
using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Queue
{
    [Serializable]
    public class QueueModel
    {
        public List<UnitModel> Units = new();
    }
}