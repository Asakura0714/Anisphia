using System;
using UnityEngine;
using UnityEngine.UI;

public class AnisButtonBase : MonoBehaviour
{
    public Button MyButton {  get; private set; }

    public Action OnClick;

    public void Init(Action onClick)
    {
        MyButton = GetComponent<Button>();

        if(MyButton == null)
        {
            return;
        }

        MyButton.onClick.AddListener(() => onClick?.Invoke());
    }
}
