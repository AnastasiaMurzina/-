using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    public float offset;

    public GameObject GG;
    public GameObject FantomAttackEffect;
    public GameObject FantomParryEffect;

    public Vector3 Old;

    public PlayerMove Moving;
    public PlayerAttack At;
    public Player GGs;
    public Parry pry;


    void Start()
    {
        At = FantomAttackEffect.GetComponent<PlayerAttack>();
        Moving = GG.GetComponent<PlayerMove>();
        GGs = GG.GetComponent<Player>();
        pry = FantomParryEffect.GetComponent<Parry>();
    }

    void Update()
    {
        if (At.Attacking1 == false && pry.Parrying == false)
        {
            Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - GG.transform.position;
            float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }
    }
}
