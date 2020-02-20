using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //Set up vars
    public float speed = 500.0f;
    private Rigidbody playerRb;
    private float zBoundary = 30;
    private float xBoundary = 35;
    public bool firingUp = false;

    public GameObject ninjaStar;
    public Transform firePoint;
    public NinjaStar ninjaStarScript;
    public LookTowardMouse lookTowardMouse;

    //rotation vars
    Quaternion targetRotation;
    //public Transform target;            // target to rotate towards
    public float turnSpeed = 2F;          // speed scaling factor
    bool rotating = false;              // toggles the rotation, after targeting, toggle true, false after arrives
    float rotationTime; // when rotationTime == 1, will have rotated to our target
    //end of rotation vars



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        ninjaStarScript = ninjaStar.GetComponent<NinjaStar>();
        lookTowardMouse = GetComponent<LookTowardMouse>();
        firePoint = transform.Find("FirePoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        ConstrainMovement();
        ShootNinjaStarUp();
        ShootNinjaStarInPlayerDirection();
        DashOnSpace();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Move the Player
    void MovePlayer()
    {
        //move player in all directions
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //current settings for movement: speed = 500, |RIGIDBODY drag = 20, angular drag = 23| |INPUT SETTINGS gravity = 14, sensitivity = 12|
        playerRb.AddForce(Vector3.forward * speed * Time.deltaTime * verticalInput, ForceMode.VelocityChange);
        playerRb.AddForce(Vector3.right * speed * Time.deltaTime * horizontalInput, ForceMode.VelocityChange);
    }

    // Constrain Movement - invisible walls to stop player moving off ground plane
    void ConstrainMovement()
    {
        //Set boundary (for now)
        if (transform.position.z < -zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        }

        if (transform.position.z > zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        }

        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }
    }



    void ShootNinjaStarInPlayerDirection()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ninjaStarScript.direction = transform.forward;
            Instantiate(ninjaStar, firePoint.transform.position, transform.rotation);
        }
    }

    void ShootNinjaStarUp()
    {
        
        if (Input.GetButtonDown("Fire2"))
        {
            // turn off mouse look for a sec
            lookTowardMouse.mouseLookEnabled = false;
            //Rotate the dude to forwards
            //Vector3 relativePosition = target.position - transform.position; -->not needed for turn forwards, use Vector3.forward, Vector3.up instead
            targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            rotating = true;
            rotationTime = 0;

            //Shoot the Ninja Star forward
            ninjaStarScript.direction = Vector3.forward; // Set direction in ninjastar script
            Instantiate(ninjaStar, firePoint.transform.position, transform.rotation);
        }
        if (rotating)
        {
            rotationTime += Time.deltaTime * turnSpeed;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime);
        }
        if (rotationTime > 1)
        {
            rotating = false;
            //turn mouselook back on!
            lookTowardMouse.mouseLookEnabled = true;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy: " + collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
        }
    }

    private void DashOnSpace()
    //NOTE: This wasn't working due to the animation on the Synty (unity asset store) assets used. 
    //Not entirely sure why, but disabling "Apply Root Motion" in the editor has solved this for now.
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 facingdirection = transform.forward;
            //transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.forward, 100);
            playerRb.AddForce(facingdirection * speed * 1000 * Time.deltaTime, ForceMode.Impulse);
            Debug.Log("Trying to Dash!");
        }
    }



}
