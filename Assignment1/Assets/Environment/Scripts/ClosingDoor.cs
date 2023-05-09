using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingDoor : MonoBehaviour
{
    [SerializeField]
    public GameObject Door;

    private void OnTriggerEnter(Collider other)
    {
        Door.SetActive(true);
    }
}
