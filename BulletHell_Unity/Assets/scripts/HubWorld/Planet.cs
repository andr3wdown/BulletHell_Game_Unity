using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    bool isActive = false;
    public GameObject planetIndicator;
    public Color activeColor;
    public Color nonActiveColor;
	void FixedUpdate ()
    {
        if (isActive)
        {
            planetIndicator.SetActive(true);
            planetIndicator.transform.position = transform.position;
        }     
	}

    private void OnTriggerEnter2D(Collider2D other)
    {

        isActive = true;
        if(other.GetComponent<TargetIndicator>() != null)
        {
            other.transform.SetParent(this.transform);
            TargetIndicator ind = other.GetComponent<TargetIndicator>();
            if (ind.target == this.transform)
            {
                planetIndicator.GetComponent<SpriteRenderer>().color = activeColor;
                ind.isActive = true;
            }
            else
            {
                planetIndicator.GetComponent<SpriteRenderer>().color = nonActiveColor;
            }
        }
      
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isActive = false;
        other.transform.SetParent(null);
        other.GetComponent<TargetIndicator>().isActive = false;
        planetIndicator.SetActive(false);
    }

}
