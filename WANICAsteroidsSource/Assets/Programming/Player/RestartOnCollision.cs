//------------------------------------------------------------------------------
//
// File Name:	RestartOnCollision.cs
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

public class RestartOnCollision : MonoBehaviour
{
    // Public properties
    public float TimeUntilRestart = 3.0f;

    // Effects
    public GameObject EffectPrefab = null;
    public AudioClip ClipToPlay = null;
    public float EffectMaxDistance = 1.0f;
    public float EffectInterval = 0.9f;
    public float EffectScaleMultiplier = 2.0f;
    public Color EffectStartColor = Color.white;
    public float ClipVolumeMultiplier = 0.5f;

    // Components
    PlayerMovementController movement = null;
    PlayerMovementControllerAlternate alternateMovement = null;
    PlayerShootingController shooting = null;
    Transform mTransform = null;
    AudioSource mAudioSource = null;

    // Misc
    private bool hasCollided = false;
    private float timer = 0.0f;
    private float effectTimer = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        movement = GetComponent<PlayerMovementController>();
        alternateMovement = GetComponent<PlayerMovementControllerAlternate>();
        shooting = GetComponent<PlayerShootingController>();
        mTransform = GetComponent<Transform>();
        mAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasCollided)
        {
            if(timer > TimeUntilRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if(effectTimer > EffectInterval)
            {
                effectTimer = 0.0f;
                SpawnEffect();
            }

            timer += Time.deltaTime;
            effectTimer += Time.deltaTime;
        }
    }

    private void SpawnEffect()
    {
        // Move spawn position in random direction
        var position = mTransform.position;
        var angle = Random.Range(0, 360.0f * Mathf.Deg2Rad);
        var distance = Random.Range(0, EffectMaxDistance);
        position += new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;

        // Create effect object
        var effect = Instantiate(EffectPrefab, position,
            Quaternion.identity);

        // Scaling
        var effectTransform = effect.GetComponent<Transform>();
        effectTransform.localScale *= EffectScaleMultiplier;

        // Color
        var effectParticles = effect.GetComponent<ParticleSystem>();
        if (effectParticles)
        {
            var main = effectParticles.main;
            main.startColor = EffectStartColor;
        }

        // Set audio clip for effect
        var audioSource = effect.GetComponent<AudioSource>();
        if (audioSource != null && ClipToPlay != null)
        {
            audioSource.clip = ClipToPlay;
            audioSource.volume *= ClipVolumeMultiplier;
            audioSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;

        // Disable player controls
        if (movement != null)
            movement.enabled = false;

        if (alternateMovement != null)
            alternateMovement.enabled = false;

        if (shooting != null)
            shooting.enabled = false;

        if(mAudioSource != null)
            mAudioSource.Stop();

        effectTimer = EffectInterval;
    }
}
