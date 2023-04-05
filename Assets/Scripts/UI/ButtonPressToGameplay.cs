using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressToGameplay : MonoBehaviour
{
    public void ChangeToGameplay()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateGameplay();
        }
    }
}
