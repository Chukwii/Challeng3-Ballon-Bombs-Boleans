using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce, gravityModifier;
    public bool isOnGround = true;
    public bool jumpOnce = false;
    public bool gameOver = false;
    public bool hasStart = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle, dirtParticle;
    public AudioClip jumpSound, crashSound;
    private AudioSource playerAudio;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        hasStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(startMove());
        if (hasStart)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver == false)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                jumpOnce = true;
            }
            else if (jumpOnce && Input.GetKeyDown(KeyCode.Space) && isOnGround == false && gameOver == false)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                jumpOnce = false;
            }
            if (Input.GetKey(KeyCode.LeftShift) && isOnGround)
            {
                playerAnim.speed = 3f;
                MoveLeft.speed = 50f;
                float score = 0;
                score += 2 * Time.deltaTime;
                Debug.Log("Score: " + score);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && isOnGround)
            {
                playerAnim.speed = 1.5f;
                MoveLeft.speed = 30f;
                float score = 0;
                score += 1 * Time.deltaTime;
                Debug.Log("Score: " + score);
            }
            else
            {
                MoveLeft.speed = 30f;
            }
        }
        
    }

    IEnumerator startMove()
    {
        playerAnim.speed = 0.5f;
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), 0.08f);
        yield return new WaitForSeconds(0.8f);
        hasStart = true;
        playerAnim.speed = 1.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpOnce = false;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
