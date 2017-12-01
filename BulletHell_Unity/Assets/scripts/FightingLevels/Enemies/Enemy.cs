using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public bool isActive = false;
    public float speed;
    public float delay;
    [HideInInspector]
    public float cooldown;
    [HideInInspector]
    public BulletPool bulletPool;

    public GameObject[] drops = new GameObject[3];
    private void Awake()
    {
        bulletPool = FindObjectOfType<BulletPool>();
    }
    void OnBecameInvisible()
    {
        if (isActive)
        {
            Destroy(gameObject);
        }
    }
    
    public void TakeHP(float hpToTake)
    {
        hp -= hpToTake;
        if(hp <= 0)
        {
            Death();
        }
    }
    bool dying = false;
    void Death()
    {
        if (!dying)
        {
            dying = true;
            for (int i = 0; i < 3; i++)
            {
                Vector3 pos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0);
                Instantiate(drops[0], pos, transform.rotation);
            }

            int sele = Random.Range(0, 100);
            if (sele == 1 || sele == 4 || sele == 5)
            {
                Instantiate(drops[1], transform.position, transform.rotation);
            }
            else if (sele == 2)
            {
                Instantiate(drops[2], transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    
    }
    public GameObject SpawnBullet(Vector3 pos, Quaternion rot, List<GameObject> bulletTypeList)
    {
        int index = 0;

        if(bulletTypeList == bulletPool.basicBullets)
        {
            index = 0;
        }
        else
        {
            index = 1;
        }

        GameObject go = bulletPool.GetBullet(bulletTypeList, index);
        go.transform.position = pos;
        go.transform.rotation = rot;
        go.SetActive(true);
        return go;
    }
}
