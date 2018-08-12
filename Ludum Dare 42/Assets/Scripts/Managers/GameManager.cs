using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Event
{
    timerStop,
    meterFilled,
};

public class GameManager : MonoBehaviour, Iobserver {

    [System.NonSerialized]
    public static int level = 1;

    public Text gameOverText;
    public Text restartText;
    public Text exitText;

    private Subject subject;
    private PointHandler pointHandler;

    private bool gameOver;

    private void Start()
    {
        subject = GetComponent<Subject>();
        subject.addObserver(this);

        pointHandler = GetComponent<PointHandler>();

        gameOver = false;
    }

    private void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);

        if (pointHandler.CurrentPoints >= pointHandler.points)
        {
            gameOverText.text = "You win!";
            restartText.text = "Next Level";
            level++;
        }
        else
        {
            gameOverText.text = "You lose.";
            restartText.text = "Restart";
            level = 1;
        }

        exitText.text = "Exit";
    }

    //Called when restart button is pressed
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Notify(Event type)
    {
        if(!gameOver)
            GameOver();
    }

}
