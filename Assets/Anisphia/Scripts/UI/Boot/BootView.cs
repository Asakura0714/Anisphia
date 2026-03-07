using System;
using UnityEngine;

public class BootView : ViewBase
{
    [SerializeField] private AnisButtonBase _aSelectButton = default;
    [SerializeField] private AnisButtonBase _bSelectButton = default;
    [SerializeField] private AnisButtonBase _noControllerButton = default;

    public Action OnClickToTitle;

    public override void InitView()
    {
        _aSelectButton.Init(OnClickToTitle);
        _bSelectButton.Init(OnClickToTitle);
        _noControllerButton.Init(OnClickToTitle);
    }

    public override AnisButtonBase FirstSelectedGameObject()
    {
        return _aSelectButton;
    }
}