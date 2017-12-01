using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public Text score, lives;
    public int scorei, livesi;
    CameraController cc;
    private void Start()
    {
        cc = FindObjectOfType<CameraController>();
    }
    public void AddScore(int amount)
    {
        scorei += amount;
    }

    public void Update()
    {
        livesi = cc.lives;
        score.text = "" + scorei;
        lives.text = "" + livesi;
    }

}
