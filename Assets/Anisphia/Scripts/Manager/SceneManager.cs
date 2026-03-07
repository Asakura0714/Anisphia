using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Anis.Scene
{
    public class SceneManager : ManagerBase
    {
        public enum ESceneType
        {
            Boot,
            Title,
            StageSelect,
            MainGame,
        }
    
        private Dictionary<ESceneType, string> sceneList = new Dictionary<ESceneType, string>()
        {
            { ESceneType.Boot,"Boot"},
            { ESceneType.Title,"Title"},
            { ESceneType.StageSelect,"StageSelect"},
            { ESceneType.MainGame,"MainGame"}
        };

        private UniTaskCompletionSource<SceneBase> _sceneInitReadyTask;
        public SceneBase CurrentSceneBase { get; private set; }

        /// <summary>
        /// シーン名を取得する
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetSceneName(ESceneType type) => sceneList[type];

        public ESceneType CurrentSceneType { get; private set; }
    
        public async UniTask LoadSceneAync(ESceneType sceneType)
        {
            //前回のシーン情報が残ってるので毎回生成する
            _sceneInitReadyTask = new UniTaskCompletionSource<SceneBase>();

            CurrentSceneType = sceneType;

            string sceneName = GetSceneName(sceneType);

            //シーンロード開始
            await UnitySceneManager.LoadSceneAsync(sceneName).ToUniTask();

            //Scene上の初期化準備を待機
            SceneBase redaySceneBase = await _sceneInitReadyTask.Task;

            //準備ができたので初期化開始
            await redaySceneBase.InitAsync();
            
            //Sceneの初期が完了したので、フェードが開ける
        }
    
        public override void Setup()
        {
        }
    
        public override void OnDelete()
        {
            
        }

        /// <summary>
        /// 自分のインスタンスを登録
        /// </summary>
        /// <param name="scene"></param>
        public void RegisterSceneBase(SceneBase scene)
        {
            //自分自身を保存
            CurrentSceneBase = scene;

            //通知を飛ばす
            _sceneInitReadyTask?.TrySetResult(scene);
        }
    }
}
