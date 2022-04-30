using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float gravityMultiplier = 1.0f;
    [SerializeField] float lerpDuration = 0.5f;

    // Kill player variables
    [SerializeField] float killDistance = 15f;
    [SerializeField] float killSpeed = 1f;

    // Ground Check
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] AudioSource footsteps;

    // Player Attributes
    public float speed = 6.0f;
    public bool canChangeGravity = true;
    public bool infiniteGravSwitch = false;

    Vector3 gravity;
    bool isGrounded;
    Rigidbody rb;
    bool dead = false;
    AudioSource au;
    AudioManager am;
    bool playFootsteps = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = Physics.gravity * gravityMultiplier;
        au = GetComponent<AudioSource>();
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
        //Grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Flip gravity and play animation
        if (Input.GetButtonDown("Jump") && (isGrounded || infiniteGravSwitch) && canChangeGravity)
        {
            gravity = -gravity;
            StartCoroutine(Lerp(lerpDuration, transform.localScale.y, -transform.localScale.y));
            animator.SetBool("jump", true);
            am.playGravitySwitch(au);
        }
        
        // Kill player if they stop moving or fall off
        if((Mathf.Abs(rb.velocity.x) <= killSpeed && !dead) || Mathf.Abs(transform.position.y) > killDistance)
        {
            die();
        }

        if (isGrounded && playFootsteps)
        {
            am.playFootsteps(footsteps);
            playFootsteps = false;
        }
        else if (!isGrounded && !playFootsteps)
        {
            am.stopFootsteps(footsteps);
            playFootsteps = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // reset gravity flip
        animator.SetBool("jump", false);
        am.playLandingSound(au);
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
