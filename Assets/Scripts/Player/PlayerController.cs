using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MeshRenderer m_meshRenderer;
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
      m_meshRenderer = GetComponent<MeshRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        MovementBoundaries();
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
        if(other.CompareTag("Enemy"))
        {
            TakeDamage();
        }
        else if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManger.instance.score += 50;
        }
       
      
    }

    private void TakeDamage()
    {
        if (invulnerabilitytimerCounter > 0)
            return;
        health--;
        SetToTransparentMaterial();
        if (health <= 0)
        {
            GameManger.instance.GameOver();
            Destroy(this.gameObject);
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
        //material 1 is the bottom and 0 is the top
        Material[] newMaterials = { Resources.Load("Materials/BlueTransparentMaterial") as Material, Resources.Load("Materials/RedTransparentMaterial") as Material };
        m_meshRenderer.materials = newMaterials; 
    }
    private void SetToSolidMaterial()
    {
        //material 1 is the bottom and 0 is the top
        Material[] newMaterials = { Resources.Load("Materials/BlueSolidMaterial") as Material, Resources.Load("Materials/RedSolidMaterial") as Material };
        m_meshRenderer.materials = newMaterials;
    }

    private void MovementBoundaries()
    {
        if(transform.position.z > 10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 10);
        }
        else if(transform.position.z < -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        if(Mathf.Abs(transform.position.y) > 25)
        {
            TakeDamage();
        }
    }

    

}
