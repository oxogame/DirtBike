using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject bike;

    public GameObject mudPrefab;
    public List<GameObject> mudList = new List<GameObject>();
    public int mudSize;

    public GameObject mudStartPosition;

    public GameObject targetMudPosition;

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
    void Start()
    {
        for (int i = 0; i < mudSize; i++)
        {
            GameObject go = Instantiate(mudPrefab);
            mudList.Add(go);
        }
    }

    float minAngle = -10;
    float counter;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (counter > minAngle)
            {
                counter -= 0.4f;
            }
            else
            {
                counter = minAngle;
            }
            //bike.transform.eulerAngles = new Vector3(counter, 0f, 0f);

            if (counter < -5)
            {
                for (int i = 0; i < 10; i++)
                {
                    GameObject mud = mudList.Where(x => !x.activeSelf).FirstOrDefault();
                    mud.transform.position = mudStartPosition.transform.position;
                    Vector3 force = targetMudPosition.transform.position - mud.transform.position + Vector3.right * 0.1f * i + Vector3.left * 0.5f;
                    mud.SetActive(true);
                    mud.GetComponent<Rigidbody>().velocity = bike.GetComponent<Rigidbody>().velocity;
                    mud.GetComponent<Rigidbody>().AddForce(force*20f + Vector3.up * 1200f);
                }
            }
        }
    }
}
