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
        PlayerPrefs.SetString("dadagree", "true");
		print("nobreakfast is " + PlayerPrefs.GetString("nobreakfast"));
		print("dadagree is " + PlayerPrefs.GetString("dadagree"));
    }
    
    [YarnCommand("setNoBreakfast")]
    public void noBreakfastTrue() {
		PlayerPrefs.SetString("nobreakfast", "true");
        print("nobreakfast is now " + PlayerPrefs.GetString("nobreakfast"));
    }

    [YarnCommand("setSociability")]
    public void socialFalse() {
		PlayerPrefs.SetString("dadagree", "false");
        print("dadagree is now " + PlayerPrefs.GetString("dadagree"));
    }
}
