using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                spriteRenderer.color = Color.black;
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
        //Increasing both the fill meter and the points gained
        if (!isThrownOut)
        {
            if (modifiyPoints != null && modifiySlider != null)
            {
                modifiyPoints.Modifiy(properties.pointsWorth);
                modifiySlider.Modifiy((int)baseMeterFilled);
            }
        }
    }
}
