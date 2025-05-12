using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Queue;
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
                _autoBattlerModel.PlayerCharacterModel.Queues, 
                _autoBattlerView.PlayerView, 
                _playerUnitViews);

            InstantiateUnitViews(
                _autoBattlerModel.EnemyCharacterModel.Queues, 
                _autoBattlerView.EnemyView, 
                _enemyUnitViews);

            void InstantiateUnitViews(
                Dictionary<UnitPosition, QueueModel> queues, 
                PlayerView playerView, 
                IDictionary<UnitModel, UnitView> unitViews)
            {
                foreach (var keyValuePair in queues)
                {
                    var worldLayoutGroup = playerView.GetQueueParent(keyValuePair.Key);
                    
                    foreach (var unitModel in keyValuePair.Value.Units)
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