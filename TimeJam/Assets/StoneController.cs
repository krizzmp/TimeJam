using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    public Layer Layer;
    private LayerHandler layerHandler;

    // Use this for initialization
    void Start()
    {
        layerHandler = FindObjectOfType<LayerHandler>();
        UpdateLayer();
    }
    

    // Update is called once per frame
    void Update()
    {
    }

    [UsedImplicitly]
    public void OnExitPortal()
    {
        switch (this.Layer)
        {
            case Layer.Past:
                this.Layer = Layer.Present;
                break;
            case Layer.Present:
                this.Layer = Layer.Past;
                break;
        }

        UpdateLayer();
    }

    private void UpdateLayer()
    {

        Material activeMaterial = layerHandler.NotEqualOneMaterial;
        Material inActiveMaterial = layerHandler.RegularMaterial;
        if (Layer == layerHandler.CurrentLayer)
        {
            this.gameObject.layer = 8;
            this.GetComponent<SpriteRenderer>().material = activeMaterial;
        }
        else
        {
            this.gameObject.layer = 9;
            this.GetComponent<SpriteRenderer>().material = inActiveMaterial;
        }
        switch (Layer)
        {
            case Layer.Past:
                this.gameObject.layer = 8;
                this.transform.SetParent(layerHandler.layer_past.transform);
                break;
            case Layer.Present:
                this.gameObject.layer = 9;
                this.transform.SetParent(layerHandler.layer_present.transform);
                break;
        }
    }

    [UsedImplicitly]
    public void OnEnterPortal()
    {
        Material activeMaterial = layerHandler.NotEqualOneMaterial;
        Material inActiveMaterial = layerHandler.RegularMaterial;

        this.gameObject.GetComponent<SpriteRenderer>().material = inActiveMaterial;
        switch (this.gameObject.layer)
        {
            case 8:
                this.gameObject.layer = 13;
                break;
            case 9:
                this.gameObject.layer = 12;
                break;
        }
    }
}