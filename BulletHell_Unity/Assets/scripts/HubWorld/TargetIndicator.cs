using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andr3wDown.Math;

public class TargetIndicator : MonoBehaviour
{
    public Transform target;
    public GameObject indicator;
    public bool isActive = false;
	void Update ()
    {

        if(target != null && !isActive)
        {          
            indicator.SetActive(true);
            indicator.transform.localPosition = Vector3.zero;
            indicator.transform.rotation = MathOperations.LookAt2D(transform.position, target.position, 90);
        }
        else
        {
            indicator.SetActive(false);
        }
        
	}
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void NullTarget()
    {
        target = null;
    }
}
