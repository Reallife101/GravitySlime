using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

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
    private float default_speed;

    Vector3 gravity;
    Vector3 respawnPoint;
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
        respawnPoint = transform.position;
        default_speed = speed;
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
            if (transform.localScale.y < 0)
            {
                StartCoroutine(Lerp(lerpDuration, -1, 1));
            }
            else
            {
                StartCoroutine(Lerp(lerpDuration, 1, -1));
            }
            animator.SetBool("jump", true);
            am.playGravitySwitch(au);
        }
        
        // Kill player if they stop moving or fall off
        if((Mathf.Abs(rb.velocity.x) <= killSpeed  || Mathf.Abs(transform.position.y) > killDistance) && !dead)
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

        // Debug.Log(rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // reset gravity flip
        animator.SetBool("jump", false);
        am.playLandingSound(au);
        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.05f, .05f);
    }

    public void die()
    {
        animator.SetTrigger("death");
        StartCoroutine(turnOffplayer());
        speed = 0f;
        dead = true;
        am.bgmOff();
        am.playExplode(au);
    }
    
    public void respawn()
    {
        speed = default_speed;
        gameObject.SetActive(true);
        transform.position = respawnPoint;
        am.bgmOn();
        dead = false;
    }

    public void setRespawn(){
        respawnPoint = transform.position;
        Debug.Log("respawn: " + respawnPoint);
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
        respawn();
    }

}
