using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Cooldown c;
    public GameObject rocket;
    public Transform barrel;
	
	void Update ()
    {
        c.cooldown -= Time.deltaTime;
        if(c.cooldown <= 0)
        {
            Instantiate(rocket, barrel.position, barrel.rotation);
            c.cooldown = c.delay;
        }
	}
}
