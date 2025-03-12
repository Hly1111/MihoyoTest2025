using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    /// <summary>
    /// DESIGN PATTERN: Singleton, Facade, Abstract Factory, Composite
    /// </summary>
    public class AudioManager : Singleton<AudioManager>, IDisposable
    {
        private readonly BGMManager _bgmManager;
        private readonly SFXManager _sfxManager;
        
        public AudioManager()
        {
            _bgmManager = new BGMManager();
            _sfxManager = new SFXManager();
        }
        
        public void Initialize()
        {
            _bgmManager.Initialize();
            _sfxManager.Initialize();
        }
        
        public void PlayBgm(string name) => _bgmManager.PlayBGM(name);
        public void StopBgm() => _bgmManager.StopBgm();
        public void PauseBgm() => _bgmManager.PauseBgm();
        public void ResumeBgm() => _bgmManager.ResumeBgm();
        public void ChangeBgmVolume(float vol) => _bgmManager.ChangeVolume(vol);
        
        public void PlaySfx(string name, bool loop = false, UnityAction<AudioSource> callback = null)
            => _sfxManager.PlaySfx(name, loop, callback);
        public void PlaySfxScheduled(double time, string name, bool loop = false, UnityAction<AudioSource> callback = null)
            => _sfxManager.PlaySfxScheduled(time, name, loop, callback);
        public void StopSfx(AudioSource source) => _sfxManager.StopSfx(source);
        public void ChangeSfxVolume(float vol) => _sfxManager.ChangeVolume(vol);
        public void Dispose()
        {
            _bgmManager.Dispose();
            _sfxManager.Dispose();
        }
    }
}

