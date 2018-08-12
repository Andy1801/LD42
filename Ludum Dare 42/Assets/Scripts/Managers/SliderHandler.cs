using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour, Imodifiable<int>{

    private Subject subject;
    private Slider spaceSlider;

    private Image fillImage;
    private Color fillColor;

    private float maxSlider;

    private void Start()
    {
        subject = GetComponent<Subject>();

        spaceSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        fillImage = spaceSlider.fillRect.GetComponent<Image>();
        fillColor = fillImage.color;
        maxSlider = spaceSlider.maxValue;
    }

    public void ChangeColor()
    {
        Color hold = Color.green;

        float rgbChange = spaceSlider.value / maxSlider;

        hold.r = fillColor.r + rgbChange;
        hold.g = fillColor.g - rgbChange;

        if (fillImage != null)
            fillImage.color = hold;
    }

    public void Modifiy(int fillMeter)
    {
        spaceSlider.value += fillMeter;

        if (spaceSlider.value >= maxSlider)
            subject.UpdateObservers(Event.meterFilled);
    }
}
