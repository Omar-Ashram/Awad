using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _target;
    [SerializeField] Transform _barrel;
    [SerializeField] Transform _shootPos;
    [SerializeField] float _shootRate;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _bulletLifeTime;
    [SerializeField] float _damage;

    float _lastShootTime;

    private void Start()
    {
        _lastShootTime = Time.time + _shootRate;
    }

    private void Update()
    {
        _barrel.LookAt(_target);  

        if(Time.time > _lastShootTime) 
        {
            Fire();

        }
    }

    void Fire() 
    {
        _lastShootTime = Time.time + _shootRate;
        GameObject bullet = Instantiate( _bulletPrefab,_shootPos.position,Quaternion.identity);
        bullet bulletScript  = bullet.GetComponent<bullet>();

        bulletScript.SetBullet(_bulletSpeed, _bulletLifeTime, _damage, _target);

    }
}
