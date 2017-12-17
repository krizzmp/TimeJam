using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering;

public class LayerHandler : MonoBehaviour
{
    private enum Layer
    {
        Past,
        Present,
		All
    }

    public GameObject layer_past;
    public GameObject layer_present;
    public Material NotEqualOneMaterial;
    public Material RegularMaterial;
    public GameObject[] Backgrounds;
    public GameObject PlayerGameObject;

    public GameObject PortalPrefab;
    public GameObject PortalPlaceHolder;

    private Layer currentLayer;
    private bool StoneIsInPast;

    // Use this for initialization
    void Start()
    {
		currentLayer = Layer.Past;
		ToggleLayer();
		//Instantiate(PortalPrefab, PlayerGameObject.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLayer();
        }
        if (Input.GetMouseButtonDown(0))
        {
            PortalPlaceHolder.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("sets transform");
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PortalPlaceHolder.transform.position = new Vector3(Mathf.FloorToInt(screenToWorldPoint.x) + 0.5f,
                Mathf.FloorToInt(screenToWorldPoint.y) + 0.5f, 0);
            ;
        }
        if (Input.GetMouseButtonUp(0))
        {
            PortalPlaceHolder.SetActive(false);
            foreach (GameObject portal in GameObject.FindGameObjectsWithTag("Portal"))
            {
                Destroy(portal);
            }
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(PortalPrefab, new Vector3(Mathf.FloorToInt(screenToWorldPoint.x) + 0.5f,
                Mathf.FloorToInt(screenToWorldPoint.y) + 0.5f, 0), Quaternion.identity);
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
		if(currentLayer == Layer.Present)
		{
			PlayerGameObject.layer = 10;
			currentLayer = Layer.Past;
			SwitchActiveLayer(layer_past, layer_present);
		}
		else if(currentLayer == Layer.Past)
		{
			PlayerGameObject.layer = 11;
			currentLayer = Layer.Present;
			SwitchActiveLayer(layer_present, layer_past);
		}
	}

	private void SwitchActiveLayer(GameObject active, GameObject inactive)
	{
		active.GetComponent<SortingGroup>().sortingOrder = -1;
		inactive.GetComponent<SortingGroup>().sortingOrder = -2;

		Material activeMaterial = NotEqualOneMaterial;
		Material inActiveMaterial = RegularMaterial;

		foreach(TilemapRenderer tmr in active.GetComponentsInChildren<TilemapRenderer>())
		{
			tmr.material = activeMaterial;
		}

		foreach(SpriteRenderer spr in active.GetComponentsInChildren<SpriteRenderer>())
		{
			spr.material = activeMaterial;
		}

		//Inactive
		foreach(TilemapRenderer tmr in inactive.GetComponentsInChildren<TilemapRenderer>())
		{
			tmr.material = RegularMaterial;
		}

		foreach(SpriteRenderer spr in inactive.GetComponentsInChildren<SpriteRenderer>())
		{
			spr.material = RegularMaterial;
		}
	}

	#region MyRegion

    public void ChangeLayerForGameObject(GameObject stone)
    {
        Material activeMaterial = NotEqualOneMaterial;
        Material inActiveMaterial = RegularMaterial;

        GameObject Present_Stone = GameObject.Find("Stone");
  
        Present_Stone.GetComponent<SpriteRenderer>().material = inActiveMaterial;
        //Present_Stone.layer = 9;
		if(Present_Stone.layer == 8)
		{
			Present_Stone.layer = 13;
		}
		else if(Present_Stone.layer == 9)
		{
			Present_Stone.layer = 12;
		}
	}

    #endregion

    public void ChangeLayerForExitGameObject(GameObject stone)
    {
        Material activeMaterial = NotEqualOneMaterial;
        Material inActiveMaterial = RegularMaterial;
        StoneIsInPast = !StoneIsInPast;

        GameObject Present_Stone = GameObject.Find("Stone");
        if (StoneIsInPast && currentLayer == Layer.Past || !StoneIsInPast && currentLayer == Layer.Present)
        {
            Present_Stone.layer = 8;
            Present_Stone.GetComponent<SpriteRenderer>().material = activeMaterial;
        }
        else
        {
            Present_Stone.layer = 9;
            Present_Stone.GetComponent<SpriteRenderer>().material = inActiveMaterial;
        }
        if (StoneIsInPast)
        {
            Present_Stone.layer = 8;
			stone.transform.SetParent(layer_past.transform);

		}
        else
        {
            Present_Stone.layer = 9;
			stone.transform.SetParent(layer_present.transform);
		}
    }
}