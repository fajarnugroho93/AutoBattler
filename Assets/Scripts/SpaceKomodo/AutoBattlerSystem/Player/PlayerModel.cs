using System;
using System.Collections.Generic;
using R3;
using SpaceKomodo.AutoBattlerSystem.Characters;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Player.Squad;

namespace SpaceKomodo.AutoBattlerSystem.Player
{
    public class PlayerModel
    {
        public readonly CharacterModel CharacterModel;
        public readonly Dictionary<PlayerAttributeType, PlayerAttribute> Attributes;

        public SquadModel SquadModel;
        public readonly Dictionary<BattleTargetFlags, UnitModel> BattleTargetFlagsToUnitModelDictionary = new();
        public readonly Dictionary<UnitModel, BattleTargetFlags> UnitModelToBattleTargetFlagsDictionary = new();

        public PlayerModel(
            CharacterModel characterModel)
        {
            Attributes = new Dictionary<PlayerAttributeType, PlayerAttribute>();

            var playerAttributeTypes = Enum.GetValues(typeof(PlayerAttributeType));
            foreach (PlayerAttributeType playerAttributeType in playerAttributeTypes)
            {
                switch (playerAttributeType)
                {
                    case PlayerAttributeType.Life:
                    case PlayerAttributeType.Armour:
                    case PlayerAttributeType.Spirit:
                    case PlayerAttributeType.Aura:
                        Attributes.Add(playerAttributeType, new PlayerAttribute
                        {
                            AttributeType = playerAttributeType,
                            Value = new ReactiveProperty<int>(),
                            MaxValue = new ReactiveProperty<int>(),
                        });
                        break;
                }
            }
            
            CharacterModel = characterModel;
            ResetModel();
        }

        public void ResetModel()
        {
            Attributes[PlayerAttributeType.Life].Value.Value = CharacterModel.Life;
            Attributes[PlayerAttributeType.Life].MaxValue.Value= CharacterModel.Life;
            Attributes[PlayerAttributeType.Spirit].Value.Value = CharacterModel.Spirit;
            Attributes[PlayerAttributeType.Spirit].MaxValue.Value = CharacterModel.Spirit;
        }
    }
}