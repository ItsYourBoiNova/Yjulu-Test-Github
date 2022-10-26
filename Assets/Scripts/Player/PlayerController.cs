using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    MeshRenderer m_meshRenderer;
    Rigidbody m_RigidBody;
    const float gravity = 4.5f, gravityFlipCD = 1f;
    int gravityModifier = 1
    ,rotationDirection
    ,health=3;
    float inputDirection
    ,movementSpeed = 300
    ,rotationSpeed = 200
    ,invulnerabilityTimer =1
    ,invulnerabilitytimerCounter
    ,gravityFlipCdCounter;
    Vector3 toGoToRotation,currentRotationToVector3;
    [SerializeField] Image healthBar;

    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
      m_meshRenderer = GetComponent<MeshRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        GravityFlipInternalCD();
        HealthBarDisplay();
        MovementBoundaries();
        InvurnabilityTimerCountDown();
        GetInputDirection();
        
        if(Input.GetKeyDown(KeyCode.E) && gravityFlipCdCounter <=0)
        {
            RotateRight();
            
        }
        else if (Input.GetKeyDown(KeyCode.Q) && gravityFlipCdCounter <= 0)
        {
            RotateLeft();   
        }
      

    }
    private void FixedUpdate()
    {
        Rotate();
        CustomGravity();
        Move();
    }
    private void HealthBarDisplay()
    {
        float healthFloat = health;
        healthBar.fillAmount = healthFloat / 3;
    }
    private void Rotate()
    {
       
        if (Mathf.Abs(Mathf.Abs(toGoToRotation.x) - Mathf.Abs(currentRotationToVector3.x)) > 2 )
        {
            currentRotationToVector3 += Vector3.right * rotationDirection * rotationSpeed * Time.fixedDeltaTime;
          
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
        m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0, m_RigidBody.velocity.z);
        gravityModifier *= -1;
        gravityFlipCdCounter = gravityFlipCD;
      
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

    private void GravityFlipInternalCD()
    {
        if(gravityFlipCdCounter >0)
        {
            gravityFlipCdCounter -= Time.deltaTime;
        }
        else
        {
            gravityFlipCdCounter = 0;
        }
    }

}
