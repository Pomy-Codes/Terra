using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using FMODUnity;

public class UIMenu : MonoBehaviour
{
    public bool IsPaused;

    [Header("Panels")]
    public GameObject PauseMenu;
    public GameObject Transition;

    [Header ("Components")]
    public GameObject fullscreenT;
    private Animator TransAnim;

    [Header("Gameplay")]
    public GameObject Resume;
    public GameObject Setting;
    public GameObject Menu;
    public GameObject Back_Q;

    [Header("Main Menu")]
    public GameObject Tutorial;
    public GameObject Game;
    public GameObject Back_;

    public GameObject Play_;
    public GameObject Options;
    public GameObject Quit;

    public GameObject Volume;
    public GameObject Master;
    public GameObject Music;
    public GameObject SFX;
    public GameObject Ambience;
    public GameObject Back_O;

    [Header("Dead Pigs")]
    public GameObject[] Pigs;
    public GameObject[] GoldenPigs;
    public SpriteRenderer Background;
    public SpriteRenderer Title;
    public GameObject Thanks;

    public void SetFullScreen()
    {
        fullscreenT.SetActive(false);
        Screen.fullScreen = true;
    }
    
    #region Fading
    IEnumerator FadeIn()
    {
        Transition.SetActive(true);
        TransAnim.SetTrigger("FadeIn");
        yield return new WaitForSecondsRealtime(1f);
        Transition.SetActive(false);
    }
    IEnumerator FadeOut()
    {
        Transition.SetActive(true);
        TransAnim.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(1.5f);
    }


    #endregion

    #region Buttons
    public void ResetMusic()
    {
        AudioManager.instance.GamePlayMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        AudioManager.instance.MenuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        AudioManager.instance.MenuMusicInstance.start();
    }
    private void Start()
    {
        if(GameManager.instance.finished == true)
        {
            Thanks.SetActive(true);
        }
        TransAnim = Transition.GetComponent<Animator>();
        StartCoroutine(FadeIn());
        DeadPigDisplay();
        PitchChange();
        if(Screen.fullScreen == false)
        {
            fullscreenT.SetActive(true);
        }
        else
        {
            fullscreenT.SetActive(false);
        }
    }
    private void PitchChange()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            if (GameManager.instance.PigsDead + GameManager.instance.GoldenPigsDead > 4 && GameManager.instance.PigsDead <= 8)
            {
                Background.color = new Color(142, 142, 142);
            }
            else if ((GameManager.instance.PigsDead + GameManager.instance.GoldenPigsDead) > 8)
            {
                Background.color = new Color(0, 0, 0);
            }

        }
        
        
    }
    private void DeadPigDisplay()
    {
        try
        {
            for (int i = 0; i < GameManager.instance.PigsDead; i++)
            {
                if (i <= Pigs.Length)
                {
                    Pigs[i].SetActive(true);
                }
            }
            for (int i = 0; i < GameManager.instance.GoldenPigsDead; i++)
            {
                if (i <= GoldenPigs.Length)
                {
                    GoldenPigs[i].SetActive(true);
                }
            }
        }
        catch
        {
            Debug.Log("No GameManager");
        }

    }
    public void MenuButton()
    {
        AudioManager.instance.GamePlayMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(OnMenu());
        UnPause();
        SceneManager.LoadScene(0);
    }
    IEnumerator OnMenu()
    {
        StartCoroutine(FadeOut());
        yield return new WaitForSecondsRealtime(1.5f);
    }
    public void QuitButton()
    {
        Screen.fullScreen = false;
        Application.Quit();
    }
    public void PlayLevel()
    {
        StartCoroutine(OnLevel());
    }
    IEnumerator OnLevel()
    {
        AudioManager.instance.MenuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(FadeOut());
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(2);

    }
    public void QuickSettings()
    {
        StartCoroutine(QuickSettingsOptions());
    }
    public void QuickSettingsBack()
    {
        StartCoroutine(QuickSettingsOptionsBack());
    }   
    public void Play()
    {
        StartCoroutine(PlayOptions());
    }
    public void PlayBack()
    {
        StartCoroutine(PlayOptionsBack());
    }
    public void Settings()
    {
        StartCoroutine(SettingsOptions());
    }
    public void SettingsBack()
    {
        StartCoroutine(SettingsOptionsBack());
    }
    public void OnDead()
    {
        StartCoroutine(OnDeath());
    }
    public void OnFinishTutorial()
    {
        StartCoroutine(OnTutorialEnd());    
    }
    IEnumerator OnTutorialEnd()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameManager.instance.finished = true;
        }
        AudioManager.instance.GamePlayMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(FadeOut());
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(0);
    }
    IEnumerator OnDeath()
    {
        StartCoroutine(FadeOut());
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    IEnumerator PlayOptions()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Play_.SetActive(false);
        Tutorial.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Options.SetActive(false);
        Game.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Quit.SetActive(false);
        Back_.SetActive(true);
    }
    IEnumerator PlayOptionsBack()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Quit.SetActive(true);
        Back_.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Options.SetActive(true);
        Game.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Play_.SetActive(true);
        Tutorial.SetActive(false);
    }
    IEnumerator SettingsOptions()
    {
        Volume.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Play_.SetActive(false);
        Master.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Options.SetActive(false);
        Music.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Quit.SetActive(false);
        SFX.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Back_O.SetActive(true);
    }
    IEnumerator SettingsOptionsBack() {
        Back_O.SetActive(false);
        Quit.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Options.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Play_.SetActive(true);
        SFX.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Music.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Master.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Volume.SetActive(false);
    }
    IEnumerator QuickSettingsOptionsBack()
    {
        Back_O.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        SFX.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Options.SetActive(true);
        Music.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Play_.SetActive(true);
        Master.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Quit.SetActive(true);
        Volume.SetActive(false);
    }
    IEnumerator QuickSettingsOptions()
    {
        Volume.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Play_.SetActive(false);
        Master.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Options.SetActive(false);
        Music.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Quit.SetActive(false);
        SFX.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        Back_O.SetActive(true);
    }
    IEnumerator StartGame()
    {
        StartCoroutine(FadeOut());
        AudioManager.instance.MenuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(1);
    }   
    public void BeginTutorial()
    {
        StartCoroutine(StartGame());
    }

    public void Replay()
    {
        UnPause();
    }
    public void Back()
    {
        UnPause();

        StartCoroutine(FadeOut());
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        UnPause();

        SceneManager.LoadScene(1);
    }

    #endregion
    #region Inputs
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused)
        {
            UnPause();
        }
    }
    #endregion 
    #region Pauses
    private void Pause()
    {
        //Stopping Time and enabling Pause Menu

        PauseMenu.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0f;
    }
    public void UnPause()
    {
        IsPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    #endregion
}
