//------------------------------------------------------------------------------
//
// File Name:	DestroyOnCollision.cs
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

public class DestroyOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HighScoreHolder.totalKills++; 
        Destroy(gameObject);
    }
}
