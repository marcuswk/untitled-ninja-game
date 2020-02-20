using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject player;
    public int hits = 0;
    public float deathVelocity = 15f;
    public GameObject animatedModelSpine;
    public GameObject ragdollSpine;
    public GameObject ragdoll;
    public GameObject animatedModel;
    public bool alive = true;
    public Rigidbody enemyRb;
 



    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        //getting spine to attach ninja stars to - using .Find on the transform to get only THIS object's child
        //maybe just drag it in on the inspector next time eh?
        //spine = transform.Find("AnimatedModel").Find("mixamorig:Hips").Find("mixamorig:Spine").gameObject;   
        enemyRb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        ragdoll.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (hits > 2 && alive)
        {
            Die();
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            //knockback
            gameObject.GetComponent<Rigidbody>().velocity = ((transform.position - player.transform.position).normalized * 100);

            //Experimenting with getting the ninjastars to stick to the enemy. 
            //This is paired with 'isFlying' bool on the ninjaStar script, which stops its velocity and 
            //angular velocity. 
            other.gameObject.GetComponent<NinjaStar>().isFlying = false;
            other.gameObject.transform.SetParent(animatedModelSpine.transform);

            //randomise the position and rotation for comedic effect.
            other.gameObject.transform.position = animatedModelSpine.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, -0.6f));
            other.gameObject.transform.Rotate(0f, 0f, Random.Range(-30f, 30f));
            
            //Set to Kinematic
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            
            //destroy the ninja star's collider, stop weird interactions between other enemies and other ninja stars
            Destroy(other.gameObject.GetComponent<Collider>());

            //increase hit count
            hits++;
        }

    }

    private void Die()
    {
        //copy transform data to ragdoll
        CopyTransformData(animatedModel.transform, ragdoll.transform, (GetComponent<Rigidbody>().velocity).normalized * deathVelocity);

        //set ninjastar children to new parent on ragdoll
        CopyProjectileChildren(animatedModelSpine.transform, ragdollSpine.transform);

        //a bit of random rotation for variety
        //var randomRotation = Quaternion.Euler(Random.Range(-90, 90), Random.Range(-45, 45), Random.Range(-30, 30));
        var randomRotation = Quaternion.Euler(Random.Range(30,-90), Random.Range(60, -60), Random.Range(60,-60));
        transform.Rotate(randomRotation.eulerAngles); 

        //turn off box collider on parent object
        GetComponent<BoxCollider>().enabled = false;

        //Activate ragdoll and deactivate animated model
        ragdoll.gameObject.SetActive(true);
        animatedModel.gameObject.SetActive(false);

        //set alive to false so script doesn't repeat. This will stop the enemy 
        //chasing in the movement script too.
        alive = false;

    }

 

    private void CopyTransformData(Transform sourceTransform, Transform DestinationTransform, Vector3 velocity)
    {
        if (sourceTransform.childCount != DestinationTransform.childCount)
        {
            Debug.LogWarning("Invalid Transform Copy, number of children in hierarchies do not match");
            return;
        }

        for (int i = 0; i < sourceTransform.childCount; i++)
        {
            var source = sourceTransform.GetChild(i);
            var destination = DestinationTransform.GetChild(i);
            destination.position = source.position;
            destination.rotation = source.rotation;
            var rb = destination.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.velocity = velocity;
            }
            CopyTransformData(source, destination, velocity);
        }
    }

    private void CopyProjectileChildren(Transform sourceTransform, Transform destinationTransform)
    {
        Transform[] children = sourceTransform.GetComponentsInChildren<Transform>();
        
        foreach (Transform child in children)
        {
            if (child.gameObject.CompareTag("Projectile"))
            {
                child.SetParent(destinationTransform);
            }
        }
    }


}
