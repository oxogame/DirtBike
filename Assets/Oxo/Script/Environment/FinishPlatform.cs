using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.LevelComplated();
    }
}
