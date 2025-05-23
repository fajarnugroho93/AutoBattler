using MessagePipe;
using SpaceKomodo.AutoBattlerSystem.Events;
using SpaceKomodo.AutoBattlerSystem.Core;
using SpaceKomodo.AutoBattlerSystem.Player;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SpaceKomodo.AutoBattlerSystem.Characters.Units;
using SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills;
using SpaceKomodo.Utilities;
using VContainer.Unity;

namespace SpaceKomodo.AutoBattlerSystem.Simulator
{
    public class SimulatorPlayerController : IStartable
    {
        private readonly SimulatorModel _simulatorModel;
        private readonly AutoBattlerModel _autoBattlerModel;
        private readonly ISubscriber<PlaySimulationButtonClickedEvent> _playSimulationButtonClickedSubscriber;
        private readonly IPublisher<PlaybackSpeedChangedEvent> _playbackSpeedChangedPublisher;
        private readonly ISubscriber<ChangePlaybackSpeedEvent> _changePlaybackSpeedSubscriber;
        private readonly ISubscriber<PausePlaybackEvent> _pausePlaybackSubscriber;
        private readonly ISubscriber<ResumePlaybackEvent> _resumePlaybackSubscriber;

        private Coroutine _playbackCoroutine;
        private float _playbackSpeed;

        private float FramePlaybackDuration => SimulatorConstants.FrameSecond / _playbackSpeed;

        public SimulatorPlayerController(
            SimulatorModel simulatorModel,
            AutoBattlerModel autoBattlerModel,
            ISubscriber<PlaySimulationButtonClickedEvent> playSimulationButtonClickedSubscriber,
            IPublisher<PlaybackSpeedChangedEvent> playbackSpeedChangedPublisher,
            ISubscriber<ChangePlaybackSpeedEvent> changePlaybackSpeedSubscriber,
            ISubscriber<PausePlaybackEvent> pausePlaybackSubscriber,
            ISubscriber<ResumePlaybackEvent> resumePlaybackSubscriber)
        {
            _simulatorModel = simulatorModel;
            _autoBattlerModel = autoBattlerModel;
            _playSimulationButtonClickedSubscriber = playSimulationButtonClickedSubscriber;
            _playbackSpeedChangedPublisher = playbackSpeedChangedPublisher;
            _changePlaybackSpeedSubscriber = changePlaybackSpeedSubscriber;
            _pausePlaybackSubscriber = pausePlaybackSubscriber;
            _resumePlaybackSubscriber = resumePlaybackSubscriber;
        }
        
        public void Start()
        {
            _playSimulationButtonClickedSubscriber.Subscribe(OnPlaySimulationButtonClicked);
            _changePlaybackSpeedSubscriber.Subscribe(OnChangePlaybackSpeed);
            _pausePlaybackSubscriber.Subscribe(OnPausePlayback);
            _resumePlaybackSubscriber.Subscribe(OnResumePlayback);
        }

        private void OnPlaySimulationButtonClicked(PlaySimulationButtonClickedEvent _)
        {
            PlaySimulation();
        }

        private void OnChangePlaybackSpeed(ChangePlaybackSpeedEvent evt)
        {
            _playbackSpeed = evt.Speed;
            _playbackSpeedChangedPublisher.Publish(new PlaybackSpeedChangedEvent { Speed = _playbackSpeed });
        }

        private void OnPausePlayback(PausePlaybackEvent _)
        {
            // _isPaused = true;
        }

        private void OnResumePlayback(ResumePlaybackEvent _)
        {
            // _isPaused = false;
        }

        private void PlaySimulation()
        {
            _simulatorModel.ResetSimulatorPlayer();
            _simulatorModel.ResetModel();
            _playbackSpeed = 2f;
            _autoBattlerModel.ResetModel();
            
            if (_playbackCoroutine != null)
            {
                MonoBehaviourHandler.StopCoroutine(_playbackCoroutine);
            }
            
            _playbackCoroutine = MonoBehaviourHandler.StartCoroutine(PlaybackCoroutine());
        }

        private IEnumerator PlaybackCoroutine()
        {
            while (_simulatorModel.IsSimulating)
            {
                if (_simulatorModel.IsPaused)
                {
                    yield return null;
                    continue;
                }

                ProcessFrameEvents(_simulatorModel.CurrentSimulatorFrame);

                _simulatorModel.TryToMoveNextSimulatorFrameIndex();
                
                yield return new WaitForSeconds(FramePlaybackDuration);
            }
        }

        private void ProcessFrameEvents(List<SimulatorEvent> events)
        {
            foreach (var simulatorEvent in events)
            {
                ProcessSimulatorEvent(simulatorEvent);
            }
        }

        private void ProcessSimulatorEvent(SimulatorEvent simulatorEvent)
        {
            switch (simulatorEvent.Type)
            {
                case SimulatorEventType.Lose:
                    HandleLoseEvent();
                    break;
                case SimulatorEventType.Life:
                case SimulatorEventType.Armour:
                case SimulatorEventType.Spirit:
                case SimulatorEventType.Aura:
                    ApplyUnitAttributeEvent(simulatorEvent);
                    break;
                case SimulatorEventType.Cooldown:
                    ApplySkillAttributeEvent(simulatorEvent);
                    break;
                case SimulatorEventType.Death:
                    HandleUnitDeathEvent(simulatorEvent);
                    break;
                case SimulatorEventType.StartEntropyCountdown:
                    break;
                case SimulatorEventType.StartEntropy:
                    break;
            }
        }

        private void ApplyUnitAttributeEvent(SimulatorEvent simulatorEvent)
        {
            var unitModel = simulatorEvent.TargetUnit;
            var attributeType = GetUnitAttributeType(simulatorEvent.Type);
            
            if (attributeType != null)
            {
                unitModel.Attributes[attributeType.Value].Value.Value = (int)simulatorEvent.ValueAfter;
            }
        }

        private void ApplySkillAttributeEvent(SimulatorEvent simulatorEvent)
        {
            var skillModel = simulatorEvent.TargetSkill;
            var attributeType = GetSkillAttributeType(simulatorEvent.Type);
            
            if (attributeType != null)
            {
                skillModel.Attributes[attributeType.Value].Value.Value = (int)simulatorEvent.ValueAfter;
            }
        }

        private void HandleLoseEvent()
        {
            _simulatorModel.IsSimulating = false;
        }

        private void HandleUnitDeathEvent(SimulatorEvent simulatorEvent)
        {
            var unitModel = simulatorEvent.TargetUnit;
            unitModel.IsDead = true;
        }

        private SkillAttributeType? GetSkillAttributeType(SimulatorEventType eventType)
        {
            switch (eventType)
            {
                case SimulatorEventType.Cooldown:
                    return SkillAttributeType.Cooldown;
                default:
                    return null;
            }
        }

        private UnitAttributeType? GetUnitAttributeType(SimulatorEventType eventType)
        {
            switch (eventType)
            {
                case SimulatorEventType.Life:
                    return UnitAttributeType.Life;
                case SimulatorEventType.Armour:
                    return UnitAttributeType.Armour;
                default:
                    return null;
            }
        }

        private PlayerAttributeType? GetPlayerAttributeType(SimulatorEventType eventType)
        {
            switch (eventType)
            {
                case SimulatorEventType.Life:
                    return PlayerAttributeType.Life;
                case SimulatorEventType.Armour:
                    return PlayerAttributeType.Armour;
                default:
                    return null;
            }
        }
    }
}