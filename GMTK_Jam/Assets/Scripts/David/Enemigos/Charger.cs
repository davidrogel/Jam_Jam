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

        private StopHeart stop;
        private HealthController healthController;
        private BoostSensor sensor;
        private Transform myTransform;
        private Rigidbody2D rb;
        private Animator anim;

        private void Start()
        {
            stop = GetComponent<StopHeart>();
            healthController = GetComponent<HealthController>();
            anim = GetComponent<Animator>();
            myTransform = transform;
            rb = GetComponent<Rigidbody2D>();
            sensor = myTransform.GetChild(0).GetComponent<BoostSensor>();
            anim.SetBool("walk", true);
        }

        private void Update()
        {
            SetAnims();
            
            Boost();

            Rotation();
        }

        private void Boost()
        {
            if (sensor.boost)
            {
                auxSpeedBoost -= Time.deltaTime * decrementBoostSpeedRatio;
                if (auxSpeedBoost <= speed)
                    sensor.boost = false;
            }
        }

        private void Rotation()
        {
            Vector2 dirToTarget = (target.position - myTransform.position).normalized;

            float angle = Vector3.Cross(dirToTarget, myTransform.up).z;

            myTransform.Rotate(Vector3.forward, -angle * rotateSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            // move
            rb.velocity = myTransform.up * ((sensor.boost) ? auxSpeedBoost : speed);
        }

        private void SetAnims()
        {
            anim.SetBool("boost", sensor.boost);

            if(healthController.GetCurrentHealth() <= 0)
            {
                anim.SetTrigger("death");
                // stop !
                stop.StopIt();
            }
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
                switch (go.layer)
                {
                    case 9: // death zone
                        {
                            healthController.ApplyDmg(int.MaxValue);
                        }
                        break;
                    case 11: // player
                        {
                            go.GetComponent<HealthController>().ApplyDmg(dmg);
                        }
                        break;
                }
            }
        }
    }
}
