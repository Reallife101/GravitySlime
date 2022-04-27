using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float gravityMultiplier = 1.0f;
    [SerializeField] float lerpDuration = 0.5f;
    public float speed = 6.0f;
    public bool canChangeGravity = true;
    public bool infiniteGravSwitch = false;

    Vector3 gravity;
    bool isGrounded;
    Rigidbody rb;
    bool dead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = Physics.gravity * gravityMultiplier;
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
        if (Input.GetButtonDown("Jump") && (isGrounded || infiniteGravSwitch) && canChangeGravity)
        {
            gravity = -gravity;
            StartCoroutine(Lerp(lerpDuration, transform.localScale.y, -transform.localScale.y));
            animator.SetBool("jump", true);
            isGrounded = false;
        }
        
        if(rb.velocity.x == 0 && !dead)
        {
            die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // reset gravity flip
        isGrounded = true;
        animator.SetBool("jump", false);
    }

    public void die()
    {
        animator.SetTrigger("death");
        StartCoroutine(turnOffplayer());
        speed = 0f;
        dead = true;
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

    IEnumerator turnOffplayer()
    {
        yield return new WaitForSeconds(.75f);
        gameObject.SetActive(false);
    }

}
