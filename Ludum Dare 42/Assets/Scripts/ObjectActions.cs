using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActions : MonoBehaviour
{
    public ObjectStats properties;

    [System.NonSerialized]
    public bool isBad;

    [System.NonSerialized]
    public bool isThrownOut;

    private SpriteRenderer spriteRenderer;

    private Imodifiable<int> modifiySlider;
    private Imodifiable<int> modifiyPoints;

    private float baseMeterFilled;
    
    // Use this for initialization
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        modifiySlider = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SliderHandler>();
        modifiyPoints = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PointHandler>();

        isBad = false;
        isThrownOut = false;
        baseMeterFilled = properties.meterFill;

        StartCoroutine("GoingBad");
    }

    private IEnumerator GoingBad()
    {
        float lerpTime = (Time.deltaTime / properties.timeTillBad);
        float timeTillBad = properties.timeTillBad + Time.time;

        while(!isBad)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.black, lerpTime);

            if (timeTillBad - Time.time <= 0.0f)
            {
                isBad = true;
                baseMeterFilled *= properties.badMultipler;
            }

            yield return new WaitForEndOfFrame();
        }

        StartCoroutine("ThrownOut");
    }

    private IEnumerator ThrownOut()
    {
        float timeTillThrown = properties.timeTillWorse + Time.time;

        while(!isThrownOut)
        {
            if (timeTillThrown - Time.time <= 0.0f)
                isThrownOut = true;

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDestroy()
    {
        Debug.Log(properties.objectName + " has been destoried");

        //Increasing both the fill meter and the points gained
        if (!isThrownOut)
        {
            modifiySlider.Modifiy((int)baseMeterFilled);
            modifiyPoints.Modifiy(properties.pointsWorth);
        }
    }
}
