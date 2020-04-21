using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class DormBouncer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("nobreakfast", "false");
		print("nobreakfast is " + PlayerPrefs.GetString("nobreakfast"));
    }
    
    [YarnCommand("setNoBreakfast")]
    public void noBreakfastTrue() {
		PlayerPrefs.SetString("nobreakfast", "true");
        print("nobreakfast is now " + PlayerPrefs.GetString("nobreakfast"));
    }
}
