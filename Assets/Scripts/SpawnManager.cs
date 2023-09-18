using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnObstacles", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnObstacles()
    {
        if(playerControllerScript.gameOver ==  false)
        {
            int randObstacleNum = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[randObstacleNum], spawnPos, Quaternion.identity);
        }
        
    }
}
