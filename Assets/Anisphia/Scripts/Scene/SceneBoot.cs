using UnityEngine;
using Anis.Input;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class SceneBoot : SceneBase
{
    [SerializeField] private BootPresenter _presenter = default;

    protected override void OnAwake()
    {
        //Œp³‘¤‚Å‚Íˆ—‚È‚µ
    }

    public override async UniTask InitAsync()
    {
        _presenter.InitPresenter();

        //UI‚ğ—LŒø‚É‚·‚é
        AnisphiaMainSystem.Instance.InputManager.SetEnableInputAction(EEnableInputType.UI);

        await UniTask.CompletedTask;
    }
}
