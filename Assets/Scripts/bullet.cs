using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class bullet : MonoBehaviour
{
    [SerializeField] GameObject explosion1;
    [SerializeField] GameObject explosion2;
    [SerializeField] GameObject bulletBody;

    [SerializeField] List<AudioClip> _hitSound;
    float _bulletSpeed;
    float _bulletLifeTime;
    float _damage;
    Transform _target;

    float rotateSpeed = 200f;

    Rigidbody _rb;

    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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
    public static UnityAction<float> OnHitBoss;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player")) OnHit?.Invoke(_damage);

        if(collision.transform.CompareTag("GAS")) OnHitBoss?.Invoke(_damage);

        if(Random.value >= 0.5f) explosion1.SetActive(true);
        else explosion2.SetActive(true);

        
        _target = null;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        bulletBody.SetActive(false);
        int i = Random.Range(0, 1);
        if(_audioSource != null) 
            _audioSource.PlayOneShot(_hitSound[i]);
        Destroy(this.gameObject,2f);
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
