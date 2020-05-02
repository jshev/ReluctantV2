using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using Yarn.Unity.Example;

public class BathBouncer : MonoBehaviour
{
    public GameObject blockage;
    GameObject breakfaststall;
    GameObject nobreakfaststall;
    GameObject apologizenobreakfaststall;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        
        print("nobreakfast is " + PlayerPrefs.GetString("nobreakfast"));
		print("apologizetobotan is " + PlayerPrefs.GetString("apologizetobotan"));
        
        breakfaststall = GameObject.Find("BreakfastStall");
        nobreakfaststall = GameObject.Find("NoBreakfastStall");
        apologizenobreakfaststall = GameObject.Find("ApologizeNoBreakfastStall");

        if (PlayerPrefs.GetString("nobreakfast") == "false") {
            nobreakfaststall.SetActive(false);
            apologizenobreakfaststall.SetActive(false);
        } else {
            breakfaststall.SetActive(false);
            if (PlayerPrefs.GetString("apologizetobotan") == "true") {
                nobreakfaststall.SetActive(false);
            } else {
                apologizenobreakfaststall.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("blockExit")]
    public void blockExit()
	{
		blockage.SetActive(true);
	}


}
