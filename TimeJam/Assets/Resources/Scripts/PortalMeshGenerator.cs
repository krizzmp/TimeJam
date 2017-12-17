using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMeshGenerator : MonoBehaviour
{
    public Material mat;

    public Transform portalTop;
    public Transform portalBottom;
    Vector3 topPos;
    Vector3 botPos;

    private Transform player;

    float width = 2;
    float height = 2;

    Mesh mesh;

    Vector3[] vertices;

    private void Start()
    {
        mesh = new Mesh();

        vertices = new Vector3[4];

        GetComponent<MeshRenderer>().material = mat;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        UpdateRayCast();
    }

    private void UpdateRayCast()
    {
        Vector3 playerPosition = player.position + new Vector3(0, 0.2f, 0);
        var headingTop = portalTop.position - playerPosition;
        var distanceTop = headingTop.magnitude;
        var directionTop = headingTop / distanceTop;

        var headingBot = portalBottom.position - playerPosition;
        var distanceBot = headingBot.magnitude;
        var directionBot = headingBot / distanceBot;

        Ray2D hitTop = new Ray2D();
        hitTop.origin = playerPosition;
        hitTop.direction = directionTop;

        Ray2D hitBot = new Ray2D();
        hitBot.origin = playerPosition;
        hitBot.direction = directionBot;

        Debug.DrawRay(playerPosition, directionTop * 100);
        Debug.DrawRay(playerPosition, directionBot * 100);

        UpdateVertices(hitTop.GetPoint(100), hitBot.GetPoint(100));
    }

    private void UpdateVertices(Vector3 top, Vector3 bot)
    {
        Vector3 topPos = portalTop.localPosition;
        Vector3 botPos = portalBottom.localPosition;

        vertices[1].Set(topPos.x, topPos.y, topPos.z);
        vertices[0].Set(botPos.x, botPos.y, botPos.z);
        vertices[2].Set(top.x, top.y, top.z);
        vertices[3].Set(bot.x, bot.y, bot.z);

        mesh.vertices = vertices;

        mesh.triangles = new int[] {0, 1, 2, 0, 2, 3};


        GetComponent<MeshFilter>().mesh = mesh;
    }
}