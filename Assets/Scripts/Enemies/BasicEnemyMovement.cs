using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicEnemyMovement : EnemyClassesParent
{
   
  
    private void FixedUpdate()
    {
        this.transform.position += Vector3.right * Time.fixedDeltaTime * speed;
    }


}
