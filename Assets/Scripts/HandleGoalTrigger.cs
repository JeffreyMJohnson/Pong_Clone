﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGoalTrigger : MonoBehaviour
{

    public bool IsPlayerOne;

    private ManageScore _managerScript;

	// Use this for initialization
	void Start ()
	{
	    	_managerScript = gameObject.GetComponentInParent<ManageScore>();
    }
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger fired.");

        if (_managerScript != null)
        {
            _managerScript.AddScore(IsPlayerOne);
        }
    }
}