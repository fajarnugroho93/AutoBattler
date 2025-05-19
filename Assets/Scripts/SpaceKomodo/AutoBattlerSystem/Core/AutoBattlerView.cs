using MessagePipe;
using SpaceKomodo.AutoBattlerSystem.Events;
using SpaceKomodo.AutoBattlerSystem.Player;
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

        [SerializeField] private Button _simulateButton;
        [SerializeField] private Button _playSimulationButton;
        
        [Inject] private readonly AutoBattlerModel _autoBattlerModel;
        [Inject] private readonly IPublisher<SimulateButtonClickedEvent> _simulateButtonClickedPublisher;
        [Inject] private readonly IPublisher<PlaySimulationButtonClickedEvent> _playSimulationButtonClickedPublisher;

        private void Awake()
        {
            _simulateButton.onClick.AddListener(OnSimulateButtonClicked);
            _playSimulationButton.onClick.AddListener(OnPlaySimulationButtonClicked);
            
            void OnSimulateButtonClicked()
            {
                _simulateButtonClickedPublisher.Publish(new SimulateButtonClickedEvent());
            }
            
            void OnPlaySimulationButtonClicked()
            {
                _playSimulationButtonClickedPublisher.Publish(new PlaySimulationButtonClickedEvent());
            }
        }

        private void Start()
        {
            PlayerCanvasView.Setup(_autoBattlerModel.PlayerModel);
            EnemyCanvasView.Setup(_autoBattlerModel.EnemyModel);
        }
    }
}