using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
	private FixedJoint2D fixedJoint;
		
	// Use this for initialization
	void Start ()
	{
		fixedJoint = GetComponent<FixedJoint2D>();
		fixedJoint.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(fixedJoint.connectedBody != null)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				fixedJoint.connectedBody = null;
				fixedJoint.enabled = false;
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		GameObject colliderObject = collision.gameObject;
		if(colliderObject.tag == "Draggable")
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				fixedJoint.enabled = true;
				fixedJoint.connectedBody = colliderObject.GetComponent<Rigidbody2D>();
			}
		}
	}
}
