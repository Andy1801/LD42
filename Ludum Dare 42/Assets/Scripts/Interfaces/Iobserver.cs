using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iobserver {
    void Notify(Event type);
}
