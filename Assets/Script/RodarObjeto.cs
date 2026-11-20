using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodarObjeto : MonoBehaviour
{
    [Header("Ajustes da rotação")]
    public Vector3 speed;

    void FixedUpdate()
    {
        transform.Rotate(speed * Time.fixedDeltaTime);
    }
}