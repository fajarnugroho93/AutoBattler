using System;
using System.Collections.Generic;
using R3;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using UnityEngine;

namespace SpaceKomodo.AutoBattlerSystem.Characters.Units
{
    [Serializable]
    public class UnitModel : ICloneable
    {
        public int Tier;
        public int Index;
        public string Name;
        public Sprite Portrait;
        public List<SkillContainerModel> Skills;
        
        public ReactiveProperty<UnitState> State;
        public ReactiveProperty<UnitPosition> Position;
        
        public UnitModel(UnitModel unitModel)
        {
            
        }
        
        public object Clone()
        {
            return new UnitModel(this);
        }
    }
}