using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Horothenic
{
    public class StepsController : MonoBehaviour
    {
        private const float CAMERA_TRANSITION_TIME = 0.5f;

        [Header("COMPONENTS")]
        [SerializeField] private Camera _camera;
        [SerializeField] private YearStep[] _yearSteps;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int _startStep;
        [SerializeField] private bool _overrideInstant;
        [SerializeField] private StepsContainer _stepsContainer;

        private bool _transitioning = false;
        private int _currentStep = -1;
        private Sequence _cameraSequence;

        private IEnumerator Start()
        {
            yield return null;

            foreach (var yearStep in _yearSteps)
            {
                yearStep.Initialize();
            }

            _currentStep = _startStep;
            for (var i = 0; i <= _currentStep; i++)
            {
                StartCoroutine(ShowStep(i, true));
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GoToNextStep();
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GoToPreviousStep();
            }
        }

        private void GoToNextStep()
        {
            if (_transitioning) return;
            if (_currentStep + 1 >= _stepsContainer.Count) return;

            _currentStep++;
            StartCoroutine(ShowStep(_currentStep, _overrideInstant));
        }

        private void GoToPreviousStep()
        {
            if (_transitioning) return;
            if (_currentStep - 1 < 0) return;

            HideStep(_currentStep);
            _currentStep--;
            StartCoroutine(ShowStep(_currentStep, true));
        }

        private IEnumerator ShowStep(int stepIndex, bool instantly = false)
        {
            _transitioning = true;

            var step = _stepsContainer.GetStep(stepIndex);

            if (step.CameraZoom + step.CameraPosition.magnitude != 0)
            {
                Vector3 cameraPosition = step.CameraPosition;
                cameraPosition.z = _camera.transform.position.z;

                if (instantly)
                {
                    _camera.orthographicSize = step.CameraZoom;
                    _camera.transform.position = cameraPosition;
                }
                else
                {
                    _cameraSequence?.Kill(true);
                    _cameraSequence = DOTween.Sequence();
                    _cameraSequence.Insert(default, DOTween.To(() => _camera.orthographicSize, x => _camera.orthographicSize = x, step.CameraZoom, CAMERA_TRANSITION_TIME));
                    _cameraSequence.Insert(default, _camera.transform.DOMove(cameraPosition, CAMERA_TRANSITION_TIME));
                }

                if (!instantly)
                {
                    yield return new WaitForSeconds(CAMERA_TRANSITION_TIME);
                }
            }

            foreach (var group in step.ActivableGroups)
            {
                foreach (var activable in group.Activables)
                {
                    if (instantly)
                    {
                        activable.JustAppear();
                    }
                    else
                    {
                        activable.Activate();
                    }
                }

                foreach (var activable in group.Deactivables)
                {
                    if (instantly)
                    {
                        activable.JustDisappear();
                    }
                    else
                    {
                        activable.Deactivate();
                    }
                }

                if (!instantly)
                {
                    yield return new WaitForSeconds(Activable.TRANSITION_TIME);
                }
            }

            _transitioning = false;
        }

        private void HideStep(int stepIndex)
        {
            if (_currentStep >= _stepsContainer.Count)
            {
                return;
            }

            _transitioning = true;

            var step = _stepsContainer.GetStep(stepIndex);

            foreach (var group in step.ActivableGroups)
            {
                foreach (var activable in group.Activables)
                {
                    activable.JustDisappear();
                }

                foreach (var activable in group.Deactivables)
                {
                    activable.Deactivate();
                }
            }

            _transitioning = false;
        }
    }
}
