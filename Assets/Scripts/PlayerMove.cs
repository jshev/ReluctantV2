using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class PlayerMove : MonoBehaviour
{
	float VelX, VelY, MaxX, MaxY;
	public GameObject nonPlayable;
	public bool pause = false;

    void Update()
    {
    	VelX = 0;
    	VelY = 0;

    	if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true) {
            return;
        }

    	VelX = 0;
    	VelY = 0;
    	if (!pause)
    	{
	        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
	        	VelX +=5;
	        }
	        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
	        	VelX -=5;
	        }
	        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
	        	VelY +=5;
	        }
	        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
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

    void OnCollisionEnter2D (Collision2D col)
    {
    	if (col.gameObject.GetComponent<NPC>() != null) {
    		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
    		CheckForNearbyNPC ();
    		Destroy(col.gameObject);
    	}

    	if (col.gameObject.GetComponent<SceneSwap>() != null) {
    		col.gameObject.GetComponent<SceneSwap>().SceneLoad();
    	}
    }

    public void CheckForNearbyNPC()
    {
    	 var allParticipants = new List<NPC> (FindObjectsOfType<NPC> ());
            var target = allParticipants.Find (delegate (NPC p) {
                return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
                (p.transform.position - this.transform.position)// is in range?
                .magnitude <= 2.0f;
            });
            if (target != null) {
                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner> ().StartDialogue (target.talkToNode);
            }


    }
}
