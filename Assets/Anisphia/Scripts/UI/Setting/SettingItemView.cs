using Anis.UI;
using UnityEngine;

public class SettingItemView : MonoBehaviour
{
    [SerializeField] private AnisSlider _slider = default;

    public void Setup()
    {
        AnisSliderData data = new AnisSliderData()
        {
            MaxValue = 100,
            MinValue = 0f
        };

        _slider.Setup(data);
    }
}
