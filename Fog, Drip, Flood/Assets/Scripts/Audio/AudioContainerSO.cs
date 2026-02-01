using System.Collections.Generic;
using UnityEngine;

/* 
 * INFO - AudioContainer
 * 
 * Works as a basic ScriptableObject for holding an array of AudioClips
 * 
 */

[System.Serializable]
public class AudioContainer : IAudio
{
    public AudioClip[] audioClips;
    [Range(0,1)]public float volume = 1;
    public float pitchVariance = 0;

    private int arrayLength => audioClips.Length;

    public AudioSO GetClip() => GetRandomAudio();

    // Get random clip from array
    private AudioSO GetRandomAudio()
    {
        int rnd = Random.Range(0, arrayLength);
        return new AudioSO(audioClips[rnd], volume, pitchVariance);
    }
}
