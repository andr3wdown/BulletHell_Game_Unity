using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().TakeHp(damage);
            if (gameObject.transform.parent != null)
                gameObject.transform.SetParent(null);

            gameObject.SetActive(false);
            
        }
    }
    public override void OnBecameInvisible()
    {
        if(gameObject.transform.parent != null)
           gameObject.transform.SetParent(null);

        gameObject.SetActive(false);
        
    }

    public void Deactivate()
    {
        if (gameObject.transform.parent != null)
            gameObject.transform.SetParent(null);

        gameObject.SetActive(false);
    }

}
