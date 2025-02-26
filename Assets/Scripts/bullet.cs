using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class bullet : MonoBehaviour
{
    float _bulletSpeed;
    float _bulletLifeTime;
    float _damage;
    Transform _target;

    float rotateSpeed = 200f;

    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if(_bulletLifeTime > 0) 
        {
            _bulletLifeTime -= 0.1f;            
        }
        else Destroy(this.gameObject);

        if (_target == null) return;
        chaseTarget();
    }

    public static UnityAction<float> OnHit;
    private void OnCollisionEnter(Collision collision)
    {
        OnHit?.Invoke(_damage);
        Destroy(this.gameObject);
    }

    public void SetBullet(float bulletSpeed,float bulletLifeTime,float damage,Transform target) 
    {
        _bulletSpeed = bulletSpeed;
        _bulletLifeTime = bulletLifeTime;    
        _damage = damage;
        _target = target;
    }

    private void chaseTarget()
    {
        Vector3 direction = (_target.position - _rb.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        _rb.velocity = transform.forward * _bulletSpeed;
    }
}
