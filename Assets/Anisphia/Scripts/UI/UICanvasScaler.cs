using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;
using EDescriptionType = InspectorDescriptionAttribute.EDescriptionType;

[InspectorDescription("CanvasScalerの詳細はこのスクリプト内で行っています",EDescriptionType.Info)]
[RequireComponent(typeof(CanvasScaler))]
public class UICanvasScaler : MonoBehaviour
{
    private CanvasScaler _canvasScaler;

    private void Awake()
    {
        _canvasScaler = GetComponent<CanvasScaler>();

        if (_canvasScaler == null) return;

        _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _canvasScaler.referenceResolution = AnisphiaDefine.GameFramne.SCREEN_SIZE;
        _canvasScaler.screenMatchMode = ScreenMatchMode.MatchWidthOrHeight;
        _canvasScaler.matchWidthOrHeight = 0;
        _canvasScaler.referencePixelsPerUnit = 100;
    }
}
