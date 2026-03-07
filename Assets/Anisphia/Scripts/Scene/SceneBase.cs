using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    private void Awake()
    {
        AnisphiaMainSystem.Instance.SceneManager.RegisterSceneBase(this);

        OnAwake();
    }

    //Œp³‚ğ‹­§‚³‚¹‚é
    protected abstract void OnAwake();

    //ƒV[ƒ“Å‰‚Ì‰Šú‰»
    public virtual async UniTask InitAsync()
    {
        await UniTask.CompletedTask;
    }
}
