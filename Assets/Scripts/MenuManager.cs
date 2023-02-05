using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour // Manages Menu UI button inputs and sounds
{
    public AudioSource MainMenu;
    public AudioSource Level1;
    public AudioSource Level2;
    public AudioSource Level3;

    private void Start()
    {
       if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            MainMenu.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            Level1.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            Level2.Play();
        }
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            Level3.Play();
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnQuitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

   public void LoadMainMenu()
    {   
        SceneManager.LoadScene("Main Menu");
        MainMenu.Play();
    }
}
