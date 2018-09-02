using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam_jam
{
    public class Charger : MonoBehaviour
    {
        public float speed = 5f;
        public float speedBoost = 15f;
        public float decrementBoostSpeedRatio = 2f;
        public float rotateSpeed = 150f;
        public Transform target;

        public int dmg = 1;

        [SerializeField]
        private float auxSpeedBoost = 0f;

        private BoostSensor sensor;
        private Transform myTransform;
        private Rigidbody2D rb;

        private void Start()
        {   
            myTransform = transform;
            rb = GetComponent<Rigidbody2D>();
            sensor = myTransform.GetChild(0).GetComponent<BoostSensor>();
        }

        private void Update()
        {
            if (sensor.boost)
            {
                auxSpeedBoost -= Time.deltaTime * decrementBoostSpeedRatio;
                if (auxSpeedBoost <= speed)
                    sensor.boost = false;
            }

            Vector2 dirToTarget = (target.position - myTransform.position).normalized;

            float angle = Vector3.Cross(dirToTarget, myTransform.up).z;

            myTransform.Rotate(Vector3.forward, -angle * rotateSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            // move
            rb.velocity = myTransform.up * ((sensor.boost) ? auxSpeedBoost : speed);
        }

        public void BoostSpeedUp()
        {
            auxSpeedBoost = speedBoost;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject go = other.gameObject;
            if (go != null)
            {
                if (go.layer == 11)
                {
                    //auxSpeedBoost = speedBoost;
                    //boosted = true;
                    //print("vida");
                    go.GetComponent<HealthController>().ApplyDmg(dmg);
                }
            }
        }
    }
}
