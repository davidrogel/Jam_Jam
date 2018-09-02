using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jam_jam
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 7f;
        [SerializeField]
        private float dashForce = 100f;

        private bool invulnerable = false;
        [SerializeField]
        private float invulnerableTime = 1f;
        private float currentInvulnerableTime;

        [SerializeField]
        private float timeDashing = 2;
        private float defaultTimeDashing;
        private bool dashing = false;
        private Vector3 dashDir;

        private bool getDmg = false;

        private Vector2 keyInput;
        private Transform myTransform;
        private Rigidbody2D rb;

        private Animator anim;

        private HealthController playerHealth;        
        private UI_Bar healthBar;

        private void Start()
        {
            playerHealth = GetComponent<HealthController>();
            healthBar = GameObject.Find("HealthBar").GetComponent<UI_Bar>();

            healthBar.Value = healthBar.MaxValue = playerHealth.GetHealth();

            currentInvulnerableTime = invulnerableTime;
            defaultTimeDashing = timeDashing;

            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            myTransform = transform;
        }

        private void Update()
        {
            healthBar.Value = playerHealth.GetCurrentHealth();

            // input
            keyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            // inmoooorrrtal
            InvulnerableState();

            // dash
            Dash();

            // animaciones
            AnimStates();

            // rotation
            Rotation();
        }

        private void AnimStates()
        {
            anim.SetBool("dash", dashing);

            if(getDmg)
            {
                anim.SetTrigger("getdmg");
                getDmg = false;
            }
            else if((keyInput != Vector2.zero))
            {
                anim.SetBool("onMovement", true);
            }
            else
            {
                anim.SetBool("onMovement", false);
            }
        }

        private void InvulnerableState()
        {
            if (invulnerable)
            {
                currentInvulnerableTime -= Time.deltaTime;
                if (currentInvulnerableTime <= 0)
                {
                    currentInvulnerableTime = invulnerableTime;
                    invulnerable = false;
                }
            }
        }

        private void Dash()
        {
            bool dashInput = 
            (
                Input.GetKeyDown(KeyCode.LeftShift) || 
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.E)
            );

            if (dashInput && !dashing)
            {
                dashDir = keyInput;
                dashing = true;
            }

            // si terminamos de dashear
            if (timeDashing < 0)
            {
                timeDashing = defaultTimeDashing;
                dashing = false;
            }
            // si estamo en tiempo de dash
            else if (dashing)
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 8) // enemy
            {
                invulnerable = true;
                getDmg = true;
            }
        }
    }
}