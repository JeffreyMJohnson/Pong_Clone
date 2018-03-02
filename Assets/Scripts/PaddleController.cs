using UnityEngine;

public class PaddleController : MonoBehaviour
{

    public float Speed;
    public float MaxY;
    public float MinY;

    public bool IsLeftPaddle;

	// Update is called once per frame
	void Update ()
	{
	    float inputValue = IsLeftPaddle ? Input.GetAxis("L_Paddle") : Input.GetAxis("R_Paddle");
        
	    Vector3 direction = -Vector3.down * inputValue * Speed * Time.deltaTime;

        // clamp the y value to the max so paddles stop at the border.
	    Vector3 newPosition = transform.position + direction;
        float newYPosition = Mathf.Clamp(newPosition.y, MinY, MaxY);
	    newPosition.y = newYPosition;
	    transform.position = newPosition;
	}
}
