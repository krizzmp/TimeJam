using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour {

	private const int MAX_INVENTORY_SIZE = 6;
	private Texture[] inventory;


	// Use this for initialization
	void Start ()
	{
		inventory = new Texture[MAX_INVENTORY_SIZE];
		inventory[0] = Resources.Load<Texture>("Textures/Tex_InventoryItem_Sword");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void OnGUI()
	{
		DrawInventory();
	}

	private void DrawInventory()
	{
		Texture tex_inventorySpot = Resources.Load<Texture>("Textures/Tex_InventorySpot");
		Texture tex_inventorySpotFrame = Resources.Load<Texture>("Textures/Tex_InventorySpotFrame");

		float imageScale = 3;
		float imageWidth = tex_inventorySpot.width * imageScale;
		float imageHeight = tex_inventorySpot.height * imageScale;
		float imageCenterX = imageWidth / 2;
		float imageCenterY = imageHeight / 2;

		float inventoryWidth = imageWidth * MAX_INVENTORY_SIZE;
		float inventoryStartX = (Screen.width / 2) - (inventoryWidth / 2);
		float inventoryStartY = Screen.height - (imageHeight * 2);

		for(int i = 0; i < MAX_INVENTORY_SIZE; i++)
		{

			Texture tex_item = inventory[i];
			if(tex_item == null)
			{
				tex_item = tex_inventorySpot;
			}

			float offsetX = imageWidth * i;

			float x = inventoryStartX + offsetX;
			float y = inventoryStartY + imageCenterY;
			Rect inventoryGUI = new Rect(x, y, imageWidth, imageHeight);
			GUI.DrawTexture(inventoryGUI, tex_item, ScaleMode.StretchToFill, true, 10.0F);
			GUI.DrawTexture(inventoryGUI, tex_inventorySpotFrame, ScaleMode.StretchToFill, true, 10.0F);
		}
	}

	private void AddItemToInventory(Texture texture)
	{
		for(int i = 0; i < MAX_INVENTORY_SIZE; i++)
		{
			if(inventory[i] == null)
			{
				inventory[i] = texture;
				break;
			}
		}
	}

}
