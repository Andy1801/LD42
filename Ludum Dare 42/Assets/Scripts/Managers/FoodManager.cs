using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager: MonoBehaviour, Iobserver {

    private const float heightIncrease = 2;

    // All the food prefabs in the game
    public GameObject[] foodsProperties;
    public Vector2[] foodPosition;

    public Image nextSprite; 
    public int maxInPlay;
    public float percantageFoodDecrease;

    private ObjectActions[] foodsInPlay;

    private Subject subject;
    
    //This is where we store the next item that is going to come out
    private GameObject next;

    // The current item we are hovering over
    private int currentIndex;
    private bool isActive;

    private void Start()
    {
        foodsInPlay = new ObjectActions[maxInPlay];

        subject = GetComponent<Subject>();
        subject.addObserver(this); 

        next = null;

        // Get the inital food that will be spawned
        GetFood();

        for(int i = 0; i < maxInPlay; i++)
            SpawnFood(i);

        currentIndex = 0;
        activateFood(1);

        isActive = true;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (isActive)
        {
            if (Input.GetButtonDown("Horizontal"))
                ChangeFoodChoice((int)horizontal);

            if (Input.GetButtonDown("Submit"))
                DestroyFood(currentIndex);
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < maxInPlay; i++)
        {
            if ( isActive && foodsInPlay[i].isThrownOut)
                DestroyFood(i);
        }
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
        Vector2 newPosition = foodsInPlay[currentIndex].transform.position;
        newPosition.y += (heightIncrease * verticalDirection);

        foodPosition[currentIndex] = newPosition;
        foodsInPlay[currentIndex].transform.position = newPosition;
    }

    // Spawns the food that is going to be on the stage
    private void SpawnFood(int index)
    {
        GameObject hold = Instantiate(next, foodPosition[index], Quaternion.identity);
        foodsInPlay[index] = hold.GetComponent<ObjectActions>();
        GetFood();
    }

    // Chooses a random food from foodsProperties array
    private void GetFood()
    {
        //next = foodsProperties[Random.Range(0, foodsProperties.Length)];

        next = null;

        // Holds the potential next food to come
        ObjectActions hold;

        while(next == null)
        {
            hold = foodsProperties[Random.Range(0, foodsProperties.Length)].GetComponent<ObjectActions>();

            float random = hold.properties.spawnRate;

            for(int i = 0; i < foodsInPlay.Length; i++)
            {
                if (foodsInPlay[i] == null)
                    continue;
                else if (foodsInPlay[i].properties == hold.properties)
                    random -= (((random / 100) * (percantageFoodDecrease / 100)) * 100);
            }
            int randomTemp = Random.Range(0, 100);

            if (randomTemp <= random)
                next = hold.gameObject;

        }

        nextSprite.sprite = next.GetComponent<SpriteRenderer>().sprite;
    }

    private void DestroyFood(int index)
    {
        Destroy(foodsInPlay[index].gameObject);
        SpawnFood(index);

        if (foodsInPlay[index].isThrownOut)
            activateFood(1);
    }

    public void Notify(Event type)
    {
        isActive = false;
    }
}
