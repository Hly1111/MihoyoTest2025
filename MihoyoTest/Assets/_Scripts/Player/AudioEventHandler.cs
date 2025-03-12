using System.Collections.Generic;
using Core;
using UnityEngine;

public class AudioEventHandler : MonoBehaviour
{
    [field: SerializeField] public List<AudioClip> FootstepClips { get; private set; }
    [field: SerializeField] public List<AudioClip> IdleVoiceClips { get; private set; }
    [field: SerializeField] public List<AudioClip> ComboAttackVoiceClips { get; private set; }
    [field: SerializeField] public List<AudioClip> ShutDownAttackVoiceClips { get; private set; }
    
    
    public void PlayAudioClip(AudioClip audioClip)
    {
        AudioManager.Instance.PlaySfx(audioClip.name);
    }
    
    public void PlayFootstepClip()
    {
        AudioManager.Instance.PlaySfx(FootstepClips[Random.Range(0, FootstepClips.Count)].name);
    }
    
    public void PlayIdleVoiceClip(float percentage)
    {
        float i = Random.Range(0f, 100f);
        if (i <=  percentage)
        {
            AudioManager.Instance.PlaySfx(IdleVoiceClips[Random.Range(0, IdleVoiceClips.Count)].name);
        }
    }

    public void PlayerComboAttackVoiceClip()
    {
        AudioManager.Instance.PlaySfx(ComboAttackVoiceClips[Random.Range(0, ComboAttackVoiceClips.Count)].name);
    }
    
    public void PlayShutDownAttackVoiceClip()
    {
        AudioManager.Instance.PlaySfx(ShutDownAttackVoiceClips[Random.Range(0, ShutDownAttackVoiceClips.Count)].name);
    }
}
