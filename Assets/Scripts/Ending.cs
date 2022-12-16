using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public InputField nameInput;
    public InputField emailInput;

    public void Submit()
    {
        this.SendGameScore().Forget();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }

    private async UniTask SendGameScore()
    {
        await GameCore.Instance.SendScore(new ScorePostRequest
        {
            correct = GameCore.Instance.correct,
            incorrect = GameCore.Instance.incorrect,
            email = emailInput.text,
            name = nameInput.text,
            time = GameCore.Instance.time
        });
        SceneManager.LoadScene("Scoreboard");
    }
}
