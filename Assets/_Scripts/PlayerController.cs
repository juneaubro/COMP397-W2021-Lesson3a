using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Movement Properties")]
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float gravity = -30f;
    [SerializeField]
    private float jumpHeight = 3f;

    [Header("Ground Detection Properties")]
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float groundRadius = 0.5f;

    [Header("Player Sounds")]
    public AudioSource jumpSound;
    public AudioSource hitSound;

    [Header("HealthBar")]
    public HealthBarScreenSpaceController healthbar;

    [Header("Player Abilities")]
    [Range(0, 100)]
    public static int health = 100;

    private GameObject MiniMap;

    private Vector3 velocity;
    private bool toggle = false;

    public static CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        MiniMap = GameObject.Find("MiniMapCanvas");
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * maxSpeed * Time.deltaTime);

        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpSound.Play();
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.M))
        {
            MiniMap.SetActive(toggle);

            toggle = !toggle;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.TakeDamage(damage);
        hitSound.Play();
    }
}
