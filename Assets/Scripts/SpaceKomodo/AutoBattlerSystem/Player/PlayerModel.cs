using System;
using System.Collections.Generic;
using R3;
using SpaceKomodo.AutoBattlerSystem.Characters;

namespace SpaceKomodo.AutoBattlerSystem.Player
{
    public class PlayerModel
    {
        public readonly CharacterModel CharacterModel;
        public readonly Dictionary<PlayerAttributeType, PlayerAttribute> Attributes;

        public PlayerModel(
            CharacterModel characterModel)
        {
            Attributes = new Dictionary<PlayerAttributeType, PlayerAttribute>();

            var attributeTypes = Enum.GetValues(typeof(PlayerAttributeType));
            foreach (PlayerAttributeType attributeType in attributeTypes)
            {
                Attributes.Add(attributeType, new PlayerAttribute
                {
                    Value = new ReactiveProperty<int>(),
                    MaxValue = new ReactiveProperty<int>(),
                });
            }
            
            CharacterModel = characterModel;
            ResetModel();
        }

        public void ResetModel()
        {
            Attributes[PlayerAttributeType.Life].Value.Value = CharacterModel.Life;
            Attributes[PlayerAttributeType.Life].MaxValue.Value = CharacterModel.Life;
        }
    }
}