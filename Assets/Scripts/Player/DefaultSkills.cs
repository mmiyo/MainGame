using System.Numerics;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultSkills : MonoBehaviour
{   
    private PlayerInput fComp_playerInput;
    private Rigidbody fComp_rigidbody;
    private bool isPropelling;
    [SerializeField] private float propellingPower;
    [SerializeField] private GameObject player;
    [SerializeField] private StatusManager statusManager;      
    void Awake()
    {
        fComp_playerInput = GetComponent<PlayerInput>();
        fComp_rigidbody = GetComponent<Rigidbody>();
        
    }

    public void Propel(InputAction.CallbackContext context)
    {   
        if(context.performed)
        isPropelling = true;
         
        if(context.canceled)
        {
            isPropelling = false;
        }
        

    }

    // Update is called once per frame
    void Update()
    {   
    
        if(isPropelling && statusManager.CurrentFuel > 0)
        {
            fComp_rigidbody.AddForce(UnityEngine.Vector3.up * propellingPower, ForceMode.Force);
             

            statusManager.DrainFuel(25f * Time.deltaTime);
            
        }

        else
        {
            fComp_rigidbody.useGravity = true;
        }
        
        }
             
}

abstract class SkillTree
{
    
}