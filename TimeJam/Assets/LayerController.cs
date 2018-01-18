using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class LayerController : MonoBehaviour
{
    public Layer Layer;

    private Layer currentLayer;
    private LayerHandler layerHandler;

    // Use this for initialization
    void Start()
    {
        layerHandler = FindObjectOfType<LayerHandler>();
        layerHandler.LayerChanged += OnLayerChanged;
    }

    private void OnLayerChanged(Layer layer)
    {
        currentLayer = layer;
        if (currentLayer == this.Layer)
        {
            this.GetComponent<SortingGroup>().sortingOrder = -1;
            SetMat(this.gameObject, layerHandler.NotEqualOneMaterial);
        }
        else
        {
            this.GetComponent<SortingGroup>().sortingOrder = -2;
            SetMat(this.gameObject, layerHandler.RegularMaterial);
        }
    }
    private static void SetMat(GameObject layer, Material material)
    {
        foreach (TilemapRenderer tmr in layer.GetComponentsInChildren<TilemapRenderer>())
        {
            tmr.material = material;
        }

        foreach (SpriteRenderer spr in layer.GetComponentsInChildren<SpriteRenderer>())
        {
            spr.material = material;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}