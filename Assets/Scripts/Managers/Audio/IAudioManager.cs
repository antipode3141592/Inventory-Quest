using UnityEngine;

namespace InventoryQuest.Audio
{
    public interface IAudioManager
    {
        public void PlayMusicTrack(AudioClip audioClip);
        public void PlaySFX(AudioClip audioClip);
        public void StopMusicTrack();
    }
}
