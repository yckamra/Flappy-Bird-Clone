using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{

    public static OptionsScript Instance; // Singleton pattern

    public float SFXVolume;
    public int musicVolume;
    public AudioSource music;
    public List<AudioClip> playlist;
    public Queue<AudioClip> playlistQueue = new Queue<AudioClip>();
    public AudioSource casette;

    // Start is called before the first frame update
    void Start()
    {

        SFXVolume = 0.5f;
        musicVolume = 50;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        putListInQueue();

        if (playlistQueue.Count > 0)
        {
            PlayNextClip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        casette.volume = musicVolume;
        if (!music.isPlaying && playlistQueue.Count > 0)
        {
            PlayNextClip();
        }
    }
    void PlayNextClip()
    {
        casette.Play();
        AudioClip theClip = playlistQueue.Dequeue();
        music.clip = theClip;
        playlistQueue.Enqueue(theClip);
        music.Play();
    }
    void putListInQueue()
    {
        foreach(AudioClip song in playlist)
        {
            playlistQueue.Enqueue(song);
        }
    }
    public void changeMusicVolume(Slider musicSlider)
    {
        musicVolume = (int)musicSlider.value;
        Debug.Log(musicVolume);
        music.volume = (float)musicVolume / 100;
    }
    public void changeSFXVolume(Slider SFXSlider)
    {
        SFXVolume = SFXSlider.value / 100;
        Debug.Log(SFXVolume);
    }
}
