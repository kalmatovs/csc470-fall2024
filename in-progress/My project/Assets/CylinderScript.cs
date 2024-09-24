using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CylinderScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public Rigidbody rb;
    int score = 0;    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.useGravity = true;
        }
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("Hello?");
        Destroy (other.gameObject);
        score++;
        if(score == 1) {
            scoreText.text = "You win";
        } else {
            scoreText.text = "Score: " + score;
        }
    }
}

public GameObject cameraObject;

float forwardSpeed = 20f;
float xRotationSpeed = 90f;
float yRotationSpeed = 90f;


// its all inside of update in the planescript
// float haxis = Input.GetAxis('Horizontal');
// float vaxis = Input.GetAxis('Vertical');

// Vector amountToRotate = new Vector3(0,0,0);
amountToRotate.x = vaxis * xRotationSpeed;
amountToRotate.y = hAxis * yRotationSpeed;
amounntToRotate *= Time.deltaTime;
transform.Rotate(amountToRotate, Space.Self);

transform.position += transform.forward * forwardSpeed * Time.deltaTime;

Vector3 cameraPosition = transform.positionl
cameraPosition += -transform.forward * 10f;
cameraPosition += Vector3.up * 8f;
cameraObject.transform.position = cameraPosition;

cameraObject.transform.LookAt(transform.position);

create a Gamemanager object 


