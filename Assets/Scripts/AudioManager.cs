using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private List<AudioSource> audioSources;
    void Awake()
    {
        audioSources = new List<AudioSource>();
        foreach(var sound in sounds)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            audioSources.Add(source);
        }
    }

    public void Play(string name)
    {
        var sources = audioSources.FindAll(source => source.clip.name == name);
        if (sources.Count == 0)
            print($"There is no sound named {name}");
        foreach (var source in sources)
            source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    [Range(0f, 1f) ] public float volume;
    [Range(-3f, 3f)] public float pitch;
}