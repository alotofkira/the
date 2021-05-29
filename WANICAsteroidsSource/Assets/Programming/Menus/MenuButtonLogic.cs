//------------------------------------------------------------------------------
//
// File Name:	MenuButtonLogic.cs
// Author(s):	Jeremy Kings (j.kings) & Alex Dzius (alex.dzius)
// Project:		Asteroids
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ButtonFunctions
{
    Play,
    Exit,
    Restart,
    MainMenu,
    OptionsMenu,
}

public class MenuButtonLogic : MonoBehaviour
{
    // Options for what to do when button is clicked
    public ButtonFunctions buttonFunction;

    // Function to call when button is clicked
    private delegate void ButtonAction();
    private ButtonAction buttonAction;
    private AudioSource ass;

    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>(); // this is a bug, this is a big bug, this makes audio maybe worse, but its friday 11pm and I do NOT care its a feature now
        buttonAction = null;

        switch (buttonFunction)
        {
            case ButtonFunctions.Play:
                buttonAction = Play;
                break;
            case ButtonFunctions.Exit:
                buttonAction = Exit;
                break;
            case ButtonFunctions.Restart:
                buttonAction = Restart;
                break;
            case ButtonFunctions.MainMenu:
                buttonAction = MainMenu;
                break;
            case ButtonFunctions.OptionsMenu:
                buttonAction = OptionsMenu;
                break;
        }
    }

    public void OnButtonClicked()
    {
        buttonAction();
    }

    private void Play()
    {
        ass.Play();
        SceneManager.LoadScene("GameLevel");
    }

    private void Exit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Restart()
    {
        ass.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MainMenu()
    {
        ass.Play();
        SceneManager.LoadScene("MainMenu");
    }

    private void OptionsMenu()
    {
        ass.Play();
        SceneManager.LoadScene("OptionsMenu");
    }
}
