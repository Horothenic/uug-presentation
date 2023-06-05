using UnityEngine;
using DG.Tweening;

namespace Horothenic
{
    public class MoveActivable : Activable
    {
        private static readonly float START_POSITION = 5;

        private Vector3 originalPosition;
        private Vector3 hidePosition;

        protected override void Start()
        {
            base.Start();

            originalPosition = transform.position;

            hidePosition = originalPosition;
            hidePosition.y *= START_POSITION;
        }

        public override void Activate()
        {
            transform.position = hidePosition;

            base.Activate();

            transform.DOMove(originalPosition, TRANSITION_TIME);
        }

        public override void Deactivate()
        {
            transform.DOMove(hidePosition, TRANSITION_TIME).OnComplete(base.Deactivate);
        }
    }
}
