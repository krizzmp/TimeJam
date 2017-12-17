using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
	private FixedJoint2D fixedJoint;
	private bool isOnPlayer;
	private bool showText;

	private Collider2D playerCollider;
	private Rigidbody2D body;

	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody2D>();
		fixedJoint = GetComponent<FixedJoint2D>();
		fixedJoint.enabled = false;
		isOnPlayer = false;
		showText = true;
		body.mass = 10;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(playerCollider != null)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				ToggleJoint();
			}
		}
	}

	private void ToggleJoint()
	{
		if(showText)
		{
			showText = false;
			fixedJoint.enabled = true;
			fixedJoint.connectedBody = playerCollider.GetComponent<Rigidbody2D>();

			body.mass = 2;
		}
		else
		{
			showText = true;
			fixedJoint.enabled = false;
			fixedJoint.connectedBody = null;

			body.mass = 10;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			isOnPlayer = true;
			playerCollider = collider;
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Draggable")
		{
			isOnPlayer = false;
			playerCollider = null;
		}
	}

	private void OnGUI()
	{
		if(showText && isOnPlayer)
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
