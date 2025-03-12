using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Core
{
    public class BGMManager: IDisposable
    {
        private AudioSource _bgmAudioSource;
        private float _bgmVolume;
        private readonly string _bgmBasePath = "Audio/BGM/";
    
        public BGMManager()
        {
            Initialize();
        }
        
        public void Initialize()
        {
            GameObject bgmObj = new GameObject("BGM_AudioSource");
            GameObject.DontDestroyOnLoad(bgmObj);
            _bgmAudioSource = bgmObj.AddComponent<AudioSource>();
        }
    
        public void PlayBGM(string name)
        {
            ResourceManager.Instance.ResourceLoadAsync<AudioClip>(_bgmBasePath + name, (clip) =>
            {
                _bgmAudioSource.clip = clip;
                _bgmAudioSource.volume = _bgmVolume;
                _bgmAudioSource.loop = true;
                _bgmAudioSource.Play();
            });
        }
        
        public void StopBgm() => _bgmAudioSource?.Stop();
        public void PauseBgm() => _bgmAudioSource?.Pause();
        public void ResumeBgm() => _bgmAudioSource?.Play();
        
        public void ChangeVolume(float vol)
        {
            _bgmVolume = vol;
            if (_bgmAudioSource) _bgmAudioSource.volume = vol;
        }

        public void Dispose()
        {
            CommonMono.Instance.DestroyObject(_bgmAudioSource.gameObject);
        }
    }
}

