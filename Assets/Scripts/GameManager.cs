using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject _pickUpBullet;

    bool isGameOver;

    private void Start()
    {
        if (Instance == null) 
        {
            Instance = this;           
        }
        else 
        {
            Destroy(this.gameObject);   
        }
    }


    public void StartGame() 
    {
        isGameOver = false;
    }


    public void EndGame() 
    {
        isGameOver = true;
    }

    public bool IsGameOver() 
    {
        return isGameOver;
    }


    private void SpawnPickUp(GameObject obj, Transform SpawnPos) 
    {
         Instantiate(obj, SpawnPos.position,Quaternion.identity);
    }
}
