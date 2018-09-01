using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public float speed = 5f;
    public float speedBoost = 15f;
    public float decrementBoostSpeedRatio = 2f;
    public float rotateSpeed = 150f;
    public Transform target;

    [SerializeField]
    private bool boosted = false;
    [SerializeField]
    private float auxSpeedBoost = 0f;

    private Transform myTransform;

    private Rigidbody2D rb;

    private void Start()
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(boosted)
        {
            auxSpeedBoost -= Time.deltaTime * decrementBoostSpeedRatio;
            if (auxSpeedBoost <= 0)
                auxSpeedBoost = 0;
                //boosted = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 dirToTarget = (target.position - myTransform.position).normalized;

        float angle = Vector3.Cross(dirToTarget, myTransform.up).z;

        rb.angularVelocity = -angle * rotateSpeed;

        // move
        rb.velocity = myTransform.up * ((boosted) ? auxSpeedBoost : speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        auxSpeedBoost = speedBoost;
        boosted = true;
    }
}