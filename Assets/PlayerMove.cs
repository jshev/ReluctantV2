using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	float VelX, VelY, MaxX, MaxY;
	public GameObject nonPlayable;
	public bool pause = false;

    void Update()
    {
    	VelX = 0;
    	VelY = 0;
    	if (!pause)
    	{
	        if (Input.GetKey(KeyCode.D)) {
	        	VelX +=5;
	        }
	        if (Input.GetKey(KeyCode.A)) {
	        	VelX -=5;
	        }
	        if (Input.GetKey(KeyCode.W)) {
	        	VelY +=5;
	        }
	        if (Input.GetKey(KeyCode.S)) {
	        	VelY -=5;
	        }
	    }

        GetComponent<Rigidbody2D> ().velocity = new Vector2 (VelX, VelY);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
    	nonPlayable.GetComponent<NPCMove>().Activate();
    	pause = true;
    }
}
