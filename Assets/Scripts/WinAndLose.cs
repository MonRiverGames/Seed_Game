using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WinAndLose.cs
// Created by Zachary Hubbard
// Triggers Appropriate UI Screen and Sound upon a win or lose
public class WinAndLose : MonoBehaviour // Plays sound and activates appropriate UI on Win or Lose
{
    public AudioSource WinSound; // Reference to sound played when level win
    public AudioSource LoseSound; // Reference to sound played when level win
    public GameObject WinUI; // Reference to sound played when level win
    public GameObject LoseUI; // Reference to sound played when level win

    public void Win() // If the player launches seed on fertile ground - Win
    {
        WinUI.SetActive(true);
        WinSound.Play();
    }

    public void Lose() // If the player launched the seed and lands on sparse ground - Loss
    {
        LoseUI.SetActive(true);
        LoseSound.Play();
    }
}
