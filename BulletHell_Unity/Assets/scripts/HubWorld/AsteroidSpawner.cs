using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    List<Vector3> asteroidPositions = new List<Vector3>();
    public int asteroidAmount;
    public GameObject[] asteroids;
    public GameObject indicator;
    public int asteroidFieldWidth;
    
	// Use this for initialization
	void Start ()
    {
        
        CreateAsteroidLists();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void CreateAsteroidLists()
    {
        asteroidPositions = new List<Vector3>();
        for(int j = 0; j < asteroidFieldWidth; j++)
        {
            for (int i = 0; i < 360; i++)
            {
                transform.parent.rotation = Quaternion.Euler(0,0,i);
                Vector3 pos = transform.position + (transform.parent.position - transform.position).normalized * j;
                asteroidPositions.Add(pos);
            }
        }
        for (int i = 0; i < asteroidAmount; i++)
        {
            int index = Random.Range(0, asteroidPositions.Count);
            GameObject go = (GameObject)Instantiate(asteroids[Random.Range(0, asteroids.Length)], asteroidPositions[index], Quaternion.identity);
            asteroidPositions.RemoveAt(index);
            go.transform.SetParent(this.transform.parent);
            go.GetComponent<Planet>().planetIndicator = indicator;
        }
    }
    

}
