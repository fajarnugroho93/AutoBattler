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
                _autoBattlerModel.PlayerModel.CharacterModel.Units,
                _autoBattlerView.PlayerWorldView, 
                _playerUnitViews);

            InstantiateUnitViews(
                _autoBattlerModel.EnemyModel.CharacterModel.Units,
                _autoBattlerView.EnemyWorldView, 
                _enemyUnitViews);
        }

        private void InstantiateUnitViews(
            UnitModel[] units,
            PlayerWorldView playerView, 
            IDictionary<UnitModel, UnitView> unitViews)
        {
            for (int i = 0; i < units.Length; i++)
            {
                if (units[i] == null) continue;
                
                var worldLayoutGroup = playerView.GetPositionParent(i);
                if (worldLayoutGroup == null) continue;
                
                var view = _unitViewFactory.Create(units[i], worldLayoutGroup.transform);
                unitViews.Add(units[i], view);
            }
            
            playerView.FrontLayoutGroup.RecalculateLayout();
            playerView.CenterLayoutGroup.RecalculateLayout(); 
            playerView.BackLayoutGroup.RecalculateLayout();
        }
    }
}