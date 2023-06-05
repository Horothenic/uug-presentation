using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Horothenic
{
    public class AlphaActivable : Activable
    {
        private const float DEACTIVATED_ALPHA = 0f;
        private const float ACTIVATED_ALPHA = 1f;

        public override void Activate()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            var image = GetComponent<Image>();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = default;
            }

            if (image != null)
            {
                var noAlphaColor = image.color;
                noAlphaColor.a = DEACTIVATED_ALPHA;
                image.color = noAlphaColor;
            }

            base.Activate();

            if (canvasGroup != null)
            {
                canvasGroup.DOFade(ACTIVATED_ALPHA, TRANSITION_TIME);
                return;
            }

            if (image != null)
            {
                image.DOFade(ACTIVATED_ALPHA, TRANSITION_TIME);
                return;
            }
        }

        public override void Deactivate()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            var image = GetComponent<Image>();

            if (canvasGroup != null)
            {
                canvasGroup.DOFade(DEACTIVATED_ALPHA, TRANSITION_TIME).OnComplete(base.Deactivate);
                return;
            }

            if (image != null)
            {
                image.DOFade(DEACTIVATED_ALPHA, TRANSITION_TIME).OnComplete(base.Deactivate);
                return;
            }
        }
    }
}
