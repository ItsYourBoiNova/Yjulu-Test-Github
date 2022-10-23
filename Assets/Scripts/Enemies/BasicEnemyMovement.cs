using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public float speed { get; set; }
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * speed;
    }
    
    


}
