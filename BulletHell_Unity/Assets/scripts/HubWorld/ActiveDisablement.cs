using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDisablement : MonoBehaviour
{
    HubWorldPlayerController pc;
	// Use this for initialization
	void Awake ()
    {
        pc = FindObjectOfType<HubWorldPlayerController>();
	}
    private void OnEnable()
    {
        pc.isActive = false;
    }
    private void OnDisable()
    {
        pc.isActive = true;
    }
}
