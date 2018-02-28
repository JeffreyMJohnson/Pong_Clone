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
	    Rigidbody2D rb = GetComponent<Rigidbody2D>();
	    if(rb != null)
	    {
            rb.AddForce(_velocity, ForceMode2D.Impulse);
	        //rb.velocity = _velocity;
	    }


	}
	
	// Update is called once per frame
	void Update ()
	{
	    //this.transform.Translate(_velocity * Time.deltaTime);
	    //Vector3 target = transform.position + new Vector3(_velocity.x, _velocity.y, 0);
	    //this.transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other is EdgeCollider2D)
        {
            _velocity = new Vector2(_velocity.x, -_velocity.y);
        }
    }
}
