using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    //Global Singleton accessor
    public static RPGGameManager sharedInstance = null;

    //Instance vars
    public SpawnPoint playerSpawnPoint;
    public RPGCameraManager cameraManager;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        setupScene();
    }

    public void setupScene()
    {
        spawnPlayer();
    }

    public void spawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.spawnObject();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
}
