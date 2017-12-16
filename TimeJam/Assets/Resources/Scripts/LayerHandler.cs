using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Layer currentLayer = Layer.Past;

    // Use this for initialization
    void Start()
    {
        //SetActiveLayer(Layer.Present);
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
            Backgrounds[0].GetComponentInChildren<SpriteRenderer>().sortingOrder = -2;
            Backgrounds[1].GetComponentInChildren<SpriteRenderer>().sortingOrder = -3;
        }
        else if (layer == Layer.Present)
        {
            SetMaterials(layer_present, layer_past);
            Backgrounds[0].GetComponentInChildren<SpriteRenderer>().sortingOrder = -3;
            Backgrounds[1].GetComponentInChildren<SpriteRenderer>().sortingOrder = -2;
        }
    }

    private void SetMaterials(GameObject layerActivate, GameObject layerDeactivate)
    {
        SpriteRenderer[] spriteRenderers = layerActivate.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.material = NotEqualOneMaterial;
            spriteRenderer.sortingOrder = -1;
        }
        SpriteRenderer[] _spriteRenderers = layerDeactivate.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.material = RegularMaterial;
            spriteRenderer.sortingOrder = -2;
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
            spriteRenderer.sortingOrder = spriteRenderer.sortingOrder == -1 ? -2 : -1;
        }
    }
}