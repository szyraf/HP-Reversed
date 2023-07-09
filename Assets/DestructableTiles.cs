using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class DestructableTiles : MonoBehaviour
{
    public Tilemap destructableTilemap;

    public AstarPath activeAstar;

    private int updateCount = 0;

    public GameManager gameManager;

    void Start()
    {
        destructableTilemap = GetComponent<Tilemap>();
        gameManager.UpdateWallsLeftText();
    }

    public void Hit(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;

        foreach (ContactPoint2D hit in collision.contacts)
        {
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
            destructableTilemap.SetTile(destructableTilemap.WorldToCell(hitPosition), null);
            FindObjectOfType<AudioManager>().PlayOneShot("pop");
        }

        updateCount = 15;

        AstarPath.active.Scan();

        gameManager.UpdateWallsLeftText();
    }

    private void Update()
    {
        if (updateCount > 0)
        {
            AstarPath.active.Scan();
            updateCount--;
        }
    }

    public int GetAmountOfTiles()
    {
        destructableTilemap.CompressBounds();
        int amount = 0;
        foreach (var pos in destructableTilemap.cellBounds.allPositionsWithin)
        {
            Tile tile = destructableTilemap.GetTile<Tile>(pos);
            if (tile != null) { amount += 1; }
        }

        return amount;
    }
}

