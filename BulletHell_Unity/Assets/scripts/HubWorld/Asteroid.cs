using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public float orbitingSpeed;
    [HideInInspector]
    public float currentRotZ;

    [Tooltip("Use Randomized Rotation")]
    public bool random = false;
    private void Start()
    {
        if (random)
        {
            orbitingSpeed = Random.Range(-35f, 35f);
        }
    }

    void FixedUpdate()
    {
        currentRotZ += Time.deltaTime * orbitingSpeed;
        transform.rotation = Quaternion.Euler(0, 0, currentRotZ);
    }
}
