using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneScript : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public TMP_Text timerText;

    public GameObject bulletPrefab;

    public TMP_Text scoreText;

    public GameObject cameraObject;

    // These variables will control how the plane moves
    float forwardSpeed = 12f;
    float xRotationSpeed = 90f;
    float yRotationSpeed = 90f;

    float boostTime;

    public Terrain terrain;
    int score = 0;
    bool isStopped = false;

    public float timeRemaining = 60f;  // 60 seconds countdown
    private bool timerRunning = true;
    void Start()
    {
        scoreText.text = "Score: " + score;
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update()
    {

        if (isStopped)
        {
            // If the plane has collided with a bomb, stop updating movement and rotation.
            return;
        }

        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;  // Decrease time by the amount of time passed since the last frame
                UpdateTimerDisplay();  // Update the timer text on the screen
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                EndGame();  // Trigger game over when time runs out
            }
        }
        
        float hAxis = Input.GetAxis("Horizontal"); // -1 if left is pressed, 1 if right is pressed, 0 if neither
        float vAxis = Input.GetAxis("Vertical"); // -1 if down is pressed, 1 if up is pressed, 0 if neither

        // Apply the rotation based on the inputs
        Vector3 amountToRotate = new Vector3(0,0,0);
        amountToRotate.x = vAxis * xRotationSpeed;
        amountToRotate.y = hAxis * yRotationSpeed;
        amountToRotate *= Time.deltaTime; // amountToRotate = amountToRotate * Time.deltaTime;
        transform.Rotate(amountToRotate, Space.Self);

        // Make the plane move forward by adding the forward vector to the position.
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        // Position the camera
        Vector3 cameraPosition = transform.position;
        cameraPosition += -transform.forward * 10f; // Negative forward points in the opposite direction as forward
        cameraPosition += Vector3.up * 8f; // Vector3.up is (0,1,0)
        cameraObject.transform.position = cameraPosition;
        // LookAt is a utility function that rotates a transform so that it looks at a point
        cameraObject.transform.LookAt(transform.position);

        float terrainHeight = terrain.SampleHeight(transform.position);
        if (transform.position.y < terrainHeight) {
            forwardSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootBullet();
        }

    }
    void ShootBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instantiate a bullet at the bullet spawn point and shoot it forward
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerDisplay()
    {
        // Convert the time to minutes and seconds format and update the UI text
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        // Logic to handle when the timer hits zero (e.g., stop the plane, show game over message)
        isStopped = true;  // Stop the game
        timerText.text = "Game Over!";
        Destroy(gameObject);  // Display game over message
    }

    public void OnTriggerEnter(Collider other)
    {
        // 'other' is the name of the collider that just collided with the object
        // that this script is attached to (the plane).
        // Check to see that it has the tag "collectable". Tags are assigned in the Unity editor.

        if (other.CompareTag("Collectable")) {
            Destroy(other.gameObject);
            score++;
            scoreText.text = "Score: " + score;
            if (score >= 7){
            timerRunning = false;  // Stop the timer
            timerText.text = "You Win!";
            isStopped = true;  // Stop the game
        }
        }  else if (other.CompareTag("Bomb")) {
            isStopped = true;
            scoreText.text = "You lost :(";

            Rigidbody rb = GetComponent<Rigidbody>();
            
            rb.velocity = Vector3.zero; // Stops the object from moving
            rb.angularVelocity = Vector3.zero; // Stops any rotation, if needed
            
        }
        }
    }

