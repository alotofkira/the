//------------------------------------------------------------------------------
//
// File Name:	PlayerShootingController.cs
// Author(s):	Jeremy Kings (j.kings) & Alex Dzius (alex.dzius)
// Project:		Asteroids
// Course:		WANIC VGP
//
// Copyright © 2021 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    public GameObject ProjectilePrefab = null;
    public float ShootDelay = 0.5f;
    public float ProjectileSpeed = 10.0f;
    public KeyCode ShootKey = KeyCode.Space;
    // If true, adds player move speed to projectile speed
    public bool AddPlayerMoveSpeed = true;

    private float shootTimer = 0.0f;
    private Transform mTransform = null;
    private Rigidbody2D mRigidbody = null;
    private Vector3 playerPos;
    private Vector3 spawnPos;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = ShootDelay * OptionsSliderLogic.modReload;
        mTransform = GetComponent<Transform>();
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            playerPos = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y, player.GetComponent<Transform>().position.z);
        }
       

        player = GameObject.Find("PlayerShip");

        ProjectileSpeed = OptionsSliderLogic.modBullet;
        ShootDelay = 0.5f * OptionsSliderLogic.modReload;
        if(Input.GetKeyUp(ShootKey) && shootTimer >= ShootDelay)
        {
            SpawnProjectile();
            shootTimer = 0.0f;
        }

        shootTimer += Time.deltaTime;
    }

    void SpawnProjectile()
    {
        if(player != null)
        {
            Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
            if(player.GetComponent<PlayerMovementControllerAlternate>().spriteNum == 0)
            {
                spawnPos = new Vector3(playerPos.x, playerPos.y + 0.5f, playerPos.z);
                rotation = new Vector3(0.0f, 0.0f, 90.0f);
            }
            else if (player.GetComponent<PlayerMovementControllerAlternate>().spriteNum == 1)
            {
                spawnPos = new Vector3(playerPos.x, playerPos.y - 0.5f, playerPos.z);
                rotation = new Vector3(0.0f, 0.0f, -90.0f);
            }
            else if (player.GetComponent<PlayerMovementControllerAlternate>().spriteNum == 2)
            {
                spawnPos = new Vector3(playerPos.x + 0.5f, playerPos.y, playerPos.z);
            }
            else if (player.GetComponent<PlayerMovementControllerAlternate>().spriteNum == 3)
            {
                spawnPos = new Vector3(playerPos.x - 0.5f, playerPos.y, playerPos.z);
                rotation = new Vector3(0.0f, 0.0f, 180.0f);
            }
            var projectile = GameObject.Instantiate(ProjectilePrefab, spawnPos, Quaternion.identity);
            projectile.transform.eulerAngles = rotation;
            var direction = new Vector3(Mathf.Cos(rotation.z * Mathf.Deg2Rad), Mathf.Sin(rotation.z * Mathf.Deg2Rad), 0.0f);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * ProjectileSpeed;
        }

        /*var spawnPosition = mTransform.position;
        // Nudge slightly ahead of ship
        var rotation = mTransform.eulerAngles.z * Mathf.Deg2Rad;
        var direction = new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0.0f);
        spawnPosition += direction * (mTransform.localScale.x / 2.0f
            + ProjectilePrefab.transform.localScale.x / 2.0f);

        // Create object
        var projectile = GameObject.Instantiate(ProjectilePrefab, spawnPosition, mTransform.rotation);

        // Move object
        var body = projectile.GetComponent<Rigidbody2D>();
        body.velocity = direction * ProjectileSpeed;
        if (AddPlayerMoveSpeed)
            body.velocity += mRigidbody.velocity;*/
    }
}
