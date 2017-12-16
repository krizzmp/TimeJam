using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerHandler : MonoBehaviour
{
    private enum Layer
    {
        Past,
        Present
    }

    public GameObject layer_past;
    public GameObject layer_present;
    public Material NotEqualOneMaterial;
    public Material RegularMaterial;
    public GameObject[] Backgrounds;
    public GameObject PlayerGameObject;

    private Layer currentLayer;

	private int sort_active_bg = -2;
	private int sort_active_tex_back = -1;
	private int sort_active_tex_main = 0;
	private int sort_active_tex_front = 1;

	private int sort_deactive_bg = -6;
	private int sort_deactive_tex_back = -5;
	private int sort_deactive_tex_main = -4;
	private int sort_deactive_tex_front = -3;

	// Use this for initialization
	void Start()
    {
		SetLayer(Layer.Present);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLayer();
        }

        Vector3 campos = Camera.main.transform.position;
        foreach (GameObject background in Backgrounds)
        {
            Vector3 bgpos = background.transform.position;
            background.transform.position = new Vector3(campos.x, bgpos.y, campos.z);
        }
    }

    public void ToggleLayer()
    {
        if (currentLayer == Layer.Present)
        {
            SetLayer(Layer.Past);
            PlayerGameObject.layer = 8;
        }
        else if (currentLayer == Layer.Past)
        {
            SetLayer(Layer.Present);
            PlayerGameObject.layer = 9;
        }
    }

    private void SetLayer(Layer layer)
    {
        currentLayer = layer;
        if (layer == Layer.Past)
        {
            SetMaterials(layer_past, layer_present);
            Backgrounds[0].GetComponentInChildren<SpriteRenderer>().sortingOrder = sort_active_bg;
            Backgrounds[1].GetComponentInChildren<SpriteRenderer>().sortingOrder = sort_deactive_bg;
        }
        else if (layer == Layer.Present)
        {
            SetMaterials(layer_present, layer_past);
            Backgrounds[0].GetComponentInChildren<SpriteRenderer>().sortingOrder = sort_deactive_bg;
            Backgrounds[1].GetComponentInChildren<SpriteRenderer>().sortingOrder = sort_active_bg;
        }
    }

    private void SetMaterials(GameObject layerActivate, GameObject layerDeactivate)
    {
		TilemapRenderer[] tilemapRenderers = layerActivate.GetComponentsInChildren<TilemapRenderer>();
		foreach(TilemapRenderer tilemapRenderer in tilemapRenderers)
		{
			switch(tilemapRenderer.sortingLayerName)
			{
				case "Back":
					tilemapRenderer.sortingOrder = sort_active_tex_back;
					break;
				case "Main":
					tilemapRenderer.sortingOrder = sort_active_tex_main;
					break;
				case "Front":
					tilemapRenderer.sortingOrder = sort_active_tex_front;
					break;
			}

		}

		TilemapRenderer[] _tilemapRenderers = layerDeactivate.GetComponentsInChildren<TilemapRenderer>();
		foreach(TilemapRenderer tilemapRenderer in tilemapRenderers)
		{
			switch(tilemapRenderer.sortingLayerName)
			{
				case "Back":
					tilemapRenderer.sortingOrder = sort_deactive_tex_back;
					break;
				case "Main":
					tilemapRenderer.sortingOrder = sort_deactive_tex_main;
					break;
				case "Front":
					tilemapRenderer.sortingOrder = sort_deactive_tex_front;
					break;
			}
		}


		SpriteRenderer[] spriteRenderers = layerActivate.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.material = NotEqualOneMaterial;

			switch(spriteRenderer.sortingLayerName)
			{
				case "Back":
					spriteRenderer.sortingOrder = sort_active_tex_back;
					break;
				case "Main":
					spriteRenderer.sortingOrder = sort_active_tex_main;
					break;
				case "Front":
					spriteRenderer.sortingOrder = sort_active_tex_front;
					break;
			}
			
        }
        SpriteRenderer[] _spriteRenderers = layerDeactivate.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.material = RegularMaterial;

			switch(spriteRenderer.sortingLayerName)
			{
				case "Back":
					spriteRenderer.sortingOrder = sort_deactive_tex_back;
					break;
				case "Main":
					spriteRenderer.sortingOrder = sort_deactive_tex_main;
					break;
				case "Front":
					spriteRenderer.sortingOrder = sort_deactive_tex_front;
					break;
			}
		}
    }

    public void ChangeLayerForGameObject(GameObject otherGameObject)
    {
        if (otherGameObject.transform.parent == layer_present.transform)
        {
            otherGameObject.transform.parent = layer_past.transform;
            otherGameObject.layer = layer_past.layer;
        }
        else
        {
            otherGameObject.transform.parent = layer_present.transform;
            otherGameObject.layer = layer_present.layer;
        }
        SpriteRenderer[] spriteRenderers = otherGameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.material =
                spriteRenderer.material == NotEqualOneMaterial ? RegularMaterial : NotEqualOneMaterial;
            spriteRenderer.sortingOrder = spriteRenderer.sortingOrder == sort_active_tex_main ? sort_deactive_tex_main : sort_active_tex_main;
        }
    }
}