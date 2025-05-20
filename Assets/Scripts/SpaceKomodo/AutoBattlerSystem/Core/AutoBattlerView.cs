using MessagePipe;
using R3;
using SpaceKomodo.AutoBattlerSystem.Events;
using SpaceKomodo.AutoBattlerSystem.Player;
using SpaceKomodo.AutoBattlerSystem.Simulator;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace SpaceKomodo.AutoBattlerSystem.Core
{
    public class AutoBattlerView : MonoBehaviour
    {
        public PlayerWorldView PlayerWorldView;
        public PlayerWorldView EnemyWorldView;
        public PlayerCanvasView PlayerCanvasView;
        public PlayerCanvasView EnemyCanvasView;

        [SerializeField] private Slider _simulationSlider;
        [SerializeField] private Button _simulateButton;
        [SerializeField] private Button _playSimulationButton;
        
        [Inject] private readonly AutoBattlerModel _autoBattlerModel;
        [Inject] private readonly SimulatorModel _simulatorModel;
        [Inject] private readonly IPublisher<SimulateButtonClickedEvent> _simulateButtonClickedPublisher;
        [Inject] private readonly IPublisher<PlaySimulationButtonClickedEvent> _playSimulationButtonClickedPublisher;

        private void Awake()
        {
            _simulateButton.onClick.AddListener(OnSimulateButtonClicked);
            
            void OnSimulateButtonClicked()
            {
                _simulateButtonClickedPublisher.Publish(new SimulateButtonClickedEvent());
            }
            
            _playSimulationButton.onClick.AddListener(OnPlaySimulationButtonClicked);
            
            void OnPlaySimulationButtonClicked()
            {
                _playSimulationButtonClickedPublisher.Publish(new PlaySimulationButtonClickedEvent());
            }
        }

        private void Start()
        {
            _simulatorModel.SimulatorProgress.Subscribe(OnSimulatorProgressChanged);

            void OnSimulatorProgressChanged(float value)
            {
                _simulationSlider.value = value;
            }
            
            PlayerCanvasView.Setup(_autoBattlerModel.PlayerModel);
            EnemyCanvasView.Setup(_autoBattlerModel.EnemyModel);
        }
    }
}