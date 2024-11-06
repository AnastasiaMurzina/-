using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float fStartSpeed;
    public float fSpeed;
    public float fX = -300;
    public float fY = 300;
    public float fX2;
    public float fY2;

    public bool bDashing;
    public bool bReverse;
    public bool Shifting;
    public bool Left;
    public bool Right;

    public GameObject GG;
    public GameObject MelleEffect;
    public GameObject FantomMelleEffect;
    public GameObject FantomParyyEffect;
    public GameObject Enemy;
    public GameObject Light1;
    public GameObject Light2;

    public Animator Anim;

    public Beautiful bet;
    public PlayerAttack At;
    public Player Pl;
    public Parry pry;

    public Rigidbody2D rb;

    public Vector2 v2MoveVelocity;
    public Vector2 v2MoveInput;
    public Vector3 scl;

    void Start()
    {
        bet = GG.GetComponent<Beautiful>();
        fStartSpeed = fSpeed;
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Pl = GG.GetComponent<Player>();
        pry = FantomParyyEffect.GetComponent<Parry>();
        At = FantomMelleEffect.GetComponent<PlayerAttack>();
    }

    public void Update()
    {
        if (At.Attacking1)
        {
            Light1.SetActive(false);
            Light2.SetActive(false);
        }
        if (pry.Parrying)
        {
            Light1.SetActive(false);
            Light2.SetActive(false);
        }
        if (Left && pry.Parrying == false && At.Attacking1 == false)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            Light1.SetActive(false);
            Light2.SetActive(true);
        }
        else if (Right && pry.Parrying == false && At.Attacking1 == false)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            Light1.SetActive(true);
            Light2.SetActive(false);
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0); 
        }

        if (Pl.Dead)
        {
            Anim.SetBool("Stun", false);
            Anim.SetTrigger("Dead");
        }
        if (v2MoveInput.x != 0 || v2MoveInput.y != 0)
        {
            Anim.SetBool("Walk", true);
        }
        else
        {
            Anim.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.A) == true && Input.GetKey(KeyCode.D) == false && Pl.stunned == false && Pl.Dead == false)
        {
            if (Left == false)
            {
                Left = true;
                Right = false;
            }
        }

        if (Input.GetKey(KeyCode.D) == true && Pl.stunned == false && Pl.Dead == false)
        {
            if (Right == false)
            {
                Right = true;
                Left = false;
            }
        }
        
        if (Input.GetKey(KeyCode.Space) == true && Pl.stunned == false && Pl.Dead == false)
        {
            Anim.SetBool("Dash", true);
        } 
        else if (Pl.Dead == false && Pl.Target != Pl.transform.position)
        {
            Anim.SetBool("Dash", false);
        }
        if (bDashing == false)
        {
            fSpeed = fStartSpeed;
        }
        if (Pl.stunned == false && Pl.Dead == false)
        {
            v2MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            v2MoveVelocity = v2MoveInput.normalized * fSpeed;

            fX2 = MelleEffect.transform.localScale.x;
            fY2 = MelleEffect.transform.localScale.y;
        }

        if (Pl.stunned && Pl.Dead == false)
        {
            Anim.SetBool("Stun", true);
            if (transform.position.x < Enemy.transform.position.x && bReverse == false && Left == true)
            {
                Left = false;
                bReverse = true;
            }
            else if (transform.position.x > Enemy.transform.position.x && bReverse == false && Right == true)
            {
                Right = false;
                bReverse = true;
            }
        }
        else
        {
            Anim.SetBool("Stun", false);
            bReverse = false;
        }
    }

    public void FixedUpdate()
    {
        if (Pl.stunned == false)
        {
            rb.MovePosition(rb.position + v2MoveVelocity * Time.fixedDeltaTime);
        }
    }

    public void DashSlow()
    {
        fSpeed = fStartSpeed;
        fSpeed /= 2;
        bDashing = false;
    }

    public void DashMain()
    {
        fSpeed = fStartSpeed;
        fSpeed *= 3f;
        bDashing = true;
    }

    public void ShiftMain()
    {
        fSpeed = fStartSpeed;
        fSpeed *= 5;
        bDashing = true;
    }

    public void DashEnd()
    {
        bet.Anim.SetBool("bDash", false);
        fSpeed = fStartSpeed;
        bDashing = false;
        Shifting = false;
        At.Attacking1 = false;
    }




}


