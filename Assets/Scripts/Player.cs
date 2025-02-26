using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _healDelay;
    [SerializeField] float _healIncrease;
    [Space]
    [SerializeField] GameObject _bulletPrefab;
    [Space]
    [SerializeField] Transform _target;
    [SerializeField] Transform _shootPos; 
    [SerializeField] Transform _barrel;
    [Space]
    [SerializeField] float _shootDistance;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _bulletLifeTime;
    [SerializeField] float _damage;
    [SerializeField] float _shootRate;

    float _lastShootTime;

    int _ammo = 5;

    float heal = 100;

    bool inZone = false;
    float _healTimer;

    private void OnEnable()
    {
        bullet.OnHit += TakeDamage;
    }
    private void OnDisable()
    {
        bullet.OnHit -= TakeDamage;
    }

    private void Start()
    {
        _lastShootTime = Time.time + _shootRate;
    }
    private void TakeDamage(float damage)
    {
        if(heal >= 0) heal -= damage;
        else 
        {
            heal = 0;
            GameManager.Instance.EndGame();
        }
        print(heal);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.transform.CompareTag("Coffee")) 
        {
            _healTimer = Time.time + _healDelay;  
            inZone = true;
        }

        if (other.transform.CompareTag("Bullet")) 
        {
            _ammo++;
            other.gameObject.SetActive(false);   
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Coffee"))
        {
            inZone = true;
        }
    }

    private void Update()
    {
        _barrel.LookAt(_target);
        if (inZone && Time.time > _healTimer) 
        {
            inZone = false;
            heal += _healIncrease;
            print(heal);
        }

        float dist = Vector3.Distance(transform.position, _target.position);
        if (dist < _shootDistance && _ammo > 0 && Time.time > _lastShootTime) 
        {
            if (!GameManager.Instance.IsGameOver()) Fire();
        }
    }

    void Fire()
    {
        _ammo--;
        _lastShootTime = Time.time + _shootRate;
        GameObject bullet = Instantiate(_bulletPrefab, _shootPos.position, Quaternion.identity);
        bullet bulletScript = bullet.GetComponent<bullet>();

        bulletScript.SetBullet(_bulletSpeed, _bulletLifeTime, _damage, _target);

    }
}
