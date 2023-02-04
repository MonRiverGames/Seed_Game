using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAndLose : MonoBehaviour
{

    public GameObject WinUI;
    public GameObject LoseUI;

    public void Win()
    {
        WinUI.SetActive(true);
    }

    public void Lose()
    {
        LoseUI.SetActive(true);
    }
}
