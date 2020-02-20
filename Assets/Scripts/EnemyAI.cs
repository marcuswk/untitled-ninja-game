using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    private Vector3 GetRoamingPosition()
    {
       return startPos + GetRandomDir() * Random.Range(10f, 70f);
    }
    //Generate a random and normalised direction
    public static Vector3 GetRandomDir()
    {
        //random on the x and y only
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

}

