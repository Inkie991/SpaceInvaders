using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Rigidbody2D _body;

    // Get rigidbody
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Move a bullet up and destroy at the end
    private void Update()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));

        if (transform.position.y > 7)
        {
            Destroy(gameObject);
        }
    }

}
