using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed = 2;               // The amount of tiles we want to move by.
    public float cooldownValue = 0.5f;      // The value of the cooldown.
    public bool canWalk = true;

    private float walkCooldown;              // The cooldown needed for the player to walk (Default 0.2f;)
    private float currentRotation = 0;      // The rotation of the player.

    // Sounds
    public AudioSource audioSource;
    public AudioClip[] walkingGrass;

    private void Start()
    {
        walkCooldown = cooldownValue;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player can walk, let them do it!;
        if (canWalk)
        {
            Movement();
        }
        // If the player can't walk, start the timer.
        else if (!canWalk)
        {
            walkCooldown -= Time.deltaTime;

            // Once the timer is done, set the values back to their original states.
            if (walkCooldown < 0f) { canWalk = true; walkCooldown = cooldownValue; }
        }
    }

    // The whole movement script for the player.
    void Movement()
    {
        // Wall detection. 
        var wallFrontDetected = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 2.1f); // CHANGE BACK TO 2F IF DONE
        var wallBackDetected = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), 2.1f); // CHANGE BACK TO 2F IF DONE

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2.1f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 2.1f, Color.green);

        // Set rotation.
        transform.rotation = Quaternion.Euler(0, currentRotation, 0);   

        // Moving forwards
        if (Input.GetKeyDown(KeyCode.W) && !wallFrontDetected)
        {
            transform.position += transform.forward * moveSpeed;
            canWalk = false;
            SteppingSound();
        }

        if (Input.GetKeyDown(KeyCode.S) && !wallBackDetected)
        {
            transform.position -= transform.forward * moveSpeed;
            canWalk = false;
            SteppingSound();
        }

        // Rotating Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentRotation -= 90f;
        }
        // Rotating Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentRotation += 90f;
        }
    }

    void SteppingSound()
    {
        int randomClip = Random.Range(0, walkingGrass.Length);
        audioSource.clip = walkingGrass[randomClip];
        audioSource.PlayOneShot(audioSource.clip, 0.5f);
    }
}
