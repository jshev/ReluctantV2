using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class SceneStart : MonoBehaviour
{

    bool once = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!once) {
            CheckForNearbyNPC();
            once = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
