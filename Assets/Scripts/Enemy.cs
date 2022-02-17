using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;

    // Do shot at random time
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

    // Call death, destroy bullet and increase the score
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Managers.GameProcess.ChangeScore();
            Death();
            Destroy(other.gameObject);
        }
    }

    // Play death sound and destroy this game object
    private void Death()
    {
        Managers.Audio.DeathSound();
        Destroy(gameObject);
    }
}
