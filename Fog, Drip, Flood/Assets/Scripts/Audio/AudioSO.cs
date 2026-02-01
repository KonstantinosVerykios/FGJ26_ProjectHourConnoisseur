using UnityEngine;

/* 
 * INFO - AudioSO
 * 
 * Very basic object that just holds a audio clip, volume and pitchVariance
 * 
 */

[System.Serializable]
public class AudioSO : IAudio
{
    public AudioClip clip;
    [Range(0, 1)] public float volume;
    [Min(0)] public float pitchVariance;

    public AudioSO(AudioClip clip, float volume, float pitchVariance)
    {
        this.clip = clip;
        this.volume = volume;
        this.pitchVariance = pitchVariance;
    }

    public AudioSO GetClip() => new AudioSO(clip, volume, pitchVariance);
}
