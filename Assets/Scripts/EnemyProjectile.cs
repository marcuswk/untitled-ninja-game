using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float projectileSpeed = 100f;
    private Rigidbody rb;
    public Transform playerTransform;
    public Vector3 direction;

    public bool isFlying = true;
    public bool rotateWeapon = true;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").transform;
        direction = (playerTransform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            rb.AddForce(direction * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
            //rotate the ninjastar if rotateWeapon = true;
            if (rotateWeapon)
            {
                transform.Rotate(Vector3.up * 100, Time.deltaTime * 200);
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }
}
