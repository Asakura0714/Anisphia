using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Anis.Input
{
    /// <summary>
    /// 接続されている入力
    /// </summary>
    public enum EDeviceType
    {
        None,
        KeyboradMouse,
        Gamepad,
    }

    public enum EEnableInputType
    {
        Player,
        UI
    }

    public enum EUIInputType
    {
        Decision,   //決定
        Cancel,     //キャンセル
        Home,       //ホーム
        Any,        //どこでも
        Navigate,   //選択の移動
        Tab,        //ページめくり
    }

    public enum EUIDirectionalType
    {
        None,
        Up,
        Down,
        Right,
        Left
    }

    public class InputManager : ManagerBase
    {
        private InputControl _control;

        private Action<Vector2> _playerMoveAction;
        private Action<Vector2> _playerAimAction;
        private Action<InputAction.CallbackContext> _playerFireAction;
        private Action<InputAction.CallbackContext> _playerMineAction;
        private Action<InputAction.CallbackContext> _playerPauseAction;

        public bool UseCurrentMouse { get; private set; }

        public EEnableInputType CurrentInpuType { get; private set; }

        public InputControl.UIActions GetUI => _control.UI;

        public bool IsCurrentInputActionUI => AnisphiaMainSystem.Instance.InputManager.CurrentInpuType == EEnableInputType.UI;

        private Dictionary<EUIInputType, UIInputModuleBase> _inputDictionary = new();

        public override void Setup()
        {
            _control = new InputControl();

            _control.Player.Fire.performed  += (content => _playerFireAction?.Invoke(content));
            _control.Player.Mine.performed  += (content => _playerMineAction?.Invoke(content));
            _control.Player.Pause.performed += (content => _playerPauseAction?.Invoke(content));

            InputSystem.onActionChange += OnActionChange;

            var actionUi = _control.UI;

            UIInputModuleBase decision = new InputDecisionModule(actionUi.Decision);
            UIInputModuleBase cancel = new InputCancelModule(actionUi.Cancel);
            UIInputModuleBase home = new InputHomeModule(actionUi.Home);
            UIInputModuleBase any = new InputAnyModule(actionUi.Any);
            UIInputModuleBase navi = new InputNavigateModule(actionUi.Navigate);
            UIInputModuleBase tab = new InputTabModule(actionUi.Tab);

            _inputDictionary.Add(EUIInputType.Decision, decision);
            _inputDictionary.Add(EUIInputType.Cancel, cancel);
            _inputDictionary.Add(EUIInputType.Home, home);
            _inputDictionary.Add(EUIInputType.Any, any);
            _inputDictionary.Add(EUIInputType.Navigate, navi);
            _inputDictionary.Add(EUIInputType.Tab, tab);



#if !UNITY_EDITOR
            //zACursor.visible = false;
            //Cursor.lockState = CursorLockMode.Confined;
#endif
        }
        private void OnActionChange(object obj, InputActionChange change)
        {
            // 1. まず「アクションが実行された瞬間」以外は無視する
            if (change != InputActionChange.ActionPerformed) return;

            // 2. 渡された obj が InputAction 型かどうかを安全に判定（キャストエラー防止）
            if (obj is InputAction action)
            {
                // アクションを起こしたデバイスを取得
                var device = action.activeControl?.device;

                if (device == null) return;

                // マウス/キーボード判定を更新
                UseCurrentMouse = device is Mouse || device is Keyboard;
            }
        }


        public void SetBindPlayerInput(Action<Vector2> moveAxis,
                                       Action<Vector2> aimAxis,
                                       Action<InputAction.CallbackContext> actionFire,
                                       Action<InputAction.CallbackContext> actionMine)
        {
            if (_control == null)
            {
                return;
            }

            //スティックとか十字キーのBind
            _playerMoveAction = moveAxis;

            //Aiｍ
            _playerAimAction = aimAxis;

            //FIre
            _playerFireAction = actionFire;

            //Mine
            _playerMineAction = actionMine;

        }

        public void SetBindPause(Action<InputAction.CallbackContext> actionPause)
        {
            if (_control == null)
            {
                return;
            }

            _playerPauseAction = actionPause;
        }

        /// <summary>
        /// 有効タイプを設定
        /// </summary>
        /// <param name="enable"></param>
        public void SetEnableInputAction(EEnableInputType type)
        {
            CurrentInpuType = type;

            switch (type)
            {
                case EEnableInputType.Player:
                    {
                        //プレイヤーの操作を有効
                        _control.Player.Enable();

                        //UIの操作を無効
                        _control.UI.Disable();
                    }
                    break;
                case EEnableInputType.UI:
                    {
                        //プレイヤーの操作を無効
                        _control.Player.Disable();

                        //UIの操作を有効
                        _control.UI.Enable();
                    }
                    break;
                default:
                    break;
            }
        }

        public override void OnDelete()
        {

        }

        public override void OnUpdate()
        {
            //プレイヤーの移動入力を監視する
            bool Moveflg = _control.Player.Move.IsPressed();
            Vector2 leftAxis = _control.Player.Move.ReadValue<Vector2>();
            if (Moveflg && IsOverDeadZone(leftAxis))
            {
                _playerMoveAction?.Invoke(leftAxis.normalized);
            }

            Vector2 rightAxis = _control.Player.Aim.ReadValue<Vector2>();
            if (UseCurrentMouse)
            {
                //プレイヤーの入力へ渡す
                _playerAimAction?.Invoke(rightAxis.normalized);
            }
            else
            {
                //DeadZoneを超えないと弾く
                if (IsOverDeadZone(rightAxis) == false)
                {
                    return;
                }

                //プレイヤーの入力へ渡す
                _playerAimAction?.Invoke(rightAxis.normalized);
            }
        }

        /// <summary>
        /// 入力のDeadZoneを判定する
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        private bool IsOverDeadZone(Vector2 axis)
        {
            bool isOverX = Mathf.Abs(axis.x) > AnisphiaDefine.Input.DEADZONE;
            bool isOverY = Mathf.Abs(axis.y) > AnisphiaDefine.Input.DEADZONE;

            return isOverX || isOverY;
        }


        //決定を「押された瞬間」
        public bool IsGetPressedInput(EUIInputType inputType)
        {
            //InpuActionを取得
            _inputDictionary.TryGetValue(inputType, out var pressedInput);
            if (pressedInput != null)
            {
                return pressedInput.IsPressed();
            }

            Debug.LogError("InputActionの取得に失敗しました");

            return false;
        }

        //決定を「離した瞬間」
        public bool IsGetReleasedInput(EUIInputType inputType)
        {
            //InpuActionを取得
            _inputDictionary.TryGetValue(inputType, out var pressedInput);
            if (pressedInput != null)
            {
                return pressedInput.IsReleased();
            }

            Debug.LogError("InputActionの取得に失敗しました");

            return false;
        }

        //決定を「押下中」
        public bool IsGetHoldInput(EUIInputType inputType)
        {
            //InpuActionを取得
            _inputDictionary.TryGetValue(inputType, out var pressedInput);
            if (pressedInput != null)
            {
                return pressedInput.IsHold();
            }

            Debug.LogError("InputActionの取得に失敗しました");

            return false;
        }

        /// <summary>
        /// 決定とキャンセルを入れ替える
        /// </summary>
        public void SwapDecisionAndCancel()
        {
            var DecisionInput = _inputDictionary[EUIInputType.Decision];
            var CancelInput = _inputDictionary[EUIInputType.Cancel];

            _inputDictionary[EUIInputType.Decision] = CancelInput;
            _inputDictionary[EUIInputType.Cancel] = DecisionInput;
        }

        public IDirectionalInput GetDirectionalInterfece(EUIInputType inputType)
        {
            _inputDictionary.TryGetValue(inputType, out var inputAction);

            return inputAction as IDirectionalInput;
        }
    }
}
