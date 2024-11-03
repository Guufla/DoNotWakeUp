using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool musicEnabled = true;
    public AudioSource ambientMusicSource;

    int numOfSounds;

    bool timeSet;
    float timeTillNextSound;

    [Header("Min and Max Time between sounds")]
    [SerializeField] int minSeconds;
    [SerializeField] int maxSeconds;


    public static AudioManager Instance;

    void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            if (s.place)
            {
                s.source = s.place.AddComponent<AudioSource>();
            }
            else
            {
                s.source = gameObject.AddComponent<AudioSource>();
            }
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        if (musicEnabled)
        {
            ambientMusicSource.Play();
        }

        timeSet = false;

        numOfSounds = sounds.Length;
    }

    void Update(){
        if(timeSet == false){
            timeTillNextSound = UnityEngine.Random.Range(minSeconds,maxSeconds);
            timeSet = true;
            Debug.Log("time till next sound" + timeTillNextSound);
        }
    }
    void FixedUpdate(){

        if(timeSet == true && timeTillNextSound <= 1f){
            Debug.Log("Playing a sound");
            int indexOfSound = UnityEngine.Random.Range(0,numOfSounds);
            Debug.Log(sounds[indexOfSound].name);
            PlaySound(sounds[indexOfSound].name);

            timeTillNextSound = 500f; // Weird but it works so leave it fr

            timeSet = false;
        }

        else if(timeSet == true && timeTillNextSound > 1f){
            timeTillNextSound -= Time.deltaTime;
        }
    
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source.isPlaying)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not playing!");
        }
    }
}
