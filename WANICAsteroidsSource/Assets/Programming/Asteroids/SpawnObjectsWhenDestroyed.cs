//------------------------------------------------------------------------------
//
// File Name:	SpawnObjectsWhenDestroyed.cs
// Author(s):	Jeremy Kings (j.kings)
// Project:		Asteroids
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsWhenDestroyed : MonoBehaviour
{
    // Public properties
    public int MinObjectsToSpawn = 2;
    public int MaxObjectsToSpawn = 3;

    public int Index;

    // Don't spawn objects if total scale is lower than this
    public float SizeThresholdForSpawning = 0.5f;

    // Components
    private Transform mTransform = null;
    private Vector3 splitPosition;
    private Vector3 splitScale;

    // Other objects
    private ObjectSpawnManager spawnManager = null;

    // Start is called before the first frame update
    void Start()
    {
        mTransform = GetComponent<Transform>();
        splitScale = new Vector3(0.5f, 0.5f, 0.5f);
        spawnManager = FindObjectOfType<ObjectSpawnManager>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (mTransform == null)
            return;

        // Too small
        //if (mTransform.localScale.x < SizeThresholdForSpawning)
            //return;

        //var numToSpawn = UnityEngine.Random.Range(MinObjectsToSpawn, MaxObjectsToSpawn);

        //for (var i = 0; i < numToSpawn; ++i)
        //{
            //spawnManager.SpawnAtSetPosition(mTransform);
        //}

        switch(Index)
        {
            //base
            case 0:
                break;

            //speed
            case 1:
                break;

            //target
            case 2:
                break;

            //split
            case 3:
                splitPosition = GetComponent<Transform>().position;
                spawnManager.SpawnSplit(splitPosition - new Vector3(1, 0, 0), splitScale);
                spawnManager.SpawnSplit(splitPosition + new Vector3(1, 0, 0), splitScale);
                break;

        }
    }
}
