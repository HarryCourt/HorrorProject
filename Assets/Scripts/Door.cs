using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasKey = false; 
    public bool busted;
    public Transform blockageToAppear;

    // Start is called before the first frame update
    void Start()
    {
        if (blockageToAppear != null) { blockageToAppear.gameObject.SetActive(false); }
        else { return; }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
