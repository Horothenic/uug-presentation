using System;
using TMPro;
using UnityEngine;

namespace Horothenic
{
    public class MonthStep : MonoBehaviour
    {
        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _text;

        public void Initialize(int index)
        {
            var date = new DateTime(1, index + 1, 1);
            _text.text = date.ToString("MMM");
        }
    }
}
