using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    public int maxShotCount;

    CameraController cc;
    ScoreHandler sh;
    private void Start()
    {
        cc = FindObjectOfType<CameraController>();
        sh = FindObjectOfType<ScoreHandler>();
    }
    public void AddLives()
    {
        cc.lives++;
    }
    public void IncreaseShotCount()
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        pc.shotCount++;
        if(pc.shotCount >= maxShotCount)
        {
            pc.shotCount = maxShotCount;
        } 
    }
    public void ChangeShotType()
    {

    }
    public void AddScore(int amount)
    {
        sh.AddScore(amount);
    }
	
}
