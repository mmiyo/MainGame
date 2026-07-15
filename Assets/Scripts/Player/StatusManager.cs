using System;
using System.Collections;
using NUnit.Framework;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{   
    [SerializeField] private PlayerManager playerManager;

    [Header("HP")]
    private float maxHP = 100;
    private float minHP = 0;
    private float currentHP;
    public float CurrentHP 
    { 
        get {return currentHP;} 
        private set {currentHP = Mathf.Clamp(value, 0, maxHP);} 
    }
    [SerializeField] private Slider hpBar;

    [Header("Fuel")]
    private float maxFuel = 100;
    private float minFuel = 0;
    private bool isRegenerating = false;
    private float currentFuel;
    public float CurrentFuel 
    { 
        get {return currentFuel;} 
        private set {currentFuel = value;} 
    }
    [SerializeField] private Slider fuelBar;

    private void Awake()
    {   
        //HP
        hpBar.maxValue = maxHP;
        hpBar.minValue = minHP;

        currentHP = maxHP;
        hpBar.value = currentHP;

        //Fuel
        fuelBar.maxValue = maxFuel;
        fuelBar.minValue = minFuel;

        currentFuel = maxFuel;
        fuelBar.value = currentFuel;
    }

    public void ApplyDamage(float dmgTaken)
    {
        currentHP = currentHP - dmgTaken;
    }

    public void DrainFuel(float fuelLoss)
    {
        currentFuel = currentFuel - fuelLoss;
    }

    // Update is called once per frame
    void Update()
    {   
        isRegenerating = false;

        hpBar.value = currentHP;
        fuelBar.value = currentFuel;

        while(playerManager.IsGrounded() && currentFuel < 100)
        {   
            isRegenerating = true;
            StartCoroutine(regenerateFuel());
            break;
        }

    }

    private IEnumerator regenerateFuel()
    {   
        if(isRegenerating)
        yield return new WaitForSeconds(0.5f);
        currentFuel = currentFuel + Time.deltaTime * 10f;
        //Debug.Log("yes");
        
          
    }
}
