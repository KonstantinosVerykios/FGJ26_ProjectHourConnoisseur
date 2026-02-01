using UnityEngine;

public class AudioContainerSource : MonoBehaviour
{
    public AudioContainer audioContainer;

    [SerializeField] private bool loop = false;

    private AudioSource audioSource => GetComponent<AudioSource>();

    public void StartPlaying()
    {
        loop = true;
        PlayNewAudioClip();
    }

    public void StopPlaying() => loop = false;

    // not optimised. Optimising will save a few nanoseconds. I aint doing that, nor will you!
    private void Update()
    {
        // If we shouldn't be playing don't check
        if (!loop)
            return;

        // if we already playing don't do nothing extra
        if (audioSource.isPlaying)
            return;

        // we only get here when we should be playing audio but nothing is playing
        PlayNewAudioClip();
    }

    private void PlayNewAudioClip()
    {
        if (audioContainer == null)
        {
            Debug.LogWarning("AudioContainerSource: trying to play audio but audioContainer is null!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioContainerSource: No AudioSource component found!");
            return;
        }

        AudioSO newAudio = audioContainer.GetClip();

        if (newAudio == null || newAudio.clip == null)
        {
            Debug.LogError("AudioContainerSource: No audio clip to play!");
            return;
        }

        audioSource.resource = newAudio.clip;
        audioSource.volume = newAudio.volume;


        float randomPitch = Random.Range(-newAudio.pitchVariance, newAudio.pitchVariance);
        audioSource.pitch = 1f+randomPitch;

        audioSource.Play();

        if (!audioSource.isPlaying)
        {
            Debug.LogError("AudioSource.Play() was called but audio is not playing!");
        }
    }
}
