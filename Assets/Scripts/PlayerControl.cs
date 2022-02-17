using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    private float speed = 10f;
    private int healthPoint = 5;
    private bool isUnbreakable = false;
    private SpriteRenderer playerSprite;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        Managers.UI.ChangeHealth(healthPoint);
        StartCoroutine(InfiniteLoop());
    }
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPosition = touch.position;

            int screenWidth = Screen.width;

            if (0 < touchPosition.x && touchPosition.x < screenWidth / 2)
            {
                MoveLeft();
            } else if (screenWidth / 2 < touchPosition.x && touchPosition.x < screenWidth)
            {
                MoveRight();
            }
        }

        if (healthPoint == 0)
        {
            Managers.GameProcess.GameOver();
        }

        
    }

    private void MoveLeft()
    {
        if (transform.position.x > -2.6f)
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
        }
    }

    private void MoveRight()
    {
        if (transform.position.x < 2.6f)
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
        }
    }

    private void bulletShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,
            new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
    }
    
    private IEnumerator InfiniteLoop()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (true)
        {
            bulletShoot();
            yield return waitTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUnbreakable && healthPoint > 0)
        {
            if (other.gameObject.CompareTag("Enemy Bullet"))
            {
                Destroy(other.gameObject);
                TakeDamage();
            }
        }
    }

    private void TakeDamage()
    {
        healthPoint--;
        Managers.UI.ChangeHealth((healthPoint));
        StartCoroutine(BecomeUnbreakable());
    }

    private IEnumerator BecomeUnbreakable()
    {
        if (!isUnbreakable)
        {
            isUnbreakable = true;
            for (int i = 0; i <= 3; i++)
            {
                playerSprite.color = new Color(1, 1, 1, 0.3f);
                yield return new WaitForSeconds(0.5f);
                playerSprite.color = new Color(1, 1, 1, 1f);
                yield return new WaitForSeconds(0.5f);
            }

            isUnbreakable = false;
        }
    }

}
