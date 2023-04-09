using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePressMultiplayer : MonoBehaviour
{
    public AudioSource buttonclick;
    public AudioClip buttonclip;

    public void ChangeToMultiplayer(bool isOn)
    {
        buttonclick.PlayOneShot(buttonclip);
        GameManager.instance.isMultiplayer = isOn;
    }
}
