using Anis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Anis.UI;

public class SettingPatsSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemNameText = default;
    [SerializeField] private TextMeshProUGUI _itemNumberText = default;
    [SerializeField] private AnisSlider _slider = default;


    public void Start()
    {
        AnisSliderData data = new AnisSliderData()
        {
            MaxValue = 1,
            MinValue = 0
        };

        _slider.Setup(data);
        _slider.OnChangeSliderAction += OnChangeSliderValue;
    }

    public void OnChangeSliderValue(float sliderValue)
    {
        //‚¢‚Á‚½‚ñ100”{‚É‚µ‚Ä‚¨‚­
        float a = sliderValue * 100f;

        _itemNumberText.SetText(a.ToString());
    }

    public void Setup()
    {

    }

    public void OnUpdateItemNameText(string inText)
    {
        if (_itemNameText == null)
        {
            return;
        }

        _itemNameText.SetText(inText);
    }
}
