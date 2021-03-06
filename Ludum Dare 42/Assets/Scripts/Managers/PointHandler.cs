﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointHandler : MonoBehaviour, Imodifiable<int> {

    public int points;

    public Text pointsText;
    public Text goalText;

    private int currentPoints;

    public int CurrentPoints { get { return currentPoints; } }

    private void Start()
    {
        points *= GameManager.level;

        goalText.text = "Goal: " + points.ToString(); 

        ChangeScore();
    }

    private void ChangeScore()
    {
        pointsText.text = "Score: " + currentPoints;
    }

    public void Modifiy(int pointsGained)
    {
        currentPoints += pointsGained;

        if (pointsText != null)
            ChangeScore();
    }

    
}
