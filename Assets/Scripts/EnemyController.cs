using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private Transform target;
    private Animator animator;
    private EnemyDeath enemyDeath;
    private EnemyProjectile enemyProjectileScript;
    private Transform animatedModelTransform;
    

    //movement
    private float rotateSpeed = 1f;
    public float speed = 10000;
    public float lookRadius = 10f;
    //public bool alive = true; -this is in enemyDeath script
    public bool targetAquired;

    //shooting
    private float timeBetweenShots;
    private float startTimeBetweenShots;
    public GameObject projectile;
    public bool shooting = false;
    private Transform firePoint;




    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        enemyDeath = GetComponent<EnemyDeath>();
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        target = player.transform;
        timeBetweenShots = startTimeBetweenShots;
        enemyProjectileScript = projectile.GetComponent<EnemyProjectile>();
        animatedModelTransform = transform.Find("AnimatedModel");
        firePoint = transform.Find("FirePoint").transform;
        animator.SetBool("isIdle", true);
        startTimeBetweenShots = Random.Range(2f, 5f);



    }

    private void Awake()
    {
        
    }

    void Update()
    {
        
        ChasePlayer();
        DieAfterFall();
        Shoot();

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void FaceTarget(float turnSpeed)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //slerp around to face target over time for smoothnessssss
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

  

    void ChasePlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            targetAquired = true;
        }


        if (enemyDeath.alive && targetAquired && !shooting)
        {
            animator.SetBool("isIdle", false);
            FaceTarget(3f);
            Vector3 moveTo = (player.transform.position - transform.position).normalized;
            rb.AddForce(moveTo * speed * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    void DieAfterFall()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
            Debug.Log("I'm dead arrrgfhfhfhfh");
        }
    }
    
    void Shoot()
    {
        //bool animationState = animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking");
        if (timeBetweenShots <= 0 && enemyDeath.alive && targetAquired)
        {
            shooting = true;
            
            animator.SetBool("isAttacking", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0, -5, 0), 0.5f);
            //FaceTargetNow();
            //stop for a moment


            //turn to correct angle for animation FaceTarget(0.5f);

            //Projectile is fired using animation event

        }
        else
        {
            timeBetweenShots -= Time.deltaTime;

        }
    }

    public void LaunchProjectile()
    {

       
        Instantiate(projectile, firePoint.position, Quaternion.identity);
        timeBetweenShots = startTimeBetweenShots;

    }

    public void ShootingEnds()
    {
        animator.SetBool("isAttacking", false);
        //animator.SetBool("isIdle", false);
        shooting = false;
    }


}
