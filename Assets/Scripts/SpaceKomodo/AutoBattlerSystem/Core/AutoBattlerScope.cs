using MessagePipe;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Simulator;
using SpaceKomodo.AutoBattlerSystem.Simulator.SkillTarget.Priority;
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
            
            builder.Register<SimulatorModel>(Lifetime.Singleton);
            builder.Register<SkillTargetPriorityModel>(Lifetime.Singleton);
            builder.Register<SkillTargetPriorityProcessor>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<SimulatorController>();
            builder.RegisterEntryPoint<SimulatorPlayerController>();
            builder.RegisterEntryPoint<AutoBattlerController>();
        }
    }
}