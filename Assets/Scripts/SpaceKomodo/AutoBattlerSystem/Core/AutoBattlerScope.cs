using MessagePipe;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.Utilities;
using VContainer;
using VContainer.Unity;

namespace SpaceKomodo.AutoBattlerSystem.Core
{
    public class AutoBattlerScope : LifetimeScope
    {
        public UnitView UnitViewPrefab;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(UnitViewPrefab);
            
            builder.RegisterMessageBroker<int>(builder.RegisterMessagePipe());

            builder.Register<IViewFactory<UnitModel, UnitView>, ViewFactory<UnitModel, UnitView>>(Lifetime.Singleton);
            
            builder.RegisterComponentInHierarchy<AutoBattlerModel>();
            builder.RegisterComponentInHierarchy<AutoBattlerView>();
            
            builder.RegisterEntryPoint<AutoBattlerController>();
        }
    }
}