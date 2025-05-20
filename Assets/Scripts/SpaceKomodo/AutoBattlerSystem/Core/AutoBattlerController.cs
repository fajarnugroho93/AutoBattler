using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Player;
using SpaceKomodo.Utilities;
using VContainer.Unity;

namespace SpaceKomodo.AutoBattlerSystem.Core
{
    public class AutoBattlerController : IStartable
    {
        private readonly AutoBattlerModel _autoBattlerModel;
        private readonly AutoBattlerView _autoBattlerView;
        private readonly IViewFactory<UnitModel, UnitView> _unitViewFactory;

        private readonly Dictionary<UnitModel, UnitView> _playerUnitViews = new();
        private readonly Dictionary<UnitModel, UnitView> _enemyUnitViews = new();
        
        public AutoBattlerController(
            AutoBattlerModel autoBattlerModel, 
            AutoBattlerView autoBattlerView,
            IViewFactory<UnitModel, UnitView> unitViewFactory)
        {
            _autoBattlerModel = autoBattlerModel;
            _autoBattlerView = autoBattlerView;
            _unitViewFactory = unitViewFactory;
        }

        public void Start()
        {
            _autoBattlerModel.Setup();

            InstantiateUnitViews(
                _autoBattlerModel.PlayerModel.CharacterModel.Positions, 
                _autoBattlerView.PlayerWorldView, 
                _playerUnitViews);

            InstantiateUnitViews(
                _autoBattlerModel.EnemyModel.CharacterModel.Positions, 
                _autoBattlerView.EnemyWorldView, 
                _enemyUnitViews);

            void InstantiateUnitViews(
                Dictionary<UnitPosition, PositionModel> positions, 
                PlayerWorldView playerView, 
                IDictionary<UnitModel, UnitView> unitViews)
            {
                foreach (var keyValuePair in positions)
                {
                    var worldLayoutGroup = playerView.GetPositionParent(keyValuePair.Key);
                    
                    foreach (var unitModel in keyValuePair.Value.Units.Value)
                    {
                        var view = _unitViewFactory.Create(unitModel, worldLayoutGroup.transform);
                        unitViews.Add(unitModel, view);
                    }
                    
                    worldLayoutGroup.RecalculateLayout();
                }
            }
        }
    }
}