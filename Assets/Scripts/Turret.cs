using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _target;
    [SerializeField] Transform _barrel;
    [SerializeField] Transform _shootPos;
    [SerializeField] float _shootRate; 
    [SerializeField] float _shootDistance;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _bulletLifeTime;
    [SerializeField] float _damage;

    float _lastShootTime;
    float health = 100f;

    private void OnEnable()
    {
        bullet.OnHitBoss += TakeDamage;
    }

    private void OnDisable()
    {
        bullet.OnHitBoss -= TakeDamage;
    }
    private void Start()
    {
        _lastShootTime = Time.time + _shootRate;
    }

    private void Update()
    {
        _barrel.LookAt(_target);
        float dist = Vector3.Distance(transform.position, _target.position);
        if (dist <_shootDistance && Time.time > _lastShootTime && !GameManager.Instance.IsGameOver()) 
        {
            Fire();

        }

        if(health <= 0) 
        {
            OnBossDeath?.Invoke();
            GameManager.Instance.BossDied();
        }
    }

    void Fire() 
    {
        _lastShootTime = Time.time + _shootRate;
        GameObject bullet = Instantiate( _bulletPrefab,_shootPos.position,Quaternion.identity);
        bullet bulletScript  = bullet.GetComponent<bullet>();

        bulletScript.SetBullet(_bulletSpeed, _bulletLifeTime, _damage, _target);

    }

    public static UnityAction OnBossDeath;
    void TakeDamage(float damage) 
    {
        if(health > 0) 
        {
            health -= damage;
        }

    }
}
