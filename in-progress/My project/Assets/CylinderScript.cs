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
