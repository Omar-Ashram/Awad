using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject _pickUpBullet;
    [SerializeField] List<Transform> _SpawnPos;

    [SerializeField] float bossCoins;


    const string _bestScore = "BestScore";

    bool isGameOver;
    float _coins;

    private void Start()
    {
        if (Instance == null) 
        {
            Instance = this;    
            _coins = 0;
        }
        else 
        {
            Destroy(this.gameObject);   
        }
        for (int i = 0; i < _SpawnPos.Count; i++)
        {
            SpawnPickUp(_SpawnPos[i]);
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


    private void SpawnPickUp(Transform SpawnPos) 
    {
        Instantiate(_pickUpBullet, SpawnPos.position,Quaternion.identity);
    }

    private void SetCoins(float coins)
    {
        _coins += coins;

        if (PlayerPrefs.HasKey(_bestScore))
        {
            if (_coins > PlayerPrefs.GetFloat(_bestScore))
            {
                PlayerPrefs.SetFloat(_bestScore, _coins);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(_bestScore, _coins);
        }

        print(PlayerPrefs.GetInt(_bestScore));
    }


    public void BossDied() 
    {
        print("game over");
        isGameOver = true;
        SetCoins(bossCoins);
    }
}
