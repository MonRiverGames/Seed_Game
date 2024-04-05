using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* WinAndLose.cs
 Implemented by Zachary Hubbard as part of Appalachian Seed-A-Pult for 2023 Global Game Jam 
 triggers Appropriate UI Screen and Sound upon a win or lose 
 
 Win - If seed lands on ground tagged as fertile. 
 Lose - If seed lands on ground tagged as infertile or sparse.
 */

public class WinAndLose : MonoBehaviour
{
    public AudioSource WinSound; // Reference to sound played when level win
    public AudioSource LoseSound; // Reference to sound played when level win
    public GameObject WinUI; // Reference to sound played when level win
    public GameObject LoseUI; // Reference to sound played when level win

    public void Win() // If the player launches seed on fertile ground - Win
    {
        WinUI.SetActive(true); // Actives UI Screen for Win
        WinSound.Play();
    }

    public void Lose() // If the player launched the seed and lands on sparse ground - Loss
    { 
        LoseUI.SetActive(true); // Actives UI Screen for Loss
        LoseSound.Play();
    }
}
