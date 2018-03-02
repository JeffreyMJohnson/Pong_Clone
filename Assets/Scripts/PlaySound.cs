using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlaySound : MonoBehaviour {

    private AudioSource _audioSource;

    // Use this for initialization
    void Start ()
    {
        _audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(_audioSource);
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    
    void OnCollisionEnter2D(Collision2D coll)
    {
        _audioSource.Play();
    }
}
