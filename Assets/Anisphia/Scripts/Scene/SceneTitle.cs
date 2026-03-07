using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class SceneTitle : SceneBase
{
    [SerializeField] private TitlePresenter _presenter = default;

    protected override void OnAwake()
    {
        //継承側では処理なし
    }


    public override async UniTask InitAsync()
    {
        _presenter.InitPresenter();

        _presenter.OnStageSelectAction = OnClickStageSelect;
        _presenter.OnSettingAction = OnClickSettting;
        _presenter.OnGameExitAction = OnClickGameExit;

        await UniTask.CompletedTask;
    }

    private async void OnClickStageSelect()
    {
        //AnisphiaMainSystem.Instance.SceneManager.LoadScene(Anis.Scene.SceneManager.ESceneType.StageSelect);

        await AnisphiaMainSystem.Instance.SceneManager.LoadSceneAync(Anis.Scene.SceneManager.ESceneType.StageSelect);

        Debug.Log("ステージ読み込み完了");
    }

    private void OnClickSettting()
    {
        Debug.Log("設定画面");
    }

    private void OnClickGameExit()
    {
        Debug.Log("ゲームを終了します");

        AnisphiaMainSystem.Instance.AppQuit();
    }
}
