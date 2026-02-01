using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioSourcePositions { Paddle_L, Paddle_R, Player}

    [Header("AudioSO and -Containers")]
    [SerializeField] private AudioLibrarySO audioLibrary;

    [Header("AudioSources")]
    [SerializeField] private AudioContainerSource leftPaddleAudioSource;
    [SerializeField] private AudioContainerSource rightPaddleAudioSource;
    [SerializeField] private AudioContainerSource playerCharacterAudioSource;
    [SerializeField] private AudioSource cameraAudioSource;
    [SerializeField] private AudioSource ambienceAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupAmbience();
        SetupPaddles();
    }

    private void SetupAmbience()
    {
        AudioContainer audioContainer = audioLibrary.GetAudioContainer("Ambience");
        AudioSO audio = audioContainer.GetClip();

        ambienceAudioSource.clip = audio.clip;
        ambienceAudioSource.volume = audio.volume;
        ambienceAudioSource.volume = 1;
        ambienceAudioSource.loop = true;
        ambienceAudioSource.spatialBlend = 0;

        ambienceAudioSource.Play();
    }

    private void SetupPaddles()
    {
        AudioContainer audioContainer = audioLibrary.GetAudioContainer("Paddle");
        leftPaddleAudioSource.audioContainer = audioContainer;
        rightPaddleAudioSource.audioContainer = audioContainer;
        leftPaddleAudioSource.StartPlaying();
        rightPaddleAudioSource.StartPlaying();
    }

    public void PlayAt(AudioSourcePositions AudioSourcePosition, AudioSO audio)
    {
        AudioContainerSource audioSource = GetAudioSource(AudioSourcePosition);

    }

    private AudioContainerSource GetAudioSource(AudioSourcePositions AudioSourcePosition)
        => AudioSourcePosition switch
        {
            AudioSourcePositions.Paddle_L => leftPaddleAudioSource,
            AudioSourcePositions.Paddle_R => rightPaddleAudioSource,
            AudioSourcePositions.Player => playerCharacterAudioSource,
            _ => null
        };
}
