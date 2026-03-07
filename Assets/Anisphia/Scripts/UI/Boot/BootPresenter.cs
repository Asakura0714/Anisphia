using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class BootPresenter : PresenterBase
{
    [SerializeField] private BootView _view = default;

    public override void InitPresenter()
    {
        _view.OnClickToTitle += ToTitle;

        _view.InitView();

        EventSystem.current.SetSelectedGameObject(_view.FirstSelectedGameObject().gameObject);
    }

    private void ToTitle()
    {
        AnisphiaMainSystem.Instance.SceneManager.LoadSceneAync(Anis.Scene.SceneManager.ESceneType.Title).Forget();
    }
}
