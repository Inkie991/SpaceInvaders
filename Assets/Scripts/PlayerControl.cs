using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private const float movementPadding = 0.1f;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int healthPoint = 5;
    
    [SerializeField] private bool isUnbreakable = false;
    private SpriteRenderer playerSprite;

    private float leftBorder;
    private float rightBorder;

    // Set health and start coroutine
    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        Managers.UI.ChangeHealth(healthPoint);
        StartCoroutine(InfiniteLoop());
        leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + playerSprite.bounds.size.x / 2 + movementPadding;
        rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - playerSprite.bounds.size.x / 2 - movementPadding;
    }
    
    // Check touches and health
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
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

    // Move to the left
    private void MoveLeft()
    {
        if (transform.position.x > leftBorder)
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
        }
    }
    

    // Move to the right
    private void MoveRight()
    {
        if (transform.position.x < rightBorder)
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
        }
    }

    // Instantiate a bullet
    private void BulletShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,
            new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
    }
    
    // Shoot bullet every second
    private IEnumerator InfiniteLoop()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1);
        
        while (true)
        {
            BulletShoot();
            yield return waitTime;
        }
    }
    
    // Handle trigger event
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

    // Decrease health, update UI and become unbreakable 
    private void TakeDamage()
    {
        healthPoint--;
        Managers.UI.ChangeHealth(healthPoint);
        StartCoroutine(BecomeUnbreakable());
    }

    // Set isUnbreakable and make blink effect
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
