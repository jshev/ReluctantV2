using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using Yarn.Unity.Example;

public class QuadBouncer : MonoBehaviour
{
    public GameObject Player;
    public GameObject LibraryDoor;
    public GameObject CafeDoor;
    public GameObject SchoolDoor;
    public GameObject Botan;
    public GameObject talkSab;
    public GameObject walkSab;
    // Start is called before the first frame update
    void Start()
    {
        LibraryDoor = GameObject.Find("LibraryDoor");
        CafeDoor = GameObject.Find("CafeDoor");
        SchoolDoor = GameObject.Find("SchoolDoor");
        Botan = GameObject.Find("Botan");
        talkSab = GameObject.Find("TalkSabrina");
        walkSab = GameObject.Find("WalkSabrina");

        //PlayerPrefs.SetString("LastScene", "DormLobby2");

        if (PlayerPrefs.GetString("LastScene") == "DormLobby2") {
            talkSab.SetActive(false);
            walkSab.SetActive(false);
            SchoolDoor.SetActive(false);
            if (PlayerPrefs.GetString("nobreakfast") == "true") {
                CafeDoor.SetActive(false);
                Botan.SetActive(false);
            } else {
                LibraryDoor.SetActive(false);
            }
        }

        if (PlayerPrefs.GetString("LastScene") == "Library1") {
            LibraryDoor.SetActive(false);
            CafeDoor.SetActive(false);
            Botan.SetActive(false);
            SchoolDoor.SetActive(false);
            if (PlayerPrefs.GetString("apologizetobotan") == "true") {
                walkSab.SetActive(false);
            } else {
                talkSab.SetActive(false);
            }
            Player.transform.position = new Vector3(-9.53f, -18.4f, 0f);
        }

        if (PlayerPrefs.GetString("LastScene") == "Cafe1") {
            talkSab.SetActive(false);
            walkSab.SetActive(false);
            Botan.SetActive(false);
            CafeDoor.SetActive(false);
            LibraryDoor.SetActive(false);
            Player.transform.position = new Vector3(10.45f, -18.43f, 0f);
        }

        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
    }

    [YarnCommand("endEarlyMorning")]
    public void endMorning() {
        //PlayerPrefs.SetString("endearlymorning", "true");
        //print("endearlymorning is now " + PlayerPrefs.GetString("endearlymorning"));
        SchoolDoor.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
