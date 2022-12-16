using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Image image;
    public Sprite active;
    public Sprite inactive;

    void Start()
    {
        image.sprite = GameCore.Instance.isMute ? inactive : active;
    }

    public void Toggle()
    {
        GameCore.Instance.ToggleMute();
        image.sprite = GameCore.Instance.isMute ? inactive : active;
    }
}
