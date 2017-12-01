using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBomber : Enemy {

    public List<Transform> mids;
    public Transform[] ends;
    public int routeLenght;
    public virtual void Start()
    {
        InitRoute();
    }
    public List<Transform> route = new List<Transform>();
    public Transform barrel;
    public GameObject bullet;
    public int bulletCount;
    public float spread;
    int selection = 0;
	void FixedUpdate ()
    {
        if (isActive)
        {
            if (Vector3.Distance(transform.position, route[selection].position) < 0.2f)
            {
                selection++;
                if (selection >= route.Count)
                {
                    selection = route.Count - 1;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, route[selection].position, speed * Time.deltaTime);

            if (selection > 0)
            {
                Shoot();
            }
        }
		
	}
    public virtual void Shoot()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            for(int i = 0; i < bulletCount; i++)
            {
                Quaternion rot = barrel.rotation;
                rot = Quaternion.Euler(0, 0, barrel.rotation.eulerAngles.z + Random.Range(-spread, spread));
                SpawnBullet(barrel.position, rot, bulletPool.basicBullets);
            }
            cooldown = delay;
        }
    }
    public virtual void InitRoute()
    {
        for(int i = 0; i < routeLenght; i++)
        {
            int index = Random.Range(0, mids.Count);
            route.Add(mids[index]);
            mids.RemoveAt(index);
        }

        route.Add(ends[Random.Range(0, ends.Length)]);
    }


}
