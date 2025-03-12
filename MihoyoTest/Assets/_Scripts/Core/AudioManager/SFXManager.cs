using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class SFXManager: IDisposable
    {
        private readonly List<AudioSource> _activeSfx = new List<AudioSource>();
        private float _sfxVolume = 1f;
        private readonly string _sfxBasePath = "Audio/SFX/";
        private readonly string _audioSourcePrefabPath = "AudioSource";

        public SFXManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            CommonMono.Instance.AddUpdate(SfxUpdate);
        }

        private void SfxUpdate()
        {
            for (int i = _activeSfx.Count - 1; i >= 0; i--)
            {
                if (!_activeSfx[i].isPlaying)
                {
                    ObjectPool.Instance.ReturnObject(_activeSfx[i].gameObject.name, _activeSfx[i].gameObject);
                    _activeSfx.RemoveAt(i);
                }
            }
        }
        
        public void PlaySfx(string name, bool loop = false, UnityAction<AudioSource> callback = null)
        {
            ResourceManager.Instance.ResourceLoadAsync<AudioClip>(_sfxBasePath + name, (clip) =>
            {
                ObjectPool.Instance.GetObject(_audioSourcePrefabPath, (obj) =>
                {
                    AudioSource audioSource = obj.GetComponent<AudioSource>();
                    audioSource.clip = clip;
                    audioSource.volume = _sfxVolume;
                    audioSource.loop = loop;
                    _activeSfx.Add(audioSource);
                    audioSource.Play();
                    callback?.Invoke(audioSource);
                });
            });
        }

        public void PlaySfxScheduled(double time, string name, bool loop = false, UnityAction<AudioSource> callback = null)
        {
            ResourceManager.Instance.ResourceLoadAsync<AudioClip>(_sfxBasePath + name, (clip) =>
            {
                ObjectPool.Instance.GetObject(_audioSourcePrefabPath, (obj) =>
                {
                    AudioSource audioSource = obj.GetComponent<AudioSource>();
                    audioSource.clip = clip;
                    audioSource.volume = _sfxVolume;
                    audioSource.loop = loop;
                    _activeSfx.Add(audioSource);
                    audioSource.PlayScheduled(time);
                    callback?.Invoke(audioSource);
                });
            });
        }
        
        public void StopSfx(AudioSource audioSource)
        {
            if (_activeSfx.Contains(audioSource))
            {
                audioSource.Stop();
                ObjectPool.Instance.ReturnObject(audioSource.gameObject.name, audioSource.gameObject);
                _activeSfx.Remove(audioSource);
            }
        }
        
        public void ChangeVolume(float vol)
        {
            _sfxVolume = vol;
            foreach (var audioSource in _activeSfx)
            {
                audioSource.volume = vol;
            }
        }

        public void Dispose()
        {
            CommonMono.Instance.RemoveUpdate(SfxUpdate);
        }
    }
}

