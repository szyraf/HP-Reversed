using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class NormalWalls : MonoBehaviour
{
    public Tilemap normalTilemap;

    public AstarPath activeAstar;

    private int updateCount = 0;

    void Start()
    {
        normalTilemap = GetComponent<Tilemap>();
    }

    public void Hit(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;

        foreach (ContactPoint2D hit in collision.contacts)
        {
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
            normalTilemap.SetTile(normalTilemap.WorldToCell(hitPosition), null);

            hitPosition.x = hit.point.x + 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y + 0.01f * hit.normal.y;
            normalTilemap.SetTile(normalTilemap.WorldToCell(hitPosition), null);

            FindObjectOfType<AudioManager>().PlayOneShot("pop");
        }

        updateCount = 15;

        AstarPath.active.Scan();
    }

    private void Update()
    {
        if (updateCount > 0)
        {
            AstarPath.active.Scan();
            updateCount--;
        }
    }
}

