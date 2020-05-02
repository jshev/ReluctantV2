using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using Yarn.Unity.Example;

public class CafeBouncer : MonoBehaviour
{
    public GameObject DoorBotan;
    public GameObject FoundSabrina;
    public GameObject TableSabrina;
    public GameObject TableBotan;
    public GameObject FINALCHAIR;
    public GameObject doorToQuad;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

        TableSabrina.SetActive(false);
        TableBotan.SetActive(false);
        FINALCHAIR.SetActive(false);
        doorToQuad.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("destroySabrina")]
    public void destroyFoundSabrina() {
        Destroy(FoundSabrina);
    }

    [YarnCommand("destroyBotan")]
    public void destroyDoorBotan() {
        Destroy(DoorBotan);
    }

    [YarnCommand("seatBotan")]
    public void seatBotan() {
        TableBotan.SetActive(true);
    }

    [YarnCommand("seatSabrina")]
    public void seatSabrina() {
        TableSabrina.SetActive(true);
    }

    [YarnCommand("pullOutChair")]
    public void pullOutChair() {
        FINALCHAIR.SetActive(true);
    }

    [YarnCommand("endEarlyMorning")]
    public void endMorning() {
        //PlayerPrefs.SetString("endearlymorning", "true");
        //print("endearlymorning is now " + PlayerPrefs.GetString("endearlymorning"));
        doorToQuad.SetActive(true);
    }
}
