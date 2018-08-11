using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActions : MonoBehaviour
{
    public ObjectStats properties;

    private SpriteRenderer sprite;

    private Islider spaceSlider;

    private Vector2 originalPosition;

    // Use this for initialization
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        spaceSlider = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        originalPosition = transform.position;
    }

    private void OnDestroy()
    {
        Debug.Log(properties.objectName + " has been destoried");
        // Increase the fill meter
        spaceSlider.Modifiy(properties.meterFill);
    }
}
