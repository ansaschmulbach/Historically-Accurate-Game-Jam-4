using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField] public Sound[] sounds;
    public static AudioManager instance;
    
    [Serializable]
    public class Sound
    {
        [NonSerialized] public AudioSource source;
        public string name;
        public AudioClip clip;
        [Range(0, 1f)] public float volume;
        public bool loop;
        public bool canOverlap;
    }
    
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) Debug.LogWarning("no sound with name " + name);
        if (!s.canOverlap && s.source.isPlaying) return;
        s.source.Play();
    }
    
    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }


    public IEnumerator WaitForPlay(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) Debug.LogWarning("no sound with name " + name);
        s.source.Play();
        yield return new WaitWhile(() => s.source.isPlaying);
    }

    
}
