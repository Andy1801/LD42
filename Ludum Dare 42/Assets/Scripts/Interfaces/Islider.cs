using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface Islider {
    Slider Slider { get; }

    void Modifiy(int fillAmount);
}
