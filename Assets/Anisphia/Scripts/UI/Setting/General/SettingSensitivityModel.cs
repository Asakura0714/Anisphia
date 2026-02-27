
namespace Anis.UI.Setting
{
    public class SettingSensitivityModel
    {
        /// <summary>
        /// マウスの感度
        /// </summary>
        public SettingSensitivityEntity MauseSensitivityEntity { get; private set; }

        /// <summary>
        /// コントローラーの右スティックの感度
        /// </summary>
        public SettingSensitivityEntity ContollerRightStickSensitivityEntity { get; private set; }


        public SettingSensitivityModel(SettingSensitivityData inMauseData, SettingSensitivityData inRightStickData)
        {
            //マウス
            MauseSensitivityEntity = new SettingSensitivityEntity();
            MauseSensitivityEntity.Initialize(inMauseData);

            //コントローラー
            ContollerRightStickSensitivityEntity = new SettingSensitivityEntity();
            ContollerRightStickSensitivityEntity.Initialize(inMauseData);
        }
    }
}