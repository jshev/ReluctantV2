using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class HallBouncer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		print("nobreakfast is " + PlayerPrefs.GetString("nobreakfast"));
        PlayerPrefs.SetString("apologizetobotan", "false");
		print("apologizetobotan is " + PlayerPrefs.GetString("apologizetobotan"));
    }

    [YarnCommand("setApologizeToBotan")]
    public void ApologizeToBotanTrue() {
		PlayerPrefs.SetString("apologizetobotan", "true");
        print("apologizetobotan is now " + PlayerPrefs.GetString("apologizetobotan"));
    }
}
