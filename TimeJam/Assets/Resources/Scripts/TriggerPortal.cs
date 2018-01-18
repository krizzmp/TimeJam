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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Draggable") && other.isTrigger)
        {
            other.SendMessage("OnExitPortal");
        }
        else if (other.CompareTag("Player"))
        {
            this.transform.parent.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Draggable") && other.isTrigger)
        {
            other.SendMessage("OnEnterPortal");
        }
        else if (other.CompareTag("Player"))
        {
            layerHandler.ToggleLayer();
            this.transform.parent.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }

    }
}