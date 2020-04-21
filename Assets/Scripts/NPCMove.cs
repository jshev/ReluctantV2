using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
	public TextAsset jsonFile;
	Instructions instructions;
	float destinationX = 0f, destinationY = 0f, speed = 0f;
	int instruction = 0;
	public GameObject player;


    public void Activate()
    {
    	instruction = 0;
    	instructions = JsonUtility.FromJson<Instructions>(jsonFile.text);

    	destinationX = instructions.instructionList[instruction].XLoc;
    	destinationY = instructions.instructionList[instruction].YLoc;
    	speed = instructions.instructionList[instruction].Speed;
    }

    void Update() 
    {
    	if (transform.position.x == destinationX && transform.position.y == destinationY)
    	{
    		instruction += 1;
    		if (instructions.instructionList.Length > instruction)
    		{
    			destinationX = instructions.instructionList[instruction].XLoc;
    			destinationY = instructions.instructionList[instruction].YLoc;
    			speed = instructions.instructionList[instruction].Speed;
    		}
    		else 
    		{
    			player.GetComponent<PlayerMove>().pause = false;
    		}
    	}
    	else
    	{
    		transform.position = Vector3.MoveTowards(transform.position, new Vector3(destinationX, destinationY, -2f), speed);
    	}
    }
}

[System.Serializable]
public class Instruction
{
	public float XLoc, YLoc, Speed;
}

[System.Serializable]
public class Instructions
{
	public Instruction[] instructionList;
}
