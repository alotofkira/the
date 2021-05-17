//------------------------------------------------------------------------------
//
// File Name:	ObjectSpawnManager.cs
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

public class ObjectSpawnManager : MonoBehaviour
{
    // Public properties
    public GameObject SpawnedObjectPrefab = null;
    public float MinObjectSpeed = 0.5f;
    public float MaxObjectSpeed = 1.5f;

    // Largest asteroid size
    public float BaseObjectSize = 2.0f;
    public float SpawnScaleFactor = 0.6f;

    // Waves
    public int MaxWaveSize = 20;
    public int StartingWaveSize = 8;

    // Other objects
    private Camera mCamera;

    // Misc
    private int currentWaveSize = 0;

    // Screen Bounds
    private float cameraMinX;
    private float cameraMaxX;
    private float cameraMinY;
    private float cameraMaxY;

    private enum SpawnCorner
    {
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight,
    }

    private enum SizeCategory
    {
        Large,
        Medium,
        Small
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWaveSize = StartingWaveSize;

        mCamera = Camera.main;
        float vertExtent = mCamera.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculate bounds
        cameraMinX = -horzExtent;
        cameraMaxX = horzExtent;
        cameraMinY = -vertExtent;
        cameraMaxY = vertExtent;
    }

    // Update is called once per frame
    void Update()
    {
        // Check current number of asteroids
        int numAsteroids = GameObject.FindGameObjectsWithTag("Asteroid").Length;

        if(numAsteroids == 0)
        {
            SpawnNextWave();
        }
    }

    void SpawnAtRandomPosition()
    {
        // Calculate asteroid size
        var spawnScale = CalculateSpawnScale();

        // Find corner outside bounds
        var spawnPosition = CalculateSpawnPosition(spawnScale);

        // Spawn at position with base scale
        SpawnAtSetPosition(spawnPosition, spawnScale);
    }

    private Vector3 CalculateSpawnScale()
    {
        var sizeCategory = (SizeCategory)Random.Range((int)SizeCategory.Small, (int)SizeCategory.Large);
        var spawnSize = BaseObjectSize;

        switch(sizeCategory)
        {
            case SizeCategory.Large:
                break;
            case SizeCategory.Medium:
                spawnSize *= SpawnScaleFactor;
                break;
            case SizeCategory.Small:
                spawnSize *= SpawnScaleFactor * SpawnScaleFactor;
                break;
        }

        var spawnScale = SpawnedObjectPrefab.transform.localScale * spawnSize;
        return spawnScale;
    }

    private Vector3 CalculateSpawnPosition(Vector3 spawnScale)
    {
        var spawnExtents = spawnScale * 0.5f;

        // Find corner outside bounds
        var spawnPosition = new Vector3();
        var corner = (SpawnCorner)Random.Range((int)SpawnCorner.LowerLeft, (int)SpawnCorner.UpperRight);
        switch (corner)
        {
            case SpawnCorner.LowerLeft:
                spawnPosition.x = cameraMinX - spawnExtents.x;
                spawnPosition.y = cameraMinY - spawnExtents.y;
                break;
            case SpawnCorner.LowerRight:
                spawnPosition.x = cameraMaxX + spawnExtents.x;
                spawnPosition.y = cameraMinY - spawnExtents.y;
                break;
            case SpawnCorner.UpperLeft:
                spawnPosition.x = cameraMinX - spawnExtents.x;
                spawnPosition.y = cameraMaxY + spawnExtents.y;
                break;
            case SpawnCorner.UpperRight:
                spawnPosition.x = cameraMaxX + spawnExtents.x;
                spawnPosition.y = cameraMaxY + spawnExtents.y;
                break;
        }

        return spawnPosition;
    }

    public void SpawnAtSetPosition(Transform transform)
    {
        var spawnScale = transform.localScale * SpawnScaleFactor;
        SpawnAtSetPosition(transform.position, spawnScale);
    }

    public void SpawnAtSetPosition(Vector3 position, Vector3 scale)
    {
        // Create object
        var spawnedObject = Instantiate(SpawnedObjectPrefab, position, Quaternion.identity);

        // Set object size
        spawnedObject.transform.localScale = scale;

        // Random rotation
        var rotation = Random.Range(0.0f, 360.0f);
        spawnedObject.transform.eulerAngles = new Vector3(0, 0, rotation);

        // Random speed
        var speed = Random.Range(MinObjectSpeed, MaxObjectSpeed);

        // Random move direction
        rotation = Random.Range(0.0f, 360.0f * Mathf.Deg2Rad);
        var direction = new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0);

        // Move object
        var body = spawnedObject.GetComponent<Rigidbody2D>();
        body.velocity = direction * speed;
    }

    void SpawnNextWave()
    {
        for(var i = 0; i < currentWaveSize; ++i)
        {
            SpawnAtRandomPosition();
        }

        // Increase wave size if below max
        currentWaveSize = currentWaveSize < MaxWaveSize ? currentWaveSize + 1 : currentWaveSize;
    }
}
