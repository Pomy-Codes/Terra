using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0,1)]
    public float MasterVolume = 1;
    [Range(0, 1)]
    public float MusicVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;
    [Range(0, 1)]
    public float AmbienceVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus ambianceBus;

    public AudioSource menu;
    public AudioSource gamePlay;

    public EventInstance GamePlayMusicInstance;
    public EventInstance MenuMusicInstance;
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        ambianceBus = RuntimeManager.GetBus("bus:/Ambiance");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (scene.buildIndex != 0)
        {
            InitailizeMusic(FMODEvents.instance.GamePlayMusic);
        }
        else
        {
            InializeMenuMusic(FMODEvents.instance.MenuMusic);
        }
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Update()
    {
        masterBus.setVolume(MasterVolume);
        musicBus.setVolume(MusicVolume);
        sfxBus.setVolume(SFXVolume);
        ambianceBus.setVolume(AmbienceVolume);
    }
    private void InitailizeMusic(EventReference music)
    {
        GamePlayMusicInstance = RuntimeManager.CreateInstance(music);
        GamePlayMusicInstance.start();
    }
    private void InializeMenuMusic(EventReference music)
    {
        MenuMusicInstance = RuntimeManager.CreateInstance(music);
        MenuMusicInstance.start();
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        if(GameObject.Find("UIManager").GetComponent<UIMenu>().IsPaused == false)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }
    }
}
