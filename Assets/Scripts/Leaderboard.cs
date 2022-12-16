using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    public Text[] player1;
    public Text[] player2;
    public Text[] player3;

    public List<Text[]> playerList = new List<Text[]>();
    void Awake()
    {
        playerList.Add(player1);
        playerList.Add(player2);
        playerList.Add(player3);
    }

    void Start()
    {
        this.FetchLeaderboard().Forget();
    }

    public async UniTask FetchLeaderboard()
    {
        await GameCore.Instance.FetchScore();
        for (int i = 0; i < GameCore.Instance.scores.Length; i++)
        {
            var player = playerList[i];
            var score = GameCore.Instance.scores[i];
            player[0].text = score.rank.ToString();
            player[1].text = score.time.ToString("0.00");
            player[2].text = score.name;
        }
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
