using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovingEnemyMovement : EnemyClassesParent
{
    float directionCorrector =1;
    void Start()
    {
       
    }
    private void Update()
    {
        if(transform.position.z > 5 || transform.position.z < -4)
        {
            directionCorrector *= -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(1 * speed * Time.fixedDeltaTime, 0, 1 * speed * Time.fixedDeltaTime * directionCorrector);
    }
}
