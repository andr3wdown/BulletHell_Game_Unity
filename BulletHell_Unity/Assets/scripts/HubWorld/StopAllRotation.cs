using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAllRotation : MonoBehaviour
{
    public bool reverse = false;
	void FixedUpdate ()
    {
        if(reverse)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180f);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
        
	}
}
