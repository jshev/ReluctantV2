using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class CafeBouncer : MonoBehaviour
{
    public GameObject DoorBotan;
    public GameObject FoundSabrina;
    public GameObject TableSabrina;
    public GameObject TableBotan;
    public GameObject FINALCHAIR;

    // Start is called before the first frame update
    void Start()
    {
        TableSabrina.SetActive(false);
        TableBotan.SetActive(false);
        FINALCHAIR.SetActive(false);
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
}
