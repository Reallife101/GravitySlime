using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 12.0f;
    [SerializeField] Animator animator;
    [SerializeField] float speed = 6.0f;

    Vector3 gravity;
    bool isGrounded;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = Physics.gravity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //apply current gravity
        rb.AddForce(gravity, ForceMode.Acceleration);

        //Apply forward movement
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    private void Update()
    {
        // Flip gravity and play animation
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravity = -gravity;
            StartCoroutine(Lerp(1f, transform.localScale.y, -transform.localScale.y));
            animator.SetBool("jump", true);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // reset gravity flip
        isGrounded = true;
        animator.SetBool("jump", false);
    }

    // Lerp animation for flipping gravity
    IEnumerator Lerp(float lerpDuration, float start, float end)
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(start, end, timeElapsed / lerpDuration), transform.localScale.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(transform.localScale.x, end, transform.localScale.z);
    }

}
