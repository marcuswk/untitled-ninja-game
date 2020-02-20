using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour
{
    //this is just here to help access functions on the parent gameobject from the animator on the
    //'AnimatedModel' child

    private EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    void Update()
    {
        
    }

    public void LaunchProjectile()
    {
        enemyController.LaunchProjectile();
    }

    public void ShootingEnds()
    {
        enemyController.ShootingEnds();
    }

}
