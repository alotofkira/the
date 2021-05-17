//------------------------------------------------------------------------------
//
// File Name:	DestroyIfTimeElapsed.cs
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

public class DestroyIfTimeElapsed : MonoBehaviour
{
    public float TimeUntilDestroyed = 1.0f;
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > TimeUntilDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
