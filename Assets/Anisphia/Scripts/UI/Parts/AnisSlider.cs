using Cysharp.Threading.Tasks.Triggers;
using System;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;


namespace Anis.UI
{
    public struct AnisSliderData
    {
        public float MaxValue;
        public float MinValue;
    }

    [RequireComponent(typeof(Slider))]
    public class AnisSlider : MonoBehaviour
    {
        private Slider _slider;

        public float MaxValue => _slider.maxValue;
        public float MinValue => _slider.minValue;

        public float CurrentValue => _slider.value;

        public Action<float> OnChangeSliderAction;

        public void Setup(AnisSliderData data)
        {
            _slider = GetComponent<Slider>();

            if (_slider == null)
            {
                Debug.LogError("スライダーの取得に失敗しました");
                return;
            }

            _slider.maxValue  = data.MaxValue;
            _slider.minValue  = data.MinValue;

            _slider.onValueChanged.AddListener(OnChangeSlider);
        }

        private void OnChangeSlider(float value)
        {
            OnChangeSliderAction?.Invoke(value);
        }
    }
}
