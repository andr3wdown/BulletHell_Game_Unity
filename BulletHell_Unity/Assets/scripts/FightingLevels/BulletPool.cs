using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

    [Header("BasicBullets")]
    public GameObject basicBullet;
    public int basicBulletCount;
    [Space(10)]
    [Header("RingBullets")]
    public GameObject ringBullet;
    public int ringBulletCount;

    [HideInInspector]
    public List<GameObject> basicBullets, ringBullets;
    static BulletPool instance;
	// Use this for initialization
	void Start ()
    {
        instance = this;
        CreatePool(basicBulletCount, basicBullets, basicBullet);
        CreatePool(ringBulletCount, ringBullets, ringBullet);
	}
    private void Update()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    void CreatePool(int poolSize, List<GameObject> pool, GameObject bulletType)
    {
        pool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(bulletType) as GameObject;
            go.SetActive(false);
            pool.Add(go);
        }
    }
    public GameObject GetBullet(List<GameObject> pool, int index)
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        switch (index)
        {
            case 0:
                GameObject go = Instantiate(basicBullet);
                go.SetActive(false);
                pool.Add(go);

                return go;

            case 1:
                go = Instantiate(ringBullet);
                go.SetActive(false);
                pool.Add(go);

                return go;

            default:
                go = Instantiate(basicBullet);
                go.SetActive(false);
                pool.Add(go);

                return go;
               
        }
      
    }


}
