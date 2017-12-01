using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andr3wDown.Math;

public class TargetBomber : BasicBomber
{
    [HideInInspector]
    public Transform target;

    public override void Start()
    {
        base.Start();
        target = FindObjectOfType<PlayerController>().transform;
    }
    public Transform[] barrels;
    public bool bulletType = true;
    public override void Shoot()
    {
        if(target != null)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    for (int j = 0; j < barrels.Length; j++)
                    {
                        Quaternion rot = MathOperations.LookAt2D(target.position, barrels[j].position, -90);
                        rot = Quaternion.Euler(0, 0, rot.eulerAngles.z + Random.Range(-spread, spread));

                        if(bulletType)
                            SpawnBullet(barrel.position, rot, bulletPool.basicBullets);
                        else
                            SpawnBullet(barrel.position, rot, bulletPool.ringBullets);
                    }


                }
                cooldown = delay;
            }
        }
        else
        {
            StartCoroutine(FindPlayer());
        }
        
    }

    public IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(2.1f);

        
        target = FindObjectOfType<PlayerController>().transform;
        

        if(target == null)
        {
            StartCoroutine(FindPlayer());
        }
        else
        {
            StopCoroutine(FindPlayer());
        }

    }
}
