using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AnimationCurve myCurve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate, for fun and science
        transform.Rotate(Vector3.up * 60, Time.deltaTime * 60);
        
        //Bob up and down with an animation curve! Cooooooolll!!!
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)) + 1, transform.position.z);
    }
}
