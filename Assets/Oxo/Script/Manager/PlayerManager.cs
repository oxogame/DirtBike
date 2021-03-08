using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject bike;

    public GameObject mudParticle;
    public ParticleSystem mud;

    bool isMouseDown;

    float minAngle = -20f;
    float angle;

    public List<GameObject> rockList = new List<GameObject>();
    public GameObject emptySlot;

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
                FillRock();
            }
            else
            {
                bike.transform.eulerAngles = new Vector3(minAngle, 0f, 0f);
                FillRock();
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


    void FillRock()
    {
        //emptySlot.transform.position = Vector3.Lerp(emptySlot.transform.position, new Vector3(emptySlot.transform.position.x, 0f, emptySlot.transform.position.z),0.02f);
        GameObject go = Instantiate(rockList.FirstOrDefault().gameObject);
        go.transform.position = bike.transform.forward * 25f;
        go.transform.localScale = Vector3.one * Random.Range(0.7f, 0.9f);
        go.SetActive(true);
        go.transform.DOShakePosition(0.1f, 1f);
    }
}