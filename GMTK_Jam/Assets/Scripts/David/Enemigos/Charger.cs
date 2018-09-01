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

    private bool avoid;

    private Transform myTransform;
    private Rigidbody2D rb;

    private void Start()
    {
        avoid = false;
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

        rb.angularVelocity = ((avoid) ? 1 : -1) * angle * rotateSpeed;

        // move
        rb.velocity = myTransform.up * ((boosted) ? auxSpeedBoost : speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "player")
        {
            auxSpeedBoost = speedBoost;
            boosted = true;
        }
        else if(collider.gameObject.tag == "wall")
        {
            auxSpeedBoost = speedBoost;
            avoid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "wall")
        {
            avoid = false;
        }
    }
}