using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPortal : MonoBehaviour
{
    private LayerHandler layerHandler;

    // Use this for initialization
    void Start()
    {
        layerHandler = GameObject.FindObjectOfType<LayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Draggable") && other.isTrigger)
        {
            
            Debug.Log("This a Stone.");
            
            layerHandler.ChangeLayerForGameObject(other.gameObject);
        }
        else if(other.CompareTag("Player"))
        {
            Debug.Log("Change The World");
            layerHandler.ToggleLayer();
        }
    }
}