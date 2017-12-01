using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PickupHandler
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            IncreaseShotCount();
            Destroy(gameObject);
        }
    }
}
