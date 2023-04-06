using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePressMap : MonoBehaviour
{
    public AudioSource buttonclick;
    public AudioClip buttonclip;
    public MapGenerator generator;
    public void ChangeToMapOfTheDay(bool isOn)
    {
        buttonclick.PlayOneShot(buttonclip);
        generator.isMapOfTheDay = isOn;
    }
}
