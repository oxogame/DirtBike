using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject bike;

    public GameObject mudParticle;
    public ParticleSystem mud;

    bool isMouseDown;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    float minAngle = -20f;
    float angle;
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (angle > minAngle)
            {
                angle -= 0.5f;
                bike.transform.eulerAngles = new Vector3(angle, 0f, 0f);

                var emision = mud.emission;
                emision.rateOverTime = Mathf.Abs(angle * 50);
            }
            else
            {
                bike.transform.eulerAngles = new Vector3(minAngle, 0f, 0f);
            }

            //bike.GetComponent<BikeController>().EngineTorque = 350f;
            //if (angle < minAngle / 2f)
            //{
            //    mudParticle.SetActive(true);
            //}
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            // mudParticle.SetActive(false);
            //bike.GetComponent<BikeController>().EngineTorque = 1800f;
        }

        if (!isMouseDown && angle < 0)
        {
            angle += 0.5f;
        }

        if (angle > minAngle / 3f)
        {
            var emision = mud.emission;
            emision.rateOverTime = Mathf.Abs(0f);
            //mudParticle.SetActive(false);
        }
    }
}
