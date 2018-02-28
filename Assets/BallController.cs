using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float Speed;

    private Vector2 _velocity;


	// Use this for initialization
	void Start ()
	{
        // start the ball rolling
        Vector2 direction = new Vector2(1.0f,-1.0f);
	    _velocity = Speed * direction;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    this.transform.Translate(_velocity * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D)
        {
            _velocity = new Vector2(_velocity.x, -_velocity.y);
        }
    }
}
