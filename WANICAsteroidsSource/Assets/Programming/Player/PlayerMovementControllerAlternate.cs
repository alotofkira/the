//------------------------------------------------------------------------------
//
// File Name:	PlayerMovementControllerAlternate.cs
// Author(s):	Jeremy Kings (j.kings)
// Project:		Asteroids
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using UnityEngine;

public class PlayerMovementControllerAlternate : MonoBehaviour
{
    // Public properties
    public float MoveSpeedMax = 5.0f;
    public float RotateSpeed = 90.0f;
    // If true, rotates player as direction changes.
    public bool RotationEnabled = true;
    // If true and rotation is enabled, instantly
    // rotates to current move direction.
    public bool RotateInstantly = false;
    // Movement continues in last chosen direction
    public bool MoveContinuous = true;

    // Input properties
    public KeyCode MoveUpKey = KeyCode.W;
    public KeyCode MoveDownKey = KeyCode.S;
    public KeyCode MoveRightKey = KeyCode.D;
    public KeyCode MoveLeftKey = KeyCode.A;

    // Components
    private Rigidbody2D mRigidbody = null;
    private Transform mTransform = null;
    private AudioSource mAudioSource = null;

    // Movement
    private Vector2 moveDirection = Vector2.right;

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
        // Reset movement if not continuous...
        if(!MoveContinuous)
            moveDirection = Vector2.zero;
        // or if any movement key is down
        if(Input.GetKey(MoveUpKey) || Input.GetKey(MoveDownKey) || Input.GetKey(MoveRightKey) || Input.GetKey(MoveLeftKey))
        {
            moveDirection = Vector2.zero;
        }

        // Input
        if (Input.GetKey(MoveUpKey))
            moveDirection.y += 1;

        if (Input.GetKey(MoveDownKey))
            moveDirection.y -= 1;

        if (Input.GetKey(MoveRightKey))
            moveDirection.x += 1;

        if (Input.GetKey(MoveLeftKey))
            moveDirection.x -= 1;

        // Movement
        mRigidbody.velocity = moveDirection.normalized * MoveSpeedMax;

        // Sound
        bool moving = false;
        if (moveDirection != Vector2.zero)
            moving = true;

        if (moving && !mAudioSource.isPlaying)
            mAudioSource.Play();
        else
            mAudioSource.Stop();
    }

    private void Rotate()
    {
        if (!RotationEnabled)
            return;

        Vector2 targetDirection = mRigidbody.velocity.normalized;
        // Find current direction
        Vector2 currentDirection = new Vector2(
                Mathf.Cos(mTransform.eulerAngles.z * Mathf.Deg2Rad),
                Mathf.Sin(mTransform.eulerAngles.z * Mathf.Deg2Rad));

        // Snap instantly to rotation if enabled or within reasonable distance of target rotation
        float targetRotationSnapDistance = 0.03f;
        if (RotateInstantly || ApproximatelyEqual(targetDirection, currentDirection, targetRotationSnapDistance))
        {
            mTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg);
            mRigidbody.angularVelocity = 0;
        }
        else
        {
            float turnDirection = CalculateTurnDirection(targetDirection, currentDirection);
            mRigidbody.angularVelocity = turnDirection * RotateSpeed;
        }
    }

    // Returns 1 if it would be fastest to turn left to get to target.
    // Returns -1 if it would be fastest to turn right to get to target.
    private float CalculateTurnDirection(Vector2 targetDirection, Vector2 currentDirection)
    {
        float turnDirection = 0;

        // Perpendicular to current direction
        Vector2 right = new Vector2(currentDirection.y, -currentDirection.x);

        float rightDotTarget = Vector2.Dot(targetDirection, right);
        float dirDotTarget = Vector2.Dot(targetDirection, currentDirection);
        if (!ApproximatelyEqual(Mathf.Abs(rightDotTarget), 0.0f))
        {
            // dot < 0, turn left
            if (rightDotTarget < 0)
            {
                turnDirection = 1;
            }
            // dot > 0, turn right
            else
            {
                turnDirection = -1;
            }
        }
        // Check if completely opposite direction (~180 degrees)
        else if (ApproximatelyEqual(dirDotTarget, -1.0f))
        {
            // Always turn right
            turnDirection = -1;
        }

        Debug.Log(rightDotTarget);
        Debug.Log(turnDirection);
        return turnDirection;
    }

    // Helper math functions
    private bool ApproximatelyEqual(float first, float second, float epsilon = 0.001f)
    {
        return first < second + epsilon && first > second - epsilon;
    }

    private bool ApproximatelyEqual(Vector2 first, Vector2 second, float epsilon = 0.001f)
    {
        return ApproximatelyEqual(first.x, second.x, epsilon)
            && ApproximatelyEqual(first.y, second.y, epsilon);
    }
}
