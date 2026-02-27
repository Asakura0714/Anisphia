using UnityEngine;

public static class AnisphiaDefine
{
    public static class Input
    {
        /// <summary>
        /// Stickのデットゾーン
        /// </summary>
        public const float DEADZONE = 0.15f; 
    }

    public static class GameFramne
    {
        /// <summary>
        /// プロジェクトのスクリーンサイズ：横
        /// </summary>
        public static readonly float SCREEN_HORIZONTAL = 1920f;

        /// <summary>
        /// プロジェクトのスクリーンサイズ：縦
        /// </summary>
        public static readonly float SCREEN_VERTICAL = 1080f;

        /// <summary>
        /// プロジェクトのスクリーンサイズ：横/横
        /// </summary>
        public static readonly Vector2 SCREEN_SIZE = new Vector2(SCREEN_HORIZONTAL, SCREEN_VERTICAL);
    }
}
