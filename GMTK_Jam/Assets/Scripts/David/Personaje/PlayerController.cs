﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 7f;
    [SerializeField]
    private float dashForce = 100f;

    private bool invulnerable = false;


    [SerializeField]
    private float timeDashing = 2;
    private float defaultTimeDashing;
    private bool dashing = false;
    private Vector3 dashDir;

    private Vector2 keyInput;
    private Transform myTransform;
    private Rigidbody2D rb;

    private void Start ()
    {
        defaultTimeDashing = timeDashing;
        rb = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }

    private void Update ()
    {
        // input
        keyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // dash
        Dash();

        // rotation
        Rotation();
    }

    private void Dash()
    {
        bool dashInput = (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space));
        
        if (dashInput && !dashing)
        {
            dashDir = keyInput;
            dashing = true;            
        }

        // si terminamos de dashear
        if(timeDashing < 0)
        {            
            timeDashing = defaultTimeDashing;
            dashing = false;
        }
        // si estamo en tiempo de dash
        else if(dashing)
        {
            // reducimos el tiempo
            timeDashing -= Time.deltaTime;
            // desplazamos
            rb.AddForce(dashDir * dashForce, ForceMode2D.Force);
        }
    }

    private void Rotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, 0, 10);
        Vector3 dirToMouse = (mousePosition - myTransform.position);

        float angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90;
        myTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FixedUpdate()
    {
        rb.velocity = keyInput * speed;
    }
}
