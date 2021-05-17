//------------------------------------------------------------------------------
//
// File Name:	OptionsSliderLogic.cs
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
using UnityEngine.UI;
using UnityEngine.Audio;

public enum SliderFunctions
{
    OverallVolume,
    MusicVolume,
    SFXVolume,
}

public class OptionsSliderLogic : MonoBehaviour
{
    // Options for what to do when button is clicked
    public SliderFunctions sliderFunction;

    // Mixers
    public AudioMixer mainMixer;

    // Function to call when button is clicked
    private delegate void SliderAction(float value);
    private SliderAction sliderAction;

    // Slider component
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        // Set slider action and value
        sliderAction = null;
        float sliderValue = 0.0f;
        switch (sliderFunction)
        {
            case SliderFunctions.OverallVolume:
                sliderAction = SetOverallVolume;
                mainMixer.GetFloat("OverallVolume", out sliderValue);
                break;
            case SliderFunctions.MusicVolume:
                sliderAction = SetMusicVolume;
                mainMixer.GetFloat("MusicVolume", out sliderValue);
                break;
            case SliderFunctions.SFXVolume:
                sliderAction = SetSFXVolume;
                mainMixer.GetFloat("SFXVolume", out sliderValue);
                break;
        }

        // Get slider component and set value
        slider = GetComponent<Slider>();
        // Wondering why the weird formula for changing the volume?
        // Source: https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
        slider.value = Mathf.Pow(10.0f, sliderValue / 20.0f);
    }

    public void OnSliderChanged()
    {
        sliderAction(slider.value);
    }

    // Wondering why the weird formula for changing the volume?
    // Source: https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
    private void SetOverallVolume(float value)
    {
        mainMixer.SetFloat("OverallVolume", Mathf.Log10(value) * 20);
    }

    private void SetMusicVolume(float value)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    private void SetSFXVolume(float value)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}
