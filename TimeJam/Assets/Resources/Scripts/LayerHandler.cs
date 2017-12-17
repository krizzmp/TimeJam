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

    public GameObject PortalPrefab;
    public GameObject PortalPlaceHolder;

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
        Instantiate(PortalPrefab, PlayerGameObject.transform.position + new Vector3(2, 0, 0), Quaternion.identity);
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
            spriteRenderer.sortingOrder = spriteRenderer.sortingOrder == sort_active_tex_main
                ? sort_deactive_tex_main
                : sort_active_tex_main;
        }
    }

    #endregion
}