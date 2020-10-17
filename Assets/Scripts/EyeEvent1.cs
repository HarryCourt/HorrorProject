using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEvent1 : MonoBehaviour
{
    public GameObject playerObject;
    public Transform endMenu;
    public bool isLast;

    public Transform mouth;
    public AudioSource demonScreamSource;

    private void Awake()
    {
        if (endMenu != null)
        {
            endMenu.gameObject.SetActive(false);
        }
        else if (!endMenu) { return; }

        if (mouth != null)
        {
            mouth.gameObject.SetActive(false);
        }

        if (demonScreamSource != null) { demonScreamSource.gameObject.SetActive(false); }

        else if (mouth == null && demonScreamSource == null)
        {
            return;
        }
    }

    private void Update()
    {
        float dist = Vector3.Distance(playerObject.transform.position, transform.position);
        print("Distance = " + dist);

        if (dist < 22f && !isLast)
        {
            Destroy(gameObject);
        }

        if (dist < 44f && isLast)
        {
            GetComponent<Animation>().Stop();
            demonScreamSource.gameObject.SetActive(true);
            mouth.gameObject.SetActive(true);
            transform.Translate(Vector3.up * Time.deltaTime * 45f);
        }

        if (dist < 5f && isLast)
        {
            AudioListener.volume = 0f;
            endMenu.gameObject.SetActive(true);
            demonScreamSource.Play();
            print("Game Over!");
        }

    }
}
