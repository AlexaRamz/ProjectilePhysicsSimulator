using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowLaunch : MonoBehaviour
{
    public GameObject arrow;
    public float angle;
    public float speed;

    public GameObject displayAngle;
    public GameObject displaySpeed;

    public bool canLaunch;
    public bool isReady;
    bool mouseDown;

    void Start()
    {
        canLaunch = false;
        isReady = false;
    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }

        if (canLaunch == false && mouseDown)
        {
            arrow.GetComponent<SpriteRenderer>().enabled = true;

            Vector3 pos = transform.position;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = mousePos - pos;
            angle = Mathf.RoundToInt(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            displayAngle.GetComponent<Text>().text = "Firing Angle: " + angle.ToString();
            isReady = true;

            float scale = Mathf.Sqrt(Mathf.Pow(dir.x, 2) + Mathf.Pow(dir.y, 2)) / 2;
            if (scale >= 2.5f)
            {
                scale = 2.5f;
            }
            transform.localScale = new Vector3(scale, 1, 1);
            speed = Mathf.RoundToInt(scale * 7);
            displaySpeed.GetComponent<Text>().text = "Initial Speed: " + speed.ToString();

        }
        else
        {
            arrow.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (isReady == true && mouseDown == false)
        {
            isReady = false;
            canLaunch = true;
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}