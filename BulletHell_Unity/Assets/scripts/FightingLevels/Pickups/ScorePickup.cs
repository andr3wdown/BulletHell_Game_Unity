using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : PickupHandler
{
    public int scoreToAdd;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.GetComponent<PlayerController>() != null)
        {
            AddScore(scoreToAdd);
            Destroy(gameObject);
        }
    }

}
