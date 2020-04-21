using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;
using UnityEngine.SceneManagement;

public class LobbyBouncer : MonoBehaviour
{

    GameObject sneaky;
    GameObject apologetic;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        print("nobreakfast is " + PlayerPrefs.GetString("nobreakfast"));
		print("apologizetobotan is " + PlayerPrefs.GetString("apologizetobotan"));

        sneaky = GameObject.Find("SneakBotan");
        apologetic = GameObject.Find("ApologizeBotan");

        if (PlayerPrefs.GetString("nobreakfast") == "false") {
            sneaky.SetActive(false);
            apologetic.SetActive(false);
        } else {
            if (PlayerPrefs.GetString("apologizetobotan") == "true") {
                sneaky.SetActive(false);
            } else {
                apologetic.SetActive(false);
            }
        }

    }
}
