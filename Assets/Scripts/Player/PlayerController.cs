using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] MeshRenderer upperBox, lowerBox;
    Rigidbody m_RigidBody;
    const float gravity = 9.81f;
    int gravityModifier = 1
    ,rotationDirection
    ,health=3;
    float inputDirection
    ,movementSpeed = 200
    ,rotationSpeed = 200
    ,invulnerabilityTimer =1
    ,invulnerabilitytimerCounter;
    Vector3 toGoToRotation,currentRotationToVector3;

    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
      
    }

    // Update is called once per frame
    void Update()
    {
        InvurnabilityTimerCountDown();
        GetInputDirection();
        Rotate();
        if(Input.GetKeyDown(KeyCode.E))
        {
            RotateRight();
            
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateLeft();   
        }
      

    }
    private void FixedUpdate()
    {
        CustomGravity();
        Move();
    }
    private void Rotate()
    {
       
        if (Mathf.Abs(Mathf.Abs(toGoToRotation.x) - Mathf.Abs(currentRotationToVector3.x)) > 2 )
        {
            currentRotationToVector3 += Vector3.right * rotationDirection * rotationSpeed * Time.deltaTime;
          
        }
       else
        {
            currentRotationToVector3 = toGoToRotation;
            
          
        }
        if(Mathf.Abs(currentRotationToVector3.x) > 360)
        {
            currentRotationToVector3 = Vector3.zero;
        }
        transform.rotation = Quaternion.Euler(currentRotationToVector3);
       
      
    }
    private void RotateLeft()
    {
        SwapGravity();
        rotationDirection = -1;
        if (toGoToRotation.x == 0)
        {
            toGoToRotation = new Vector3(180, 0, 0);
        }
        else
        {
            toGoToRotation = Vector3.zero;
        }
       


    }
    private void RotateRight()
    {
        SwapGravity();
        rotationDirection = 1;
        if (toGoToRotation.x == 0)
        {
            toGoToRotation = new Vector3(180, 0, 0);
        }
        else
        {
            toGoToRotation = Vector3.zero;
        }
    }
   
    private void Move()
    {
        Vector3 movementDirection = new Vector3(0, m_RigidBody.velocity.y, inputDirection*gravityModifier * movementSpeed*Time.fixedDeltaTime);
        m_RigidBody.velocity = movementDirection;
    }

    private void CustomGravity()
    {
      Vector3 gravityVector = gravity*gravityModifier  * Vector3.down;
      m_RigidBody.AddForce(gravityVector, ForceMode.Acceleration);    
    }

    private void SwapGravity()
    {
        gravityModifier *= -1;
    }
  
    private void GetInputDirection()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");

    }
    private void OnTriggerEnter(Collider other)
    {
        if(invulnerabilitytimerCounter >0)
            return;
        health--;
        SetToTransparentMaterial();
        if (health < 0)
        {
            Debug.Log("Death");
            return;
        }
        invulnerabilitytimerCounter = invulnerabilityTimer;
    }
    private void InvurnabilityTimerCountDown()
    {
        if(invulnerabilitytimerCounter>0)
        {
            
            invulnerabilitytimerCounter -= Time.deltaTime;
        }
        else
        {
            invulnerabilitytimerCounter = 0;
            SetToSolidMaterial();
        }
    }

    
   private void SetToTransparentMaterial()
    {
        upperBox.material = Resources.Load("Materials/RedTransparentMaterial") as Material;
        lowerBox.material = Resources.Load("Materials/BlueTransparentMaterial") as Material;
    }
    private void SetToSolidMaterial()
    {
        upperBox.material = Resources.Load("Materials/RedSolidMaterial") as Material;
        lowerBox.material = Resources.Load("Materials/BlueSolidMaterial") as Material;
    }

    

}
