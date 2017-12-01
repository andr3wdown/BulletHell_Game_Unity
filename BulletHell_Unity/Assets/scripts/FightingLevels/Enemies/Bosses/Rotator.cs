using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    bool dir;
    float rot;
    public int index;
    public float rotSpeed;

	void Update ()
    {
        rot = RotateBarrel(rot, rotSpeed, index);
        transform.rotation = Quaternion.Euler(0, 0,rot);

        if(transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
	}
    public static bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
    public float RotateBarrel(float value, float ratio, int index)
    {
        if (IsOdd(index))
        {
            dir = true;
        }
        else
        {
            dir = false;
        }


        if (dir)
        {
            value -= Time.deltaTime * ratio;
        }
        else
        {
            value += Time.deltaTime * ratio;
        }

        return value;
    }
}
