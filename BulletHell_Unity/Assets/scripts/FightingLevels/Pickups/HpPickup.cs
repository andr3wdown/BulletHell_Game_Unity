using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickup : PickupHandler
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            AddLives();
            Destroy(gameObject);
        }
    }
}
