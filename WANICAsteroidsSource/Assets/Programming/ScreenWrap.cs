//------------------------------------------------------------------------------
//
// File Name:	ScreenWrap.cs
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

public class ScreenWrap : MonoBehaviour
{
    // Components
    private Transform mTransform = null;
    private Rigidbody2D mRigidbody = null;
    private SpriteRenderer mRenderer = null;

    // Other objects
    private Camera mCamera = null;

    // Screen Bounds
    private float cameraMinX;
    private float cameraMaxX;
    private float cameraMinY;
    private float cameraMaxY;

    // Start is called before the first frame update
    void Start()
    {
        mTransform = GetComponent<Transform>();
        mRigidbody = GetComponent<Rigidbody2D>();
        mRenderer = GetComponent<SpriteRenderer>();

        mCamera = Camera.main;

        float vertExtent = mCamera.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculate bounds
        cameraMinX = -horzExtent;
        cameraMaxX = horzExtent;
        cameraMinY = -vertExtent;
        cameraMaxY = vertExtent;

        //Debug.Log("Camera Range X: " + cameraMinX + ", " + cameraMaxX);
        //Debug.Log("Camera Range Y: " + cameraMinY + ", " + cameraMaxY);
    }

    // Update is called once per frame
    void Update()
    {
        // Detect if object is outside viewport bounds
        if (mRenderer.isVisible)
            return;

        Vector3 position = mTransform.position;

        // Wrap position
        if(position.x < cameraMinX && mRigidbody.velocity.x < 0.0f)
        {
            position.x = cameraMaxX + mTransform.localScale.x / 2.0f;
        }
        else if(position.x > cameraMaxX && mRigidbody.velocity.x > 0.0f)
        {
            position.x = cameraMinX - mTransform.localScale.x / 2.0f;
        }

        if (position.y < cameraMinY && mRigidbody.velocity.y < 0.0f)
        {
            position.y = cameraMaxY + mTransform.localScale.y / 2.0f;
        }
        else if (position.y > cameraMaxY && mRigidbody.velocity.y > 0.0f)
        {
            position.y = cameraMinY - mTransform.localScale.y / 2.0f;
        }

        // Change position
        mRigidbody.MovePosition(position);
    }
}
