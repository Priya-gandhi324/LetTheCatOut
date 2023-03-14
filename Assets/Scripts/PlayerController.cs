using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 20.0f;
    private float turnSpeed = 50.0f;
    public TextMeshProUGUI countText, winText;
    public GameObject gate;

    private Rigidbody rb;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count= 0;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * moveSpeed);

        // Rotate player based on horizontal input
        Vector3 rotation = Vector3.up * moveHorizontal * turnSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    // Called when the player collides with another object
    void OnCollisionEnter(Collision collision)
    {
        // If player collides with a wall, move it back to its previous position
        if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            count++;
            countText.text = "Score: " + count.ToString();

            if (count >= 11)
            {
                winText.gameObject.SetActive(true);
                gate.SetActive(false);
            }
        }
    }
}
