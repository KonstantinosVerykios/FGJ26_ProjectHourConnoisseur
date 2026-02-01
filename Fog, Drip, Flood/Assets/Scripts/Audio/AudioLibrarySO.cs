using System.Collections.Generic;
using UnityEngine;

/*
 * INFO
 * 
 * Used so that holding a lot of audio is easy :)
 * 
 */

[CreateAssetMenu(fileName = "AudioLibrarySO", menuName = "Scriptable Objects/AudioLibrarySO")]
[System.Serializable]
public class AudioLibrarySO : ScriptableObject
{
    [System.Serializable]
    public class AudioContainerWrapper
    {
        public string name;
        public AudioContainer audioContainer;
    }

    // This is used for inserting elements in inspector
    [SerializeField] private List<AudioContainerWrapper> list = new List<AudioContainerWrapper>();

    // this is the one actually used in code
    public Dictionary<string, AudioContainer> dictionary = new Dictionary<string, AudioContainer>();

    public AudioContainer GetAudioContainer(string name)
    {
        AudioContainer correctContainer;
        if (dictionary.TryGetValue(name, out correctContainer))
        {
            return correctContainer;
        }
        else
        {
            Debug.LogWarning("O-oh. Something broke in AudioLibray. COuldn't get correct AudioContainer");
            return null;
        }
    }

    private void OnEnable()
    {
        // turn the list into a dictionary for ease
        list.ForEach(item => dictionary[item.name] = item.audioContainer);
    }
}
