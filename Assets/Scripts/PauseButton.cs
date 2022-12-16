using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;
    public Image image;
    public Sprite active;
    public Sprite inactive;

    void Start()
    {
        image.sprite = GameCore.Instance.isPause ? inactive : active;
        this.pausePanel.SetActive(GameCore.Instance.isPause);
    }

    public void Toggle()
    {
        GameCore.Instance.isPause = !GameCore.Instance.isPause;
        this.pausePanel.SetActive(GameCore.Instance.isPause);
        image.sprite = GameCore.Instance.isPause ? inactive : active;
    }
}
