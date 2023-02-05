using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAndLose : MonoBehaviour
{
    public AudioSource WinSound;
    public AudioSource LoseSound;
    public GameObject WinUI;
    public GameObject LoseUI;

    public void Win()
    {
        WinUI.SetActive(true);
        WinSound.Play();
    }

    public void Lose()
    {
        LoseUI.SetActive(true);
        LoseSound.Play();
    }
}
