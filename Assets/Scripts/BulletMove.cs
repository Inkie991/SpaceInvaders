using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float speed = 2f;
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));
        
       // _body.MovePosition(transform.position + Vector3.up * (speed * Time.deltaTime));

        if (transform.position.y > 7)
        {
            Destroy(gameObject);
        }
    }

}
