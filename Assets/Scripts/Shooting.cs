﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooting : Singleton<Shooting>
{
    [Header("EMP")]
    [SerializeField]
    float empForce = 50f;
    [SerializeField]
    float empRadius = 30f;
    [Header("Bullet")]
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float maxBulletTime = 3f;
    float currentBulletTime;
    bool isHoldingFire = false;
    [SerializeField]
    float bulletTimeIncrease = 2f;
    [SerializeField]
    float bulletForce = 200f;
    [Header("Shooting")]
    [SerializeField]
    float reloadTime = 1f;
    float currentReloadTime = 0f;
    bool reloading = false;
    [Header("Turret")]
    [SerializeField]
    Transform muzzle;
    

    public float EmpForce
    {
        get
        {
            return empForce;
        }
    }

    public float EmpRadius
    {
        get
        {
            return empRadius;
        }
    }

    public float MaxBulletTime
    {
        get
        {
            return maxBulletTime;
        }
    }

    public float BulletForce
    {
        get
        {
            return bulletForce;
        }
    }

    public float CurrentBulletTime
    {
        get
        {
            return currentBulletTime;
        }
    }

    public float CurrentReloadTime
    {
        get
        {
            return currentReloadTime;
        }
    }

    public float ReloadTime
    {
        get
        {
            return reloadTime;
        }
    }

    public bool Reloading
    {
        get
        {
            return reloading;
        }
    }

    void Start()
    {
        MouseInputController.Instance.RegisterOnLeftMouseClickListener(HoldShoot);
        MouseInputController.Instance.RegisterOnLeftMouseReleaseListener(ReleaseShoot);
    }

    void HoldShoot()
    {
        if (reloading)
        {            
            return;
        }
            
        isHoldingFire = true;
        
    }

    void ReleaseShoot()
    {
        if (!isHoldingFire)
            return;
        isHoldingFire = false;        
        StopAllCoroutines();
        reloading = true;
        BulletLogic bl =
            (Instantiate(bulletPrefab, muzzle.position, transform.rotation) as GameObject).
            GetComponent<BulletLogic>();
        bl.Init(currentBulletTime);
        currentBulletTime = 0;
    }

    void Update () {

        if (reloading)
        {
            currentReloadTime += Time.deltaTime;
            if (currentReloadTime > reloadTime)
            {
                currentReloadTime = 0;
                reloading = false;
            }
        }

        if (isHoldingFire)
        {
            currentBulletTime += Time.deltaTime * bulletTimeIncrease;
            if(currentBulletTime > maxBulletTime)
            {
                currentBulletTime = maxBulletTime;
            }
        }
        
    }
}
