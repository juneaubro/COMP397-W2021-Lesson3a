using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementForce = 5f;
    [SerializeField]
    private float jumpForce = 0.35f;
    private bool isGrounded;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
                rb.AddForce(Vector3.right * movementForce);

            if (Input.GetAxisRaw("Horizontal") < 0)
                rb.AddForce(Vector3.left * movementForce);

            if (Input.GetAxisRaw("Vertical") > 0)
                rb.AddForce(Vector3.forward * movementForce);

            if (Input.GetAxisRaw("Vertical") < 0)
                rb.AddForce(Vector3.back * movementForce);

            if (Input.GetAxisRaw("Jump") > 0)
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
