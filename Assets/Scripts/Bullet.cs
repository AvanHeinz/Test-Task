using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    Vector3 lastVelocity;

    private void Awake()
    {
        //?????????? ?????? ?????? ???????? ?????
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //??? ??? ???????????? ?? ????????????
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 3f);
    }
}
