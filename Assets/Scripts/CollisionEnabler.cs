using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class CollisionEnabler : MonoBehaviour
{
	public GameObject otherObj;
	BoxCollider2D otherCol;

	// Start is called before the first frame update
	void Start()
    {
		otherCol = otherObj.GetComponent<BoxCollider2D>();
		otherCol.enabled = false;
	}

	[YarnCommand("enableBox")]
	public void enableBoxCollider()
	{
		print("Called enableBox on " + gameObject.name);
		otherCol.enabled = true;
	}


}
