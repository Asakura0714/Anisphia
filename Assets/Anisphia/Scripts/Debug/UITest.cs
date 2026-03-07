using UnityEngine;
using Anis.Input;

public class UITest : MonoBehaviour
{
    [SerializeField] private AnisphiaButton[] _buttons;

    private InputManager _inputManager;
    int state_Decision = 0;
    int state_Cancel = 0;
    int state_Home = 0;
    int state_tab = 0;
    int state_Navi = 0;
    int state_any = 0;

    int currentButtonCunt = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputManager = AnisphiaMainSystem.Instance.InputManager;

        if (_inputManager != null)
        {
            _inputManager.SetEnableInputAction(EEnableInputType.UI);
        }

        foreach (var button in _buttons)
        {
            button.Init(AnisphiaButton.EButtonState.Idle);
        }

        //1番目のボタンだけ選択中にする
        _buttons[0].UpdateState(AnisphiaButton.EButtonState.Focus,true);
    }

    // Update is called once per frame
    void Update()
    {
        //bool isPush = _inputManager.GetUI.Navigate.WasPerformedThisFrame();
        Vector2 axis = _inputManager.GetUI.Navigate.ReadValue<Vector2>();

       
        //KeyInput(ref state_Decision,EUIInputType.Decision);
        //KeyInput(ref state_Cancel, EUIInputType.Cancel);
        //KeyInput(ref state_Home, EUIInputType.Home);
        //KeyInput(ref state_tab, EUIInputType.Tab);
        KeyInput(ref state_Navi, EUIInputType.Navigate);
        //KeyInput(ref state_any, EUIInputType.Any);


        //if (isPush)
        //{
        //    //押下時の瞬間だけ取る
        //    if (_inputManager.GetUI.Navigate.IsPressed())
        //    {
        //        bool isUp = axis.y > 0.3f;
        //        if (isUp)
        //        {
        //            Debug.Log("Up");
        //        }
        //        else if (axis.y < 0.1f)
        //        {
        //            Debug.Log("Donw");
        //        }
        //        else if (axis.x > 0.3)
        //        {
        //            Debug.Log("Right");
        //        }
        //        else
        //        {
        //            Debug.Log("Left");
        //        }
        //    }
        //}
    }

    public void KeyInput(ref int state,EUIInputType inputType)
    {
        switch (state)
        {
            case 0:
                {
                    bool isPush = _inputManager.IsGetPressedInput(inputType);
                    if (isPush)
                    {
                        Debug.Log($"IsPush : {inputType}");

                        if (inputType == EUIInputType.Home)
                        {
                            //「キャンセル」と「決定」を入れ替え
                            _inputManager.SwapDecisionAndCancel();
                        }

                        //タブを押した場合
                        if (inputType == EUIInputType.Tab || inputType == EUIInputType.Navigate)
                        {
                            //方向のインターフェイスを実装してるか？
                            var dirInter = _inputManager.GetDirectionalInterfece(inputType);
                            if (dirInter == null)
                            {
                                Debug.LogWarning($"InputAction : {inputType} は方向を取得できません");
                                return;
                            }


                            int preButtonNum = currentButtonCunt;

                            //押下したボタンの方向を取得
                            var dir = dirInter.GetDirectionalType();
                            if (dir == EUIDirectionalType.Up)
                            {
                                Debug.Log($"{inputType} : 上");
                            }
                            else if (dir == EUIDirectionalType.Down)
                            {
                                Debug.Log($"{inputType} : 下");
                            }
                            else if (dir == EUIDirectionalType.Right)
                            {
                                Debug.Log($"{inputType} : 右");
                                currentButtonCunt--;
                            }
                            else if (dir == EUIDirectionalType.Left)
                            {
                                Debug.Log($"{inputType} : 左");
                                currentButtonCunt++;
                            }
                            else
                            {
                                Debug.Log($"{inputType} : このログが表示されることはない　");
                            }

                            if (currentButtonCunt < 0)
                            {
                                currentButtonCunt = _buttons.Length - 1;
                            }
                            if (currentButtonCunt > _buttons.Length - 1)
                            {
                                currentButtonCunt = 0;
                            }

                            _buttons[preButtonNum].UpdateState(AnisphiaButton.EButtonState.Idle, true);
                            _buttons[currentButtonCunt].UpdateState(AnisphiaButton.EButtonState.Focus, true);

                        }

                        state++;
                    }

                    break;
                }

            case 1:
                {
                    //音とか鳴らす
                    //カーソルを移動したり
                    state++;
                    Debug.Log($"押下時の処理 : {inputType}");
                    break;
                }
            case 2:
                {
                    bool isRelease = _inputManager.IsGetReleasedInput(inputType);

                    if (isRelease)
                    {
                        Debug.Log($"IsRelease : {inputType}");
                        state = 0;
                        return;
                    }

                    break;
                }
        }
    }
}
