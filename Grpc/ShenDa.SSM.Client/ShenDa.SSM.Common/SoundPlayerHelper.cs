using System;

namespace ShenDa.SSM.Common
{
    public class SoundPlayerHelper
    {
        public static void PlaySound(string mediaName, string mediaPath = "Sounds", string mediaExtension = "wav")
        {
            var fullPath = $"{mediaPath}/{mediaName}.{mediaExtension}";
            //using (var player = new SoundPlayer(fullPath))
            //{
            //    player.Play();
            //}
        }
    }
}
