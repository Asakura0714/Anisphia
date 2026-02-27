using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SettingWindow : MonoBehaviour
{
    [SerializeField] SettingScreenBase _otherWindow = default;
    [SerializeField] SettingScreenBase _soundWindow = default;

    public void Setup()
    {
        //セーブデータから値を取得

        //セーブデータから値を適応
        _otherWindow.Setup();
        _soundWindow.Setup();
    }
}
