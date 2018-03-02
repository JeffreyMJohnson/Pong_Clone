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

    void OnTriggerExit2D(Collider2D other)
    {
        if (_managerScript != null)
        {
            _managerScript.AddScore(IsPlayerOne);
        }
    }
}
