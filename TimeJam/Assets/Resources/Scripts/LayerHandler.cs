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
		GoToPresent();
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

    void GoToPresent()
    {
        currentLayer = Layer.Present;

        PlayerGameObject.layer = 9;

        Material ActiveMaterial = NotEqualOneMaterial;
        Material InActiveMaterial = RegularMaterial;

        GameObject Background_Present = GameObject.Find("Background_Present");
        Background_Present.GetComponentInChildren<SpriteRenderer>().material = ActiveMaterial;
        Background_Present.GetComponentInChildren<SpriteRenderer>().sortingOrder = -2;

        GameObject Present_Background = GameObject.Find("Present_Background");
        Present_Background.GetComponent<TilemapRenderer>().material = ActiveMaterial;
        Present_Background.GetComponent<TilemapRenderer>().sortingOrder = -1;

        GameObject Present_Main = GameObject.Find("Present_Main");
        Present_Main.GetComponent<TilemapRenderer>().material = ActiveMaterial;
        Present_Main.GetComponent<TilemapRenderer>().sortingOrder = 0;

        GameObject Present_Foreground = GameObject.Find("Present_Foreground");
        Present_Foreground.GetComponent<TilemapRenderer>().material = ActiveMaterial;
        Present_Foreground.GetComponent<TilemapRenderer>().sortingOrder = 1;


		GameObject Background_Past = GameObject.Find("Background_Past");
        Background_Past.GetComponentInChildren<SpriteRenderer>().material = InActiveMaterial;
        Background_Past.GetComponentInChildren<SpriteRenderer>().sortingOrder = -6;

        GameObject Past_Background = GameObject.Find("Past_Background");
        Past_Background.GetComponent<TilemapRenderer>().material = InActiveMaterial;
        Past_Background.GetComponent<TilemapRenderer>().sortingOrder = -5;

        GameObject Past_Main = GameObject.Find("Past_Main");
        Past_Main.GetComponent<TilemapRenderer>().material = InActiveMaterial;
        Past_Main.GetComponent<TilemapRenderer>().sortingOrder = -4;

        GameObject Past_Foreground = GameObject.Find("Past_Foreground");
        Past_Foreground.GetComponent<TilemapRenderer>().material = InActiveMaterial;
        Past_Foreground.GetComponent<TilemapRenderer>().sortingOrder = -3;

		//Other objects
		GameObject Present_Stone = GameObject.Find("Stone");
		Present_Stone.GetComponent<SpriteRenderer>().material = ActiveMaterial;
		Present_Stone.GetComponent<SpriteRenderer>().sortingOrder = 0;

		GameObject Past_Sword = GameObject.Find("Item_Sword");
		if(Past_Sword != null)
		{
			Past_Sword.GetComponent<SpriteRenderer>().material = InActiveMaterial;
			Past_Sword.GetComponent<SpriteRenderer>().sortingOrder = -4;
		}

		// Ladders
		GameObject Present_Ladder = GameObject.Find("Present_Ladder");
		SpriteRenderer[] Present_Ladder_renderers = Present_Ladder.GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer spriteRenderer in Present_Ladder_renderers)
		{
			spriteRenderer.material = ActiveMaterial;
			spriteRenderer.sortingOrder = 0;
		}

		GameObject Past_Ladder = GameObject.Find("Past_Ladder");
		SpriteRenderer[] Past_Ladder_renderes = Past_Ladder.GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer spriteRenderer in Past_Ladder_renderes)
		{
			spriteRenderer.material = InActiveMaterial;
			spriteRenderer.sortingOrder = -4;
		}
	}
    void GoToPast()
    {
        currentLayer = Layer.Past;

        PlayerGameObject.layer = 8;

        Material activeMaterial = NotEqualOneMaterial;
        Material inActiveMaterial = RegularMaterial;

        GameObject Background_Present = GameObject.Find("Background_Present");
        Background_Present.GetComponentInChildren<SpriteRenderer>().material = inActiveMaterial;
        Background_Present.GetComponentInChildren<SpriteRenderer>().sortingOrder = -6;

        GameObject Present_Background = GameObject.Find("Present_Background");
        Present_Background.GetComponent<TilemapRenderer>().material = inActiveMaterial;
        Present_Background.GetComponent<TilemapRenderer>().sortingOrder = -5;

        GameObject Present_Main = GameObject.Find("Present_Main");
        Present_Main.GetComponent<TilemapRenderer>().material = inActiveMaterial;
        Present_Main.GetComponent<TilemapRenderer>().sortingOrder = -4;

        GameObject Present_Foreground = GameObject.Find("Present_Foreground");
        Present_Foreground.GetComponent<TilemapRenderer>().material = inActiveMaterial;
        Present_Foreground.GetComponent<TilemapRenderer>().sortingOrder = -3;



        GameObject Background_Past = GameObject.Find("Background_Past");
        Background_Past.GetComponentInChildren<SpriteRenderer>().material = activeMaterial;
        Background_Past.GetComponentInChildren<SpriteRenderer>().sortingOrder = -2;

        GameObject Past_Background = GameObject.Find("Past_Background");
        Past_Background.GetComponent<TilemapRenderer>().material = activeMaterial;
        Past_Background.GetComponent<TilemapRenderer>().sortingOrder = -1;

        GameObject Past_Main = GameObject.Find("Past_Main");
        Past_Main.GetComponent<TilemapRenderer>().material = activeMaterial;
        Past_Main.GetComponent<TilemapRenderer>().sortingOrder = 0;

        GameObject Past_Foreground = GameObject.Find("Past_Foreground");
        Past_Foreground.GetComponent<TilemapRenderer>().material = activeMaterial;
        Past_Foreground.GetComponent<TilemapRenderer>().sortingOrder = 1;

		//Other objects
		GameObject Present_Stone = GameObject.Find("Stone");
		Present_Stone.GetComponent<SpriteRenderer>().material = inActiveMaterial;
		Present_Stone.GetComponent<SpriteRenderer>().sortingOrder = -4;

		GameObject Past_Sword = GameObject.Find("Item_Sword");
		if(Past_Sword != null)
		{
			Past_Sword.GetComponent<SpriteRenderer>().material = activeMaterial;
			Past_Sword.GetComponent<SpriteRenderer>().sortingOrder = 0;
		}

		// Ladders
		GameObject Present_Ladder = GameObject.Find("Present_Ladder");
		SpriteRenderer[] Present_Ladder_renderers = Present_Ladder.GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer spriteRenderer in Present_Ladder_renderers)
		{
			spriteRenderer.material = inActiveMaterial;
			spriteRenderer.sortingOrder = -4;
		}

		GameObject Past_Ladder = GameObject.Find("Past_Ladder");
		SpriteRenderer[] Past_Ladder_renderes = Past_Ladder.GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer spriteRenderer in Past_Ladder_renderes)
		{
			spriteRenderer.material = activeMaterial;
			spriteRenderer.sortingOrder = 0;
		}
	}

    #region MyRegion

    public void ToggleLayer()
    {
        if (currentLayer == Layer.Present)
        {
            GoToPast();
        }
        else if (currentLayer == Layer.Past)
        {
            GoToPresent();
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
            tilemapRenderer.material = NotEqualOneMaterial;
            switch (tilemapRenderer.sortingLayerName)
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
            tilemapRenderer.material = RegularMaterial;
            switch (tilemapRenderer.sortingLayerName)
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

    #endregion
}