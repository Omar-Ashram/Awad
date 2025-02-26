using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _healDelay;

    int _ammo = 0;
    int heal = 50;

    bool inZone = false;
    float _healTimer;
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

    private void Update()
    {
        if (inZone && Time.time > _healTimer) 
        {
            inZone = false;
            heal += 40;
            print(heal);
        }
    }
}
