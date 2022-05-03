//Script from Brackeys YouTube, https://www.youtube.com/watch?v=_QajrabyTJc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovementScript : MonoBehaviour
{
    public GameObject PauseCanvas;

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public GameObject Player;
    Animator Character_Animator;

    void Start()
    {
        Character_Animator = Player.GetComponent<Animator>();
        Character_Animator.SetBool("CharWalk", false);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(PauseCanvasCo());
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Application.Quit();
        }


        if (Input.GetKey(KeyCode.C))
        {
            controller.height = 2f;
        }
        else
        {
            controller.height = 3.8f;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Character_Animator.SetBool("CharWalk", true);
        }
        else
        {
            Character_Animator.SetBool("CharWalk", false);
        }

    }

    IEnumerator PauseCanvasCo()
    {
        PauseCanvas.SetActive(!PauseCanvas.activeInHierarchy);
        yield return new WaitForSeconds(0.5f);
        yield break;
    }

}
