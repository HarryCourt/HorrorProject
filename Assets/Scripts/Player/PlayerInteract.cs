using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public Transform textDescription;
    public float textWaitValue = 2f;

    public AudioSource audioSource;
    public AudioClip doorUnlockClip;
    public AudioClip keyPickupClip;

    private float textWait;
    private bool hasInteracted = false;
    private PlayerMovement playerMovementScript;

    private void Awake()
    {
        textDescription.gameObject.SetActive(false);                         // Set the panel to not active.
        playerMovementScript = GetComponent<PlayerMovement>();               // Player movement script.
        textWait = textWaitValue;
    }

    // Update is called once per frame
    void Update()
    {
        Interact();

        // If the player has interacted, start the timer to delete text.
        if (hasInteracted)
        {
            textWait -= Time.deltaTime;

            if (textWait < 0f)
            {
                textDescription.gameObject.SetActive(false);
                textWait = textWaitValue;
                hasInteracted = false;
            }
        }
    }

    // Interacting with objects.
    void Interact()
    {
        // If the player presses space, interact.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Raycasts for interacting.
            RaycastHit hit;
            var interactRay = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4f);

            #region Items & Objects
            if (hit.collider.tag == "Object")
            {
                hasInteracted = true;

                textWait = textWaitValue;                                                           // If the player manages to quickly find another object, reset timer.
                var objectScript = hit.collider.GetComponent<ObjectProperties>();                   // We know we've hit an object, so grab it's script.
                textDescription.gameObject.SetActive(true);                                         // Show text

                textDescription.GetComponent<TextMeshProUGUI>().text = objectScript.description;    // Set text.

                // If the item to activite is not null, activate it.
                if (objectScript.toSetActive != null)
                {
                    objectScript.toSetActive.SetActive(true);
                }
                else { return; }

                // If the object is an item, destroy itself and the gameobjects.
                if (objectScript.isKey)
                {
                    // Check if there is a list of doors that can be unlocked.
                    if (objectScript.doorsToUnlock.Length > 0)
                    {
                        // If so, go through them and set them to true.
                        foreach (GameObject doorObject in objectScript.doorsToUnlock)
                        {
                            doorObject.GetComponent<Door>().hasKey = true;      // Set doors to unlock
                            Destroy(hit.collider.gameObject);                   // Delete gameobject.  
                            audioSource.PlayOneShot(keyPickupClip, 0.5f);
                        }
                    }
                }
            }
            #endregion

            #region Doors
            if (hit.collider.tag == "Door")
            {
                hasInteracted = true;

                var doorScript = hit.collider.gameObject.GetComponent<Door>();  // Grab the script that has the door.

                if (!doorScript.busted)
                {
                    if (doorScript.hasKey)
                    {
                        // Todo; Add sound of opening door.
                        audioSource.PlayOneShot(doorUnlockClip, 0.5f);
                        hit.collider.gameObject.SetActive(false);      // Set door to non-visble.

                        if (doorScript.blockageToAppear != null)
                        {
                            doorScript.blockageToAppear.gameObject.SetActive(true);
                        }
                        else 
                        {
                            return;
                        }
                    }
                    else if (!doorScript.hasKey)
                    {
                        textWait = textWaitValue;                                                          // Reset timer
                        textDescription.gameObject.SetActive(true);                                        // Show text
                        textDescription.GetComponent<TextMeshProUGUI>().text = "I don't have a key...";    // Set text.
                    }
                }
                else if (doorScript.busted)
                {
                    textWait = textWaitValue;                                                          // Reset timer
                    textDescription.gameObject.SetActive(true);                                        // Show text
                    textDescription.GetComponent<TextMeshProUGUI>().text = "The door is jammed.";    // Set text.
                }
            }
            #endregion
        }
    }
}
