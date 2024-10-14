using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    public float firingAngle;
    public float velocity;
    public float gravity;

    public Transform Projectile;
    
    public GameObject Arrow;

    Rigidbody2D rb2d;

    public float elapse_time;

    public float leftBound;
    public float rightBound;
    public float bottomBound;

    void Start()
    {
        rb2d = Projectile.GetComponent<Rigidbody2D>();
        elapse_time = 0;
    }

    void ResetProjectile()
    {
        Arrow.GetComponent<ArrowLaunch>().canLaunch = false;
        rb2d.velocity = new Vector2(0, 0);
        Projectile.position = transform.position + new Vector3(0, 0, 0);
        elapse_time = 0;
    }
    void Update()
    {
        if (Arrow.GetComponent<ArrowLaunch>().canLaunch == true)
        {
            firingAngle = Arrow.GetComponent<ArrowLaunch>().angle;
            velocity = Arrow.GetComponent<ArrowLaunch>().speed;
            float Vx = velocity * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float Vy = velocity * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            rb2d.velocity = new Vector2(Vx, (Vy - (gravity * elapse_time)));
            elapse_time += Time.deltaTime;
        }
        if (Projectile.position.x >= rightBound || Projectile.position.x <= leftBound || Projectile.position.y <= bottomBound)
        {
            ResetProjectile();
        }
    }
}