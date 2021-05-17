//------------------------------------------------------------------------------
//
// File Name:	SpawnEffectWhenDestroyed.cs
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

public class SpawnEffectWhenDestroyed : MonoBehaviour
{
    // Public properties
    public GameObject EffectPrefab = null;
    public AudioClip ClipToPlay = null;
    
    // Whether to scale effect object based on this object's scale 
    public bool ScaleEffectFromSource = true;

    // Multiplier for effect size
    public float BaseScaleMultiplier = 1.0f;

    // Colors
    public Color StartColor = Color.white;

    // Sound
    public float ClipVolumeMultiplier = 1.0f;

    // Components
    private Transform mTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        mTransform = GetComponent<Transform>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (mTransform == null)
            return;

        // Create effect object
        var effect = Instantiate(EffectPrefab, mTransform.position,
            Quaternion.identity);

        // Scaling
        var effectTransform = effect.GetComponent<Transform>();
        effectTransform.localScale *= BaseScaleMultiplier;
        if (ScaleEffectFromSource)
        {
            effectTransform.localScale *= mTransform.localScale.magnitude;
        }

        // Color
        var effectParticles = effect.GetComponent<ParticleSystem>();
        if(effectParticles)
        {
            var main = effectParticles.main;
            main.startColor = StartColor;
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
}
