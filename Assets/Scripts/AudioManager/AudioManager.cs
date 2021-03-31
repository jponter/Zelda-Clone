using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource soundSource;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource music2Source;
    //[SerializeField] private AudioSource music3Source;

    //private static AudioManager instance;

    [SerializeField] private string introBGMusic;
    [SerializeField] private string levelBGMusic;
    [SerializeField] private string dungeonBGMusic;

    [SerializeField] private string swordSwing;

    public ManagerStatus status { get; private set; }

    private NetworkService _network;

    private string sound;

    private AudioSource _activeMusic;
    private AudioSource _inactiveMusic;

    public float crossFadeRate = 1.5f;
    private bool _crossFading;

    //private bool _firstPlay = true;

    //10.4 goes here
    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public float _musicVolume;

    public float musicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            if (musicSource != null && !_crossFading)
            {
                musicSource.volume = _musicVolume;
                music2Source.volume = _musicVolume;
                //music3Source.volume = _musicVolume;
            }
        }
    }

    public void Start()
    {
        GameEvents.instance.onSoundEvent += PlaySoundFX;
    }

    public void OnDisable()
    {
        GameEvents.instance.onSoundEvent -= PlaySoundFX ;
    }

    public bool musicMute
    {
        get
        {
            if (musicSource != null)
            {
                return musicSource.mute;
            }
            return false;
        }

        set
        {
            if (musicSource != null)
            {
                musicSource.mute = value;
                music2Source.mute = value;
                //music3Source.mute = value;
            }
        }
    }



    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public void Startup(NetworkService service)
    {
        _network = service;

        soundVolume = 1.0f;
        musicVolume = 1.0f;

        //init music sources here 10.10
        musicSource.ignoreListenerVolume = true;
        musicSource.ignoreListenerPause = true;
        music2Source.ignoreListenerPause = true;
        music2Source.ignoreListenerVolume = true;

        _activeMusic = musicSource;
        _inactiveMusic = music2Source;

        status = ManagerStatus.Started;

       

    }

    public void PlaySound(AudioClip clip, float vol = 0.7f)
    {
        soundSource.PlayOneShot(clip, vol);
    }

    public void PlaySoundFX(string sfx, float volumeScale = 0.7f)
    {
        soundSource.PlayOneShot(Resources.Load("SFX/" + sfx) as AudioClip, volumeScale);
    }

    public void playSound(string sound)
    {
        if (sound != null)
        {
            Debug.Log("PlaySound Event: Sound = " + sound);
            soundSource.PlayOneShot(Resources.Load("SFX/" + sound) as AudioClip, 1.0f);
        }
    }

    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load("Music/" + introBGMusic) as AudioClip);
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load("Music/" + levelBGMusic) as AudioClip);
    }

    public void PlayDungeonMusic()
    {
        PlayMusic(Resources.Load("Music/" + dungeonBGMusic) as AudioClip);
    }



    public void PlayMusic(AudioClip clip)
    {
        

        if (_crossFading) { return; }
        StartCoroutine(CrossFadeMusic(clip));
        //musicSource.clip = clip;
        //musicSource.Play();
    }

    public IEnumerator CrossFadeMusic(AudioClip clip)
    {
        _crossFading = true;
        _inactiveMusic.clip = clip;
        _inactiveMusic.volume = 0;
        _inactiveMusic.Play();

        float scaledRate = crossFadeRate * _musicVolume;
        while (_activeMusic.volume > 0)
        {
            _activeMusic.volume -= scaledRate * Time.deltaTime;
            _inactiveMusic.volume += scaledRate * Time.deltaTime;

            yield return null;
        }

        AudioSource temp = _activeMusic;

        _activeMusic = _inactiveMusic;
        _activeMusic.volume = musicVolume;

        _inactiveMusic = temp;
        _inactiveMusic.Stop();

        _crossFading = false;
    }

    public void StopMusic()
    {
        _activeMusic.Stop();
        _inactiveMusic.Stop();
    }
}
