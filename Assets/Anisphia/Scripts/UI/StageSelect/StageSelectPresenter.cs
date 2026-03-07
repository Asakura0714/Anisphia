using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageSelectPresenter : MonoBehaviour
{
    [SerializeField] private StageSelectView _view = default;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _view.OnClickStageButtonAction += OnClickStageSelect;

        _view.Init();   
    }

    /// <summary>
    /// ステージ選択
    /// </summary>
    private void OnClickStageSelect()
    {
        Debug.Log("ステージを選択");

        //MainGameへ移動
        AnisphiaMainSystem.Instance.SceneManager.LoadSceneAync(Anis.Scene.SceneManager.ESceneType.MainGame).Forget();
    }
}
