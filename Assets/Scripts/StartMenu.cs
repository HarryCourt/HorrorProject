using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject.GetComponent<PlayerMovement>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            Destroy(gameObject);
        }
    }
}
