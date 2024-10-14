using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReachTarget : MonoBehaviour
{
    public GameObject textSlide;
    public GameObject winText;

    public Transform Projectile;
    public Transform Source;
    Rigidbody2D rb2d;
    public GameObject Arrow;
    public GameObject Wall;

    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float topLimit;

    public float leftLimitw;
    public float rightLimitw;
    public float bottomLimitw;
    public float topLimitw;

    public int points;
    public Text scoreText;

    void Start()
    {
        rb2d = Projectile.GetComponent<Rigidbody2D>();
    }
    void RandomTarget()
    {
        transform.position = new Vector3(Random.Range(rightLimit, leftLimit), Random.Range(bottomLimit, topLimit), transform.position.z);
    }
    void RandomObstacle()
    {
        Vector3 wallPos = Wall.transform.position;
        wallPos = new Vector3(Random.Range(rightLimitw, leftLimitw), Random.Range(bottomLimitw, topLimitw), transform.position.z);

        float diff = Mathf.Abs(wallPos.x - gameObject.transform.position.x);
        if (diff >= 0.5f)
        {
            Wall.transform.position = wallPos;
        }
        else
        {
            RandomObstacle();
        }
    }
    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(2); 
        textSlide.GetComponent<Animator>().ResetTrigger("Background");
        winText.GetComponent<Animator>().ResetTrigger("Win");
        winText.GetComponent<SpriteRenderer>().enabled = false;
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
            textSlide.GetComponent<Animator>().SetTrigger("Background");
            winText.GetComponent<Animator>().SetTrigger("Win");
            winText.GetComponent<SpriteRenderer>().enabled = true;
            points += 1;

            ResetProjectile();
            RandomTarget();
            RandomObstacle();
            StartCoroutine(ResetAnim());
        }
    }
    void FixedUpdate()
    {
        scoreText.GetComponent<Text>().text = "Score: " + points.ToString();
    }
}