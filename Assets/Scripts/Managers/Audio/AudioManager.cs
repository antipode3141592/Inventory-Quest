using UnityEngine;

namespace InventoryQuest.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] AudioSource _musicAudioSource;
        [SerializeField] AudioSource _sfxAudioSource;
        [SerializeField] AudioSource _uiAudioSource;

        public void PlayMusicTrack(AudioClip audioClip)
        {
            _musicAudioSource.clip = audioClip;
            _musicAudioSource.Play();
        }

        public void StopMusicTrack()
        {
            _musicAudioSource.Stop();
        }

        public void PlaySFX(AudioClip audioClip)
        {
            _sfxAudioSource.clip = audioClip;
            _sfxAudioSource.Play();
        }
    }
}
