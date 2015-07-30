using UnityEngine;
using System.Collections;

public class UIEvent : MonoBehaviour 
{

    public void Retry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ToTitle()
    {
        Application.LoadLevel("Title");
    }

    public void ToGame()
    {
        Application.LoadLevel("Game");
    }
}
