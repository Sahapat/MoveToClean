using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ScorePostRequest
{
    public string name;
    public string email;
    public float time;
    public int correct;
    public int incorrect;
}

[SerializeField]
public class ScorePostResponse { }

[SerializeField]
public class GameScore
{
    public string name;
    public int rank;
    public float time;
}

[SerializeField]
public class FetchGameScore
{
    public GameScore[] scores;
}

public class GameCore : MonoBehaviour
{
    public static GameCore Instance;
    public AudioSource ambeintSource;
    public AudioSource correctSource;
    public AudioSource incorrectSource;

    public GameScore[] scores = new GameScore[0];

    public float time = 0;
    public int correct = 0;
    public int incorrect = 0;
    public bool isMute = false;
    public bool isPause = false;

    public NetworkSetting setting;
    public NetworkRestService networkRestService = new NetworkRestService(true);
    void Awake()
    {
        GameCore.Instance = this;
        DontDestroyOnLoad(this);
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

    public UniTask SendScore(ScorePostRequest scorePostRequest)
    {
        return this.networkRestService.Post<ScorePostResponse>(setting.PostScoreUrl, "", scorePostRequest, new Dictionary<string, string>());
    }

    public async UniTask FetchScore()
    {
        var response = await this.networkRestService.Get<FetchGameScore>(setting.FetchLeaderboardUrl, "");
        this.scores = response.scores;
    }
}
