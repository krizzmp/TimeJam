using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] Backgrounds;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Camera.main.transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            Camera.main.transform.position.z
        );

        Vector3 campos = Camera.main.transform.position;

        foreach (GameObject background in Backgrounds)
        {
            Vector3 bgpos = background.transform.position;
            background.transform.position = new Vector3(campos.x, bgpos.y, campos.z);
        }
    }
}