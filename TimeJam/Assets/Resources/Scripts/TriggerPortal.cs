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
        //var other = GameObject.FindGameObjectWithTag("Portal").GetComponentInChildren<Collider2D>();
        //var player = GameObject.FindGameObjectWithTag("Player");
        //if (other.OverlapPoint(player.transform.position))
        //{
        //    Debug.Log("Player is in Portal");
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Draggable") && other.isTrigger)
        {
            layerHandler.ChangeLayerForExitGameObject(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Change The World");
            //layerHandler.ToggleLayer();
            this.transform.parent.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Draggable") && other.isTrigger)
        {
            
            Debug.Log("This a Stone.");
            //other.gameObject.layer = 10;
            layerHandler.ChangeLayerForGameObject(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Change The World");
            layerHandler.ToggleLayer();
            this.transform.parent.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }

    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    throw new System.NotImplementedException();
    //}
}