using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;

public class PlayerTankInput : MonoBehaviour
{
    [SerializeField] private VirtualMouseInput _virtualMouseInput;

    /// <summary>
    /// 基本は等倍※後で設定からここを操作して感度を変える
    /// </summary>
    /// 
    [SerializeField]
    private float _rightStickSensitivity = 1f;

    [SerializeField]
    private float _mauseSensitivity = 1f;

    [SerializeField]
    bool isMouce = false;

    /// <summary>
    /// ここは変更しない
    /// </summary>
    private float CursolMoveSpeed = 1000f;

    public Transform MyTransform { get; private set; }
    private Vector2 _cursorSize;
    private Vector3 _lastMousePosition;

    public void Setup()
    {
        MyTransform = _virtualMouseInput.cursorTransform;
        var rectTrans = _virtualMouseInput.cursorTransform;

        _cursorSize = new Vector2(rectTrans.rect.width / 2f, rectTrans.rect.height / 2f);
    }


    public void MoveMouceCursol(Vector2 inputAxis)
    {
        //UI操作中は処理をしない
        if(AnisphiaMainSystem.Instance.InputManager.IsCurrentInputActionUI)
        {
            return;
        }

        //現在使用中なのがマウスの時
        if (AnisphiaMainSystem.Instance.InputManager.UseCurrentMouse)
        {
            // 前フレームからの「移動量（Delta）」を計算
            Vector2 mouseDelta = Input.mousePosition - _lastMousePosition;

            //移動量に感度反映
            MyTransform.position += (Vector3)(mouseDelta * _mauseSensitivity);

            //次フレームのために現在のマウス位置を記録（※Warp前を記録）
            _lastMousePosition = Input.mousePosition;
        }
        else
        {
            //移動量の計算
            MyTransform.position += GetCursolSpeed(inputAxis);
        }

        //画面範囲外をチェック
        if (CheckScreenEdge())
        {
#if !UNITY_EDITOR
            //画面内に収める
            SetScreenLimitPosition();
#endif
        }

        if (AnisphiaMainSystem.Instance.InputManager.UseCurrentMouse)
        {
            Mouse.current.WarpCursorPosition(MyTransform.position);

            // Warpした後の位置を記録しておくことで、Warpによる移動をDeltaとして拾わないようにする
            _lastMousePosition = MyTransform.position;
        }
    }

    private Vector3 GetCursolSpeed(Vector2 inputAxis)
    {
        var velo = inputAxis * _rightStickSensitivity * CursolMoveSpeed * Time.deltaTime;
        return new Vector3(velo.x, velo.y, 0f);
    }

    /// <summary>
    /// マウスカーソルの表示制御を行う
    /// </summary>
    /// <param name="isSetActive"></param>
    public void SetSetActiveMouseCursor(bool isSetActive)
    {
        _virtualMouseInput.cursorGraphic.gameObject.SetActive(isSetActive);
    }

    private void SetScreenLimitPosition()
    {
        //マウスカーソルの半分の値を取得
        float halfWidth = _cursorSize.x;
        float halfHeight = _cursorSize.y;

        //値の丸め込み
        float clampedX = Mathf.Clamp(MyTransform.position.x, halfWidth, Screen.width - halfWidth);
        float clampedY = Mathf.Clamp(MyTransform.position.y, halfHeight, Screen.height - halfHeight);

        MyTransform.position = new Vector3(clampedX, clampedY, 0f);
    }

    private bool CheckScreenEdge()
    {
        float x = MyTransform.position.x;
        float y = MyTransform.position.y;
        return x <= _cursorSize.x || x >= Screen.width - _cursorSize.x ||
               y <= _cursorSize.y || y >= Screen.height - _cursorSize.y;
    }

    public Vector3 GetMauseCursolWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(MyTransform.position);
    }
}
