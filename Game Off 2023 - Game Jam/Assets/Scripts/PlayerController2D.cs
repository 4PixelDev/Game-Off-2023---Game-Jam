using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpVelocity = 7f;
    [SerializeField] private float smallScale = 0.5f; // Define the smaller scale
    [SerializeField] private float normalScale = 1f; // Define the normal scale
    [SerializeField] private float sizeChangeCooldown = 1f; // Cooldown duration in seconds

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool canChangeSize = true; // Flag to check if the player can change size

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            rb.velocity = Vector2.up * jumpVelocity; // Jump 
        }

        if (Input.GetKeyDown(KeyCode.Z) && canChangeSize)
        {
            ChangeSize();
            StartCoroutine(SizeChangeCooldown());
        }
    }

    void FixedUpdate()
    {
        Move(); // Player movement
    }

    void Move()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    void ChangeSize()
    {
        // Toggle between normal and smaller scale
        if (transform.localScale.x == normalScale)
        {
            transform.localScale = new Vector3(smallScale, smallScale, 1f);
        }
        else
        {
            transform.localScale = new Vector3(normalScale, normalScale, 1f);
        }
    }

    System.Collections.IEnumerator SizeChangeCooldown()
    {
        canChangeSize = false; // Disable size change
        yield return new WaitForSeconds(sizeChangeCooldown);
        canChangeSize = true; // Enable size change after the cooldown
    }
}
