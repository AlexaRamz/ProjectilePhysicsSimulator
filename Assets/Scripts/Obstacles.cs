using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public Transform Projectile;
    public Transform Source;
    Rigidbody2D rb2d;
    public GameObject Arrow;

    void Start()
    {
        rb2d = Projectile.GetComponent<Rigidbody2D>();
    }
    void ResetProjectile()
    {
        Arrow.GetComponent<ArrowLaunch>().canLaunch = false;
        rb2d.velocity = new Vector2(0, 0);
        Projectile.position = Source.position + new Vector3(0, 0, 0);
        Source.GetComponent<Physics>().elapse_time = 0;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ResetProjectile();
        }
    }
}
