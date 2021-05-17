//------------------------------------------------------------------------------
//
// File Name:	PlayerMovementController.cs
// Author(s):	Jeremy Kings (j.kings)
// Project:		Asteroids
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // Public properties
    public float MoveSpeedMax = 5.0f;
    public float RotateSpeed = 90.0f;
    public float AccelerationAmount = 0.1f;
    public float DecelerationAmount = 0.01f;
    // If true, player gradually moves faster.
    // If false, player immediately reaches max speed.
    public bool AccelerationEnabled = true;
    // If true, player gradually slows down
    // If false, player stops immediately.
    public bool DecelerationEnabled = true;

    // Input properties
    public KeyCode MoveForwardKey = KeyCode.W;
    public KeyCode TurnRightKey = KeyCode.D;
    public KeyCode TurnLeftKey = KeyCode.A;

    // Components
    private Rigidbody2D mRigidbody = null;
    private Transform mTransform = null;
    private AudioSource mAudioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        mTransform = GetComponent<Transform>();
        mAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        // Accelerate in move direction
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(mTransform.eulerAngles.z * Mathf.Deg2Rad),
            Mathf.Sin(mTransform.eulerAngles.z * Mathf.Deg2Rad));

        // Accelerate when forward key is held
        if(Input.GetKey(MoveForwardKey))
        {
            if (AccelerationEnabled)
                mRigidbody.velocity += moveDirection * AccelerationAmount * Time.deltaTime;
            else
                mRigidbody.velocity = moveDirection * MoveSpeedMax;
            
            if(!mAudioSource.isPlaying)
                mAudioSource.Play();
        }
        // Otherwise, decelerate
        else
        {
            if (DecelerationEnabled)
                mRigidbody.velocity -= mRigidbody.velocity.normalized * DecelerationAmount * Time.deltaTime;
            else
                mRigidbody.velocity = Vector2.zero;

            mAudioSource.Stop();
        }

        // Clamp upper speed
        float speed = mRigidbody.velocity.magnitude;
        if (AccelerationEnabled && speed > MoveSpeedMax)
        {
            mRigidbody.velocity = MoveSpeedMax * (mRigidbody.velocity / speed);
        }
        // Clamp lower speed
        else if(DecelerationEnabled && !Input.GetKey(MoveForwardKey) && Mathf.Approximately(speed, 0.0f))
        {
            mRigidbody.velocity = Vector2.zero;
        }
    }

    private void Rotate()
    {
        float turnDirection = 0;

        if (Input.GetKey(TurnRightKey))
        {
            turnDirection -= 1;
        }
        
        if(Input.GetKey(TurnLeftKey))
        {
            turnDirection += 1;
        }

        mRigidbody.angularVelocity = turnDirection * RotateSpeed;
    }
}
