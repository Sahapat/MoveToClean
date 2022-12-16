using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    public static GameCore Instance;
    public AudioSource ambeintSource;
    public AudioSource correctSource;
    public AudioSource incorrectSource;

    public int time = 0;
    public int correct = 0;
    public int incorrect = 0;
    public bool isMute = false;

    public NetworkSetting setting;
    void Awake()
    {
        GameCore.Instance = this;
        DontDestroyOnLoad(this);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToggleMute()
    {
        this.isMute = !this.isMute;
        ambeintSource.mute = this.isMute;
        correctSource.mute = this.isMute;
        incorrectSource.mute = this.isMute;
    }

    public void PlayCorrect()
    {
        this.correctSource.Play();
    }

    public void PlayInCorrect()
    {
        this.incorrectSource.Play();
    }
}
