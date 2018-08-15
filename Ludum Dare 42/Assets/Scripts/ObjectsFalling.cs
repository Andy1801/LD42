using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFalling : MonoBehaviour {

    public GameObject[] foodInPlay;

    public float ySpawn;

    public float minXSpawn;
    public float maxXSpawn;

    public float timeInBetween;

    private float startingTime;

    private void Start()
    {
        startingTime = timeInBetween;
    }

    private void Update()
    {
        if(startingTime - Time.time <= 0)
        {
            startingTime = Time.time + timeInBetween;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Vector2 spawnLocation = new Vector2(Random.Range(minXSpawn, maxXSpawn), ySpawn);

        GameObject hold = Instantiate(foodInPlay[Random.Range(0, foodInPlay.Length)], spawnLocation, Quaternion.identity);

        hold.GetComponent<ObjectActions>().enabled = false;
        hold.AddComponent<Rigidbody2D>();
        hold.AddComponent<BoxCollider2D>();
        
    }
}
