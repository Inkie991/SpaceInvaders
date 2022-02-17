using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryShoot()
    {
        float chance = Random.Range(0,1f);
        if (chance > 0.8f)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab);
            bullet.transform.position =
                new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Managers.GameProcess.ChangeScore();
            Death();
            Destroy(other.gameObject);
        }
    }

    private void Death()
    {
        Managers.Audio.DeathSound();
        Destroy(gameObject);
    }
}
