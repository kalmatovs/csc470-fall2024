using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f; // Speed at which the bullet moves
    public float lifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hits a "Collectable"
        if (other.CompareTag("Collectable"))
        {
            // Destroy the collectable object and the bullet
            Destroy(other.gameObject);
            Destroy(gameObject);

            // Access the plane's script to increase the score
            PlaneScript plane = FindObjectOfType<PlaneScript>();
            if (plane != null)
            {
                plane.IncreaseScore();
            }
        }
    }
}
