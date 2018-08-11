using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager: MonoBehaviour {

    // All the food prefabs in the game
    public GameObject[] foodsProperties;
    public Vector2[] foodPosition;

    public Image nextSprite; 
    public int maxInPlay;

    private const float heightIncrease = 2;

    private Transform[] foodsInPlay;
    
    //This is where we store the next item that is going to come out
    private GameObject next;

    // The current item we are hovering over
    private int currentIndex;

    private void Start()
    {
        foodsInPlay = new Transform[maxInPlay];

        next = null;

        // Get the inital food that will be spawned
        GetFood();

        for(int i = 0; i < maxInPlay; i++)
            SpawnFood(i);

        currentIndex = 0;
        activateFood(1);
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Horizontal"))
            ChangeFoodChoice((int)horizontal);

        if (Input.GetButtonDown("Submit"))
            DestroyFood();

    }

    private void ChangeFoodChoice(int direction)
    {
        if (currentIndex + direction >= 0 && currentIndex + direction < maxInPlay)
        {
            activateFood(-1);
            currentIndex += direction;
            activateFood(1);
        }
    }

    /// <summary>
    /// Changes the position of the object depending on wether we are over a object or not
    /// </summary>
    /// <param name="verticalDirection">1: Represents the object moves up. -1: Represents it moves down</param>
    private void activateFood(int verticalDirection)
    {
        Vector2 newPosition = foodsInPlay[currentIndex].position;
        newPosition.y += (heightIncrease * verticalDirection);

        foodsInPlay[currentIndex].position = newPosition;
    }

    // Spawns the food that is going to be on the stage
    private void SpawnFood(int index)
    {
        GameObject hold = Instantiate(next, foodPosition[index], Quaternion.identity);
        foodsInPlay[index] = hold.GetComponent<Transform>();
        GetFood();
    }

    // Chooses a random food from foodsProperties array
    private void GetFood()
    {
        next = foodsProperties[Random.Range(0, foodsProperties.Length)];
        nextSprite.sprite = next.GetComponent<SpriteRenderer>().sprite;
    }

    private void DestroyFood()
    {
        Destroy(foodsInPlay[currentIndex].gameObject);
        SpawnFood(currentIndex);
        activateFood(1);
    }
}
