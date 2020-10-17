using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectProperties : MonoBehaviour
{
    public string description;

    public bool isKey;

    public GameObject[] doorsToUnlock;
    public GameObject toSetActive;
    public AudioClip pickupSound;

    private void Awake()
    {
        if (toSetActive != null) toSetActive.SetActive(false);
    }

}
