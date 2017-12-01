using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andr3wDown.Math;

public class PatternBomber : BasicBomber
{
    public override void Shoot()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            for (int i = 0; i < bulletCount / 2; i++)
            {
                Quaternion rot = barrel.rotation;
                rot = Quaternion.Euler(0, 0, barrel.rotation.eulerAngles.z - 90 + spread * i);
                SpawnBullet(barrel.position, rot, bulletPool.basicBullets);

            }

            for (int i = 0; i < bulletCount / 2; i++)
            {
                Quaternion rot = barrel.rotation;
                rot = Quaternion.Euler(0, 0, barrel.rotation.eulerAngles.z + 90 - spread * i);
                SpawnBullet(barrel.position, rot, bulletPool.basicBullets);
            }
            cooldown = delay;
        }
    }
}