using TMPro;
using UnityEngine;

namespace Horothenic
{
    public class YearStep : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private MonthStep[] _monthSteps;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int year;

        public void Initialize()
        {
            _text.text = year.ToString();

            for (var i = 0; i < _monthSteps.Length; i++)
            {
                _monthSteps[i].Initialize(i);
            }
        }
    }
}
