using Anis.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SceneMainGame : SceneBase
{
    [SerializeField] private MainGamePresenter _uiPresenter = default;
    [SerializeField] private PlayerTank _playerTank = default;

    //public override void Awake()
    //{
    //    InitScene();
    //}

    protected override void OnAwake()
    {
        //継承側では処理なし
    }

    public override async UniTask InitAsync()
    {
        _playerTank.Setup();

        //Playerの操作情報はBinidしたので、Playerへ切り替える
        AnisphiaMainSystem.Instance.InputManager.SetEnableInputAction(EEnableInputType.Player);

        _uiPresenter.OnClickPauseAction = OnPause;
        _uiPresenter.OnClickResumeAction = OnResume;
        _uiPresenter.OnClickRetryAction = OnRetry;
        _uiPresenter.OnClickStageSelectAction = OnSelectStage;
        _uiPresenter.OnClickSettingAction = OnSetting;

        //デバッグ用の処理
        _uiPresenter.OnClickSettingAction = () =>
        {
            AnisphiaMainSystem.Instance.AppQuit();
        };

        //UI初期化
        _uiPresenter.Init();

        await UniTask.CompletedTask;
    }

    /// <summary>
    /// ゲームの進行を止める
    /// </summary>
    /// <param name="isStop"></param>
    public void SetGameTimeScale(bool isStop)
    {
        Time.timeScale = isStop ? 0f : 1f;
    }

    #region Pause

    /// <summary>
    /// ポーズ中
    /// </summary>
    /// <param name="pauseContext"></param>
    private void OnPause(InputAction.CallbackContext pauseContext)
    {
        Debug.Log("ポーズ");

        //UIのInputActionを有効にする
        AnisphiaMainSystem.Instance.InputManager.SetEnableInputAction(EEnableInputType.UI);

        //マウスカーソルを非表示
        _playerTank.PlayerInput.SetSetActiveMouseCursor(false);

        //ポーズ画面表示
        _uiPresenter.SetActivePauseScreen(true);

        //ゲームを停止
        SetGameTimeScale(true);

        //ポーズを開いた時にFocusを行う
        EventSystem.current.SetSelectedGameObject(_uiPresenter.GetSelectedGameObject);
    }

    /// <summary>
    /// ゲーム再開
    /// </summary>
    private void OnResume()
    {
        Debug.Log("ゲーム再開");

        //マウスカーソルを表示
        _playerTank.PlayerInput.SetSetActiveMouseCursor(true);

        //ポーズ画面非表示
        _uiPresenter.SetActivePauseScreen(false);

        //ゲームを停止
        SetGameTimeScale(false);

        //PlayerのInputActionを有効にする
        AnisphiaMainSystem.Instance.InputManager.SetEnableInputAction(EEnableInputType.Player);
    }

    /// <summary>
    /// ゲームやりおなし
    /// </summary>
    private void OnRetry()
    {
        Debug.Log("ゲームやり直し");

        //ポーズ画面非表示
        _uiPresenter.SetActivePauseScreen(false);

        //ゲームを停止
        SetGameTimeScale(false);

        AnisphiaMainSystem.Instance.SceneManager.LoadSceneAync(Anis.Scene.SceneManager.ESceneType.MainGame).Forget();
    }

    /// <summary>
    /// ステージ選択画面へ
    /// </summary>
    private void OnSelectStage()
    {
        Debug.Log("ステージ選択へ");

        //ポーズ画面非表示
        _uiPresenter.SetActivePauseScreen(false);

        //ゲームを停止
        SetGameTimeScale(false);

        //ステージ選択へ
        AnisphiaMainSystem.Instance.SceneManager.LoadSceneAync(Anis.Scene.SceneManager.ESceneType.StageSelect).Forget();
    }

    /// <summary>
    /// 設定画面を開く
    /// </summary>
    private void OnSetting()
    {
        Debug.Log("設定画面を開くよ");
    }

    #endregion
}
