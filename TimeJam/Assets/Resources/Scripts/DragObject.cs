using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
	private FixedJoint2D fixedJoint;
	private bool isOnDraggableObject;
	private bool canDrag;

	private Collider2D draggableCollider;

	// Use this for initialization
	void Start ()
	{
		fixedJoint = GetComponent<FixedJoint2D>();
		fixedJoint.enabled = false;
		isOnDraggableObject = false;
		canDrag = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(draggableCollider != null)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				ToggleJoint();
			}
		}
	}

	private void ToggleJoint()
	{
		if(canDrag)
		{
			canDrag = false;
			fixedJoint.enabled = true;
			fixedJoint.connectedBody = draggableCollider.GetComponent<Rigidbody2D>();
		}
		else
		{
			canDrag = true;
			fixedJoint.enabled = false;
			fixedJoint.connectedBody = null;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Draggable")
		{
			isOnDraggableObject = true;
			draggableCollider = collider;
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Draggable")
		{
			isOnDraggableObject = false;
			draggableCollider = null;
		}
	}

	private void OnGUI()
	{
		if(canDrag && isOnDraggableObject)
		{
			Texture tex_clickToDrag = Resources.Load<Texture>("Textures/Tex_ClickToDrag");

			float imageScale = 3;
			float imageWidth = tex_clickToDrag.width * imageScale;
			float imageHeight = tex_clickToDrag.height * imageScale;
			float imageCenterX = imageWidth / 2;
			float imageCenterY = 100 + (imageHeight / 2);

			GUI.DrawTexture(new Rect((Screen.width / 2) - imageCenterX, (Screen.height / 2) - imageCenterY, imageWidth, imageHeight), tex_clickToDrag, ScaleMode.StretchToFill, true, 10.0F);
		}
	}
}
