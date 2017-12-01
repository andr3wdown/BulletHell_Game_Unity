using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundTiler : MonoBehaviour
{
    Vector3[,] positions;
    public int gridSize;
    public Vector2 offset;

    public GameObject backgroundtile;
    private void Start()
    {
        GenerateBackGround();
    }
    public void GenerateBackGround()
    {
        positions = GetPositions();
        PlaceTiles();
	}
    Vector3[,] GetPositions()
    {
        Vector3[,] next = new Vector3[gridSize*2,gridSize*2];
        for(int x = -gridSize; x < gridSize; x++)
        {
            for (int y = -gridSize; y < gridSize; y++)
            {
                next[x+gridSize, y+gridSize] = new Vector3(x * offset.x, y * offset.y, 0);
            }
        }

        return next;
    }
	
    public void PlaceTiles()
    {
        if (positions == null)
            Debug.LogError("position list invalid!");
        else
        {
            {
                foreach(Vector3 p in positions)
                {
                    GameObject go = Instantiate(backgroundtile, p, Quaternion.identity);
                    go.transform.SetParent(this.transform);
                }

            }
        }
    }
	
	void Update () {
		
	}
}
