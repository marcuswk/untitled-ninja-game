using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour
{
    private float projectileSpeed = 200f;
    private Rigidbody rb;
    public float zBoundary = 50;
    public bool isFlying = true;
    public Transform playerTransform;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").transform;
        //direction = playerTransform.forward; //set in playerController
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            //rb.AddForce(Vector3.forward * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
            rb.AddForce(direction * projectileSpeed * Time.deltaTime, ForceMode.Impulse);

            //rotate the ninjastar
            transform.Rotate(Vector3.up * 100, Time.deltaTime * 200);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

            //destroy if it leaves the playfield
            if (transform.position.z > zBoundary)
        {
            Destroy(gameObject);
        }
    }
}
