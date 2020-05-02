using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassHallBouncer : MonoBehaviour
{
    public GameObject Player;
    GameObject Class1Door;
    GameObject Class2Door;
    GameObject BathDoor;
    GameObject NPC;

    // Start is called before the first frame update
    void Start()
    {
        Class1Door = GameObject.Find("Class1Door");
        Class2Door = GameObject.Find("Class2Door");
        BathDoor = GameObject.Find("BathDoor");
        NPC = GameObject.Find("NPC");

        if (PlayerPrefs.GetString("LastScene") == "Quad1") {
            BathDoor.SetActive(false);
            Class2Door.SetActive(false);
            NPC.SetActive(false);
            Player.transform.position = new Vector3(-2.77f, 2.14f, 0f);
        }

        if (PlayerPrefs.GetString("LastScene") == "Class1") {
            Class1Door.SetActive(false);
            Class2Door.SetActive(false);
            NPC.SetActive(false);
            Player.transform.position = new Vector3(7.48f, 1.79f, 0f);
        }

        if (PlayerPrefs.GetString("LastScene") == "Bath1") {
            Class1Door.SetActive(false);
            BathDoor.SetActive(false);
            Player.transform.position = new Vector3(-16.7f, 1.86f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
