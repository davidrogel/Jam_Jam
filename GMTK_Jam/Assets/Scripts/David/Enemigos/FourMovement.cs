using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam_jam
{
    public class FourMovement : MonoBehaviour
    {
        public float speed = 7f;

        private Sensor[] sensors;
        private Sensor currentSensorActivated = null;

        [SerializeField]
        private Vector2 moveDir;
        [SerializeField]
        private bool onMovement = false;

        private Transform myTransform;
        private Rigidbody2D rb;

        private void Start()
        {
            myTransform = transform;
            rb = GetComponent<Rigidbody2D>();

            if(sensors == null || sensors.Length == 0)
            {
                int count = myTransform.childCount;
                sensors = new Sensor[count];
                for(int i = 0; i < count; i++)
                {
                    sensors[i] = myTransform.GetChild(i).GetComponent<Sensor>();
                }
            }
        }

        private void Update()
        {
            if(!onMovement)
            {
                //print("no me muevo, busco sensor");
                CheckMoveDirection();
            }
            else
            {
                //print("me estoy moviendo");
            }
        }

        private void CheckMoveDirection()
        {
            // TODO hay que comprobar si el este se está moviendo
            // y tambien hay que pararlo, osea enviarle un go = false al sensor
            currentSensorActivated = WhichSensorIsTriggered();
            if (currentSensorActivated != null)
            {
                switch (currentSensorActivated.dir)
                {
                    case Direction.N:
                        moveDir = new Vector2(0f, 1f);
                        break;
                    case Direction.S:
                        moveDir = new Vector2(0f, -1f);
                        break;
                    case Direction.E:
                        moveDir = new Vector2(1f, 0f);
                        break;
                    case Direction.W:
                        moveDir = new Vector2(-1f, 0f);
                        break;
                }
                onMovement = true;
            }
        }
        
        private Sensor WhichSensorIsTriggered()
        {
            foreach(Sensor s in sensors)
            {
                if(s.go)
                {
                    return s;
                }
            }

            return null;
        }

        private void FixedUpdate()
        {
            rb.velocity = moveDir * speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // walls
            if (collision.collider.gameObject.layer == 10)
            {
                // paramos el enemigo
                onMovement = false;
                // reseteamos variables a estado neutro
                foreach (Sensor s in sensors)
                    s.go = false;
                moveDir = Vector2.zero;
            }
            // player
            else if (collision.gameObject.layer == 11)
            {

            }
            // other enemies
            else if (collision.gameObject.layer == 12)
            {

            }
        }
    }
}