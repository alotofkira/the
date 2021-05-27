//------------------------------------------------------------------------------
//
// File Name:	ScreenWrap.cs
// Author(s):	Alex Dzius (alex.dzius)
// Project:		Asteroids
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithScreen : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float wsh = Camera.main.orthographicSize * 2;
        float wsw = wsh / Screen.height * Screen.width;
        transform.localScale = new Vector3(wsw / sr.sprite.bounds.size.x, wsh / sr.sprite.bounds.size.y, 1);
    }
}
