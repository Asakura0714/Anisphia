using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class StageSelect : SceneBase
{
    protected override void OnAwake()
    {
        //継承側では処理なし
    }

    public override async UniTask InitAsync()
    {
        Debug.Log("ステージ選択の初期化を開始します！");

        await UniTask.Delay(TimeSpan.FromSeconds(5));

        Debug.Log("Complate!! Stage Select InitAsync");
    }
}
