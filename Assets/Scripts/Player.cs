using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] AudioClip _shootsound;
    [Space]
    [SerializeField] GameObject _hitParticle;
    [SerializeField] Transform _hitParticlePos;
    [Space]
    [SerializeField] AudioClip _healing;
    [SerializeField] AudioClip _takeItem;

    [SerializeField] List<AudioClip> _hitSound;
    [SerializeField] List<AudioClip> _dieSound;

    [SerializeField] TMP_Text _text;

    float _lastShootTime;

    int _ammo = 0;

    float heal = 100;

    bool inZone = false;
    float _healTimer;

    AudioSource _audioSource;
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
        _text.text = "Health: " + heal.ToString();
        _audioSource = GetComponent<AudioSource>();
    }
    private void TakeDamage(float damage)
    {
        if (heal > 0) 
        { 
            heal -= damage;
            int i = Random.Range(0, _hitSound.Count -1);
            _audioSource.PlayOneShot(_hitSound[i]);
            Destroy(Instantiate(_hitParticle,_hitParticlePos.position,Quaternion.identity),2f);
            _text.text = "Health: " + heal.ToString();
        }

        if(heal <= 0) 
        {
            heal = 0;
            
            int i = Random.Range(0, _dieSound.Count -1);
            _audioSource.PlayOneShot(_hitSound[i]);
            GameManager.Instance.EndGame();
        }
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
            float p = Random.Range(1f, 1.6f);
            _audioSource.pitch = p;
            _audioSource.PlayOneShot(_takeItem);
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
            _text.text = "Health: " + heal.ToString();
            _audioSource.PlayOneShot(_healing);
            
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
        _audioSource.PlayOneShot(_shootsound);
        _lastShootTime = Time.time + _shootRate;
        GameObject bullet = Instantiate(_bulletPrefab, _shootPos.position, Quaternion.identity);
        bullet bulletScript = bullet.GetComponent<bullet>();

        bulletScript.SetBullet(_bulletSpeed, _bulletLifeTime, _damage, _target);

    }
}
