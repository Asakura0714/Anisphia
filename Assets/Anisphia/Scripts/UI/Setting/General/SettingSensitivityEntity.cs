using System;

namespace Anis.UI.Setting
{
    [Serializable]
    public struct SettingSensitivityData
    {
        public float Sensitivity;

        public SettingSensitivityData(float inSensitivity)
        {
            Sensitivity = inSensitivity;
        }
    }

    public class SettingSensitivityEntity
    {
        public SettingSensitivityData SensitivityData { get; private set; }

        public void Initialize(SettingSensitivityData inData)
        {
            SensitivityData = inData;
        }
    }
}
