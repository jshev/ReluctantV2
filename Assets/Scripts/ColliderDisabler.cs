using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class ColliderDisabler : MonoBehaviour
{
    public GameObject otherObj;
	BoxCollider2D otherCol;

    // Start is called before the first frame update
    void Start()
    {
        otherCol = otherObj.GetComponent<BoxCollider2D>();
    }

    [YarnCommand("disableBox")]
	public void disableBoxCollider()
	{
		print("Called enableBox on " + gameObject.name);
		otherCol.enabled = false;
	}
    
    [YarnCommand("destroyObj")]
    public void destoryObject() {
        print("Called destoryObject on " + gameObject.name);
		Destroy(otherObj);
    }
}
