using Anis.Input;
using Anis.Scene;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AnisphiaMainSystem : MonoBehaviour
{
    /// <summary>
    /// アプリのセットアップが完了したか？
    /// </summary>
    public static bool AppInitialized { get; private set; }

    /// <summary>
    /// インスタンス
    /// </summary>
    public static AnisphiaMainSystem Instance { get; private set; }

    public SaveDataManager SaveDataManager { get; private set; }

    public InputManager InputManager { get; private set; }

    public SoundManager SoundManager { get; private set; }

    public SceneManager SceneManager { get; private set; }

    public UIManager UIManager { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AppEntryPoint()
    {
        var go = new GameObject("AnisphiaMainSystem");
        var system = go.AddComponent<AnisphiaMainSystem>();

        DontDestroyOnLoad(go);
        Instance = system;
        Instance.Setup().Forget();
    }

    private async UniTask Setup()
    {
        InputManager = CreateManager<InputManager>() as InputManager;
        SaveDataManager = CreateManager<SaveDataManager>()as SaveDataManager;
        SoundManager = CreateManager<SoundManager>() as SoundManager;
        SceneManager = CreateManager<SceneManager>() as SceneManager;
        UIManager = CreateManager<UIManager>() as UIManager;

        //アプリ終了時にコール
        Application.quitting += AppQuitting;

        //神、準備完了
        AppInitialized = true;

        //現在開きたいシーンのAwakeを待機
        await UniTask.Yield();

        //最初のシーンが見つかるまで待機
        await UniTask.WaitUntil(() => SceneManager.CurrentSceneBase != null);

        //最初に開くシーンの初期化
        await SceneManager.CurrentSceneBase.InitAsync();
    }

    /// <summary>
    /// マネージャーを生成
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    private ManagerBase CreateManager<T>()where T : ManagerBase
    {
        //クラス名をオブジェクト名に設定
        var go = new GameObject(typeof(T).Name);
        
        //クラス生成
        ManagerBase manaBase = go.AddComponent<T>();
        
        //初期化開始
        manaBase.Setup();

        //全体で保持
        DontDestroyOnLoad(go);

        return manaBase;
    }

    private void AppQuitting()
    {
        if (Instance != null)
        {
            Instance = null;
        }

        InputManager.OnDelete();
        SaveDataManager.OnDelete();
        SoundManager.OnDelete();
        SceneManager.OnDelete();
        UIManager.OnDelete();
    }

    public void AppQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }

    private void Update()
    {
        InputManager.OnUpdate();
        UIManager.OnUpdate();
    }
}
