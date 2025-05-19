using MessagePipe;
using SpaceKomodo.AutoBattlerSystem.Events;
using VContainer.Unity;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorPlayerController : IStartable
    {
        private readonly SimulatorModel _simulatorModel;
        private readonly ISubscriber<PlaySimulationButtonClickedEvent> _playSimulationButtonClickedSubscriber;

        public SimulatorPlayerController(
            SimulatorModel simulatorModel,
            ISubscriber<PlaySimulationButtonClickedEvent> playSimulationButtonClickedSubscriber)
        {
            _simulatorModel = simulatorModel;
            _playSimulationButtonClickedSubscriber = playSimulationButtonClickedSubscriber;
        }
        
        public void Start()
        {
            _playSimulationButtonClickedSubscriber.Subscribe(OnPlaySimulationButtonClicked);

            void OnPlaySimulationButtonClicked(PlaySimulationButtonClickedEvent _)
            {
                PlaySimulation();
            }
        }

        private void PlaySimulation()
        {
            
        }
    }
}