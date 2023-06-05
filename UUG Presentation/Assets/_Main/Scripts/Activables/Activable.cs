using UnityEngine;

namespace Horothenic
{
    public class Activable : MonoBehaviour
    {
        public const float TRANSITION_TIME = 1f;

        protected virtual void Start()
        {
            JustDisappear();
        }

        public virtual void Activate()
        {
            JustAppear();
        }

        public virtual void Deactivate()
        {
            JustDisappear();
        }

        public void JustAppear()
        {
            gameObject.SetActive(true);
        }

        public void JustDisappear()
        {
            gameObject.SetActive(false);
        }
    }
}
