using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OnOffPathfinding : MonoBehaviour
{
    public Transform player;
    public Transform enemy;

    public static bool enemyPathfinding = true;
    private bool isE = false;

    public AIDestinationSetter destinationSetter;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isE)
            {
                enemyPathfinding = !enemyPathfinding;
                if (enemyPathfinding)
                {
                    destinationSetter.target = player;
                }
                else
                {
                    destinationSetter.target = enemy;
                }
            }
            isE = true;
        }
        else
        {
            isE = false;
        }
        */
    }
}
