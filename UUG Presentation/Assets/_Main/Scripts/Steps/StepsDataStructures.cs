using System;
using UnityEngine;

namespace Horothenic
{
    [Serializable]
    public class StepsContainer
    {
        [SerializeField] private Step[] _steps;

        public int Count => _steps.Length;

        public Step GetStep(int index)
        {
            if (index >= Count)
            {
                return null;
            }

            return _steps[index];
        }
    }

    [Serializable]
    public class Step
    {
        [SerializeField] private string _name;
        [SerializeField] private float _cameraZoom;
        [SerializeField] private Vector2 _cameraPosition;
        [SerializeField] private ActivableGroup[] _activableGroups;

        public float CameraZoom => _cameraZoom;
        public Vector2 CameraPosition => _cameraPosition;
        public ActivableGroup[] ActivableGroups => _activableGroups;
    }

    [Serializable]
    public class ActivableGroup
    {
        [SerializeField] private Activable[] _activables;
        [SerializeField] private Activable[] _deactivables;

        public Activable[] Activables => _activables;
        public Activable[] Deactivables => _deactivables;
    }
}
