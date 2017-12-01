using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float damage;

	void Update ()
    {
        MoveBullet();
	}

    void MoveBullet()
    {
        Vector3 pos = transform.position;
        pos = transform.position + (transform.up * speed * Time.deltaTime);
        transform.position = pos;
    }
    public virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().TakeHP(damage);
            Destroy(gameObject); 
        }
    }
}
