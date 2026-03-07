using UnityEngine;

namespace Anis.UI.Setting
{
    public class SettingGeneralPresenter : MonoBehaviour
    {
        //[SerializeField] SettingGeneralView _view = default;

        private SettingSensitivityModel _sensitivityModel;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SetupSettingSensitivity();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetupSettingSensitivity()
        {
            //TODO：セーブデータからひっぱってくるよ

            //マウスの感度のデータを設定
            var mauseData = new SettingSensitivityData(50);

            //コントローラーの右スティックのデータを生成
            var controllerRightData = new SettingSensitivityData(50);

            //感度用のModelを生成
            _sensitivityModel = new SettingSensitivityModel(mauseData, controllerRightData);
        }
    }
}
