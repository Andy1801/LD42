using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, Islider {

    public Slider Slider { get { return spaceSlider; } }

    private Slider spaceSlider;

    private void Start()
    {
        spaceSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
    }

    public void Modifiy(int fillAmount)
    {
        spaceSlider.value += fillAmount;
    }

}
