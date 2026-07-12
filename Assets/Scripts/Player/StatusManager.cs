using System;
using System.Collections;
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
        private set {currentHP = value;} 
    }
    [SerializeField] private Slider hpBar;

    [Header("Fuel")]
    private float maxFuel = 100;
    private float minFuel = 0;
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
        CurrentFuel = currentFuel - fuelLoss;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHP;
        fuelBar.value = currentFuel;
        while(playerManager.IsGrounded() && currentFuel < 100)
        {
            StartCoroutine(regenerateFuel());
            break;
        }
    }

    private IEnumerator regenerateFuel()
    {           
        yield return new WaitForSeconds(2.8f);
        currentFuel = currentFuel + Time.deltaTime * 10f;
          
    }
}
