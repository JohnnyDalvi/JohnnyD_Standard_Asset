using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public Vector3 MovementVector3;

    void Update()
    {
        movingSpeed(MovementVector3);
    }
    public void movingSpeed(Vector3 moving)
    {
        transform.position += moving;
    }
}
