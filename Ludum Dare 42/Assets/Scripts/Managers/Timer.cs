using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour, Iobserver {

    private const int seconds = 60;

    public int initalMinuteTime;
    public int initalSecondTime;

    public Text minuteTimer;

    private Subject subject;

    private int currentTimeInSeconds;

    private bool isActive;

    // Use this for initialization
    void Start () {
        currentTimeInSeconds = initalSecondTime;

        subject = GetComponent<Subject>();
        subject.addObserver(this);

        isActive = true;

        StartCoroutine("UpdateTimer");
	}
	
    private IEnumerator UpdateTimer()
    {
        while(isActive)
        {
            if (initalMinuteTime == 0 && currentTimeInSeconds == 0)
                subject.UpdateObservers(Event.timerStop);

            ChangeTimer();

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void ChangeTimer()
    {
        if (currentTimeInSeconds < 0)
        {
            currentTimeInSeconds = 59;
            initalMinuteTime--;
        }

        minuteTimer.text = initalMinuteTime.ToString() + ":";

        if (currentTimeInSeconds < 10)
            minuteTimer.text += "0";

        minuteTimer.text += currentTimeInSeconds.ToString();

        currentTimeInSeconds--;
    }

    public void Notify(Event type)
    {
        //Set is Active to false in order to stop the timer
        isActive = false;
    }
}
