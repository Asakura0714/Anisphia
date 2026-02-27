using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using TMPro;

public class PlayerTank : TankBase
{

    [SerializeField] private PlayerTankInput _playerInput = default;
    [SerializeField] private TankTurretBase _tankTurret = default;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;


    public PlayerTankInput PlayerInput => _playerInput;

    struct InputData
    {
        EMoveDirection moveType;
        float inputAngle;
    }

    private enum EMoveState
    {
        Idle,   //待機
        InputCalc,//計算
        Rotate,//回転
        Move//移動
    }
    private EMoveState eMoveState = EMoveState.Idle;

    private InputData inputData;

    private enum EMoveDirection
    {
        None = 999,
        Right = 0,
        RightUp,
        Up,
        LeftUp,
        Left,
        LeftDown,
        Down,
        RightDown,

        //基本使わんよ
        MAX
    }

    public override void Setup()
    {
        base.Setup();

        _playerInput.Setup();

        //操作情報をセット
        AnisphiaMainSystem.Instance.InputManager.SetBindPlayerInput(OnMove,OnAim,OnFire,OnMine);
    }

    private void OnFire(InputAction.CallbackContext actionFire)
    {
        OnFire();
    }

    private void OnMine(InputAction.CallbackContext actionMire)
    {
        OnMine();
    }

    protected override void OnMove(Vector2 moveDir)
    {
        if (moveDir.magnitude <= 0.001f)
        {
            return;
        }

        //8方向から入力方向を取得
        var inputTuple = GetInputDirection(moveDir);

        //現在の入力角度を取得
        var eInputDir = inputTuple.Item1;
        var inputAngle = inputTuple.Item2;

        switch (eMoveState)
        {
            case EMoveState.Idle:
                {
                    eMoveState = EMoveState.InputCalc;
                }
                break;
            case EMoveState.InputCalc:
                {
                    eMoveState = EMoveState.Rotate;
                }
                break;
            case EMoveState.Rotate:
                {
                    Vector3 currentAngle = transform.localEulerAngles;

                    // Y軸に 0.01 度足す
                    currentAngle.y -= 0.1f;

                    // 新しい角度をセットし直す
                    transform.localEulerAngles = currentAngle;

                    Debug.Log($"{currentAngle}");

                    if (currentAngle.y < 0f)
                    {
                        eMoveState = EMoveState.Move;
                    }
                }
                break;
            case EMoveState.Move:
                break;
            default:
                break;
        }
    }
    protected override void OnAim(Vector2 moveDir)
    {
        //マウスカーソルを動かす
        _playerInput.MoveMouceCursol(moveDir);
    }

    protected override void OnFire()
    {
        base.OnFire();

        _tankTurret.OnFire();
        //Debug.Log("Player Fire");
    }
    protected override void OnMine()
    {
        base.OnMine();
        //Debug.Log("Player Mine");
    }

    /// <summary>
    /// 入力方向を8方向に
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    private (EMoveDirection,float) GetInputDirection(Vector2 axis)
    {
        //入力角を度数に
        var angle = Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg;
        if (angle < 0f)
        {
            angle += 360f;
        }

        int split = (int)EMoveDirection.MAX;
        int splitAngle = 360 / split;
        var dirIndex = Mathf.RoundToInt(angle / splitAngle) % split;

        return ((EMoveDirection)dirIndex,angle);
    }

    private int GetRaycastMask => 1 << 6;

    public void Update()
    {
        int distance = 1000;

        Ray ray = Camera.main.ScreenPointToRay(_playerInput.MyTransform.position);
        RaycastHit hit;
        // Rayを飛ばして当たり判定をチェック
        if (Physics.Raycast(ray, out hit, distance, GetRaycastMask))
        {
            var worldPos = new Vector3(hit.point.x, 0f, hit.point.z);


            _tankTurret.SetLookAtCursol(worldPos);
        }
    }

}
