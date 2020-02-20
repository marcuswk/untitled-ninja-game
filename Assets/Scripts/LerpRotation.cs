using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour
{
    Vector3 relativePosition;
    Quaternion targetRotation;

    public Transform target;
    public float speed = 0.1f;

    bool rotating = false;
    float rotationTime; // when rotation time = 1, we will have rotated to target ie A -----> B or 0----->1

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            relativePosition = target.position - transform.position;
            targetRotation = Quaternion.LookRotation(relativePosition);
            rotating = true;
            rotationTime = 0;
        }
        if (rotating)
        {
            rotationTime += Time.deltaTime * speed;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime);
        }

        if(rotationTime > 1)
        {
            rotating = false;
        }


    }
}
