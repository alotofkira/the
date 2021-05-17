//------------------------------------------------------------------------------
//
// File Name:	LoadSceneOnKeyPressed.cs
// Author(s):	Jeremy Kings (j.kings)
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

public class LoadSceneOnKeyPressed : MonoBehaviour
{
    public string SceneToLoad = "MainMenu";
    public KeyCode KeyToPress = KeyCode.Escape;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyToPress))
        {
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
