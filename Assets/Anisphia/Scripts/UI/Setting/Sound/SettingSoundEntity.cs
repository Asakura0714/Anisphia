using System;

namespace Anis.UI.Setting
{
    [Serializable]
    struct SettingSoundData
    {
        public float Volume { get; private set; }

        public SettingSoundData(float inVolume)
        {
            Volume = inVolume;
        }
    }

    public class SettingSoundEntity
    {
        private SettingSoundData _soundData;

        public void Setup(float initVolume)
        {
            _soundData = new SettingSoundData(initVolume);
        }
    }
}
