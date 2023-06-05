using UnityEngine;
using DG.Tweening;

namespace Horothenic
{
    public class ScaleActivable : Activable
    {
        private static readonly Vector3 ACTIVATED_SCALE = Vector3.one;
        private static readonly Vector3 DEACTIVATED_SCALE = Vector3.zero;

        public override void Activate()
        {
            transform.localScale = DEACTIVATED_SCALE;

            base.Activate();

            transform.DOScale(ACTIVATED_SCALE, TRANSITION_TIME);
        }

        public override void Deactivate()
        {
            transform.DOScale(DEACTIVATED_SCALE, TRANSITION_TIME).OnComplete(base.Deactivate);
        }
    }
}
