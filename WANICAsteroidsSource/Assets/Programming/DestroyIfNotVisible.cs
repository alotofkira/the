//------------------------------------------------------------------------------
//
// File Name:	DestroyIfNotVisible.cs
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

public class DestroyIfNotVisible : MonoBehaviour
{
    private SpriteRenderer mRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect if object is outside viewport bounds
        if (mRenderer.isVisible)
            return;

        // Destroy the object
        Destroy(gameObject);
    }
}
