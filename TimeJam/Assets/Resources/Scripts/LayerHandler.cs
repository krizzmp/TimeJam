using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering;

public enum Layer
{
    Past,
    Present
}

public class LayerHandler : MonoBehaviour
{
    public GameObject layer_past;
    public GameObject layer_present;
    public Material NotEqualOneMaterial;
    public Material RegularMaterial;

    public GameObject PortalPrefab;
    public GameObject PortalPlaceHolder;

    public Layer CurrentLayer;
    private GameObject Player;

    public event Action<Layer> LayerChanged;

    private float delta = 0f;

    private GameObject portalGameObject;

    // Use this for initialization
    void Start()
    {
        CurrentLayer = Layer.Past;
        //ToggleLayer();
        Invoke("LayerIsUpdated", 0.01f);
        //LayerIsUpdated();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
  
        //var dist = 2;
        //var mag = new Vector2(Input.GetAxis("Right Analog X"), Input.GetAxis("Right Analog Y"));
        //var pointOnCircle = Player.transform.position + new Vector3(mag.x, mag.y) * dist;
        //if (mag.magnitude > 0.1)
        //{
        //    PortalPlaceHolder.SetActive(true);

        //    PortalPlaceHolder.transform.position = new Vector3(pointOnCircle.x, pointOnCircle.y, 0);
        //}
        //else
        //{
        //    PortalPlaceHolder.SetActive(false);
        //}
        //if (Input.GetButton("Switch"))
        //{
        //    if (mag.magnitude > 0.1)
        //    {
        //        if (portalGameObject != null)
        //        {
        //            portalGameObject.transform.position = new Vector3(pointOnCircle.x, pointOnCircle.y, 0);
        //        }
        //        else
        //        {
        //            portalGameObject = Instantiate(PortalPrefab, new Vector3(pointOnCircle.x, pointOnCircle.y, 0), Quaternion.identity);
        //        }
        //    }
        //    else
        //    {
        //        ClearPortals();
        //    }
        //    //ToggleLayer();
        //}
        if (Input.GetMouseButtonDown(0))
        {
            PortalPlaceHolder.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PortalPlaceHolder.transform.position = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
        }
        if (Input.GetMouseButtonUp(0))
        {
            ClearPortals();
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(PortalPrefab, new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0), Quaternion.identity);
        }
        if (Input.GetMouseButtonUp(1))
        {
            ClearPortals();
        }
    }

    private void ClearPortals()
    {
        PortalPlaceHolder.SetActive(false);
        foreach (GameObject portal in GameObject.FindGameObjectsWithTag("Portal"))
        {
            Destroy(portal);
        }
    }

    public void ToggleLayer()
    {
        switch (CurrentLayer)
        {
            case Layer.Present:
                CurrentLayer = Layer.Past;
                LayerIsUpdated();
                break;
            case Layer.Past:
                CurrentLayer = Layer.Present;
                LayerIsUpdated();
                break;
        }
    }


    void LayerIsUpdated()
    {
        if (LayerChanged != null)
        {
            LayerChanged(CurrentLayer);
        }
    }
}