using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Property/Food")]
public class ObjectStats : ScriptableObject {

    public string objectName;
    public int pointsWorth;

    public int meterFill;

    //Time till the food goes bad and starts increasing the amount that the meal fills
    public float timeTillBad;
    public float badMultipler;

    // How much time before the object becomes worse after it has passed the time till bad
    public float timeTillWorse;
}
