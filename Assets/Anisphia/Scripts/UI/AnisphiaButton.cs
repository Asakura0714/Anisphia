using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnisphiaButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum EButtonState
    {
        Idle,       //待機
        Focus,      //選択中
        Selected,   //選択
    }

    /// <summary>
    /// ボタンの識別番号
    /// </summary>
    public int SerialNumber {  get; set; }

    /// <summary>
    /// 現在のボタンを状態
    /// </summary>
    public EButtonState CurrentState { get; private set; }

    private TMPro.TextMeshProUGUI _text;

    public Action OnIdleAction {  get; set; }
    public Action OnFocusAction{ get; set; }
    public Action OnSelectedAction{ get; set; }

    public void Init(EButtonState initState)
    {
        CurrentState = initState;

        if (_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        OnUpdateStateAction();
    }

    /// <summary>
    /// 状態を更新する
    /// </summary>
    /// <param name="nextState"></param>
    public void UpdateState(EButtonState nextState,bool isAction = false)
    {
        if (CurrentState == nextState)
        {
            return;
        }

        CurrentState = nextState;

        if (isAction)
        {
            //Stateが更新された時に発火する
            OnUpdateStateAction();
        }
    }

    private void OnUpdateStateAction()
    {
        switch (CurrentState)
        {
            case EButtonState.Idle:
                {
                    OnIdleAction?.Invoke();
                    SetText("待機中",Color.black);
                }
                break;
            case EButtonState.Focus:
                {
                    OnFocusAction?.Invoke();
                    SetText("選択中", Color.red);
                }
                break;
            case EButtonState.Selected:
                {
                    OnSelectedAction?.Invoke();
                    SetText("決定", Color.blue);
                }
                break;
            default:
                break;
        }
    }


    public void SetText(string inString,Color color)
    {
        _text.text = inString;
        _text.color = color;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
