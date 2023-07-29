using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
    [SerializeField] float WalkSpeed = 5f;
    float Walk;
    Rigidbody rb;
    Vector3 direction;
    [SerializeField] float RunSpeed = 10f;
    [SerializeField] float Jump = 7f;
    bool isGrounded = true;
    [SerializeField] Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Walk = WalkSpeed;
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if(direction.x != 0 || direction.z != 0)
        {
            anim.SetBool("Walk", true);
        }
        if(direction.x == 0 && direction.z == 0)
        {
            anim.SetBool("Walk", false);
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("Jump", true);
            rb.AddForce(new Vector3(0, Jump, 0), ForceMode.Impulse);
            isGrounded = false;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Run", true);
            Walk = RunSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("RunANDJump", true);
            isGrounded = false;
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("RunANDJump", false);
            Walk = WalkSpeed;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * Walk * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision) 
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
        anim.SetBool("RunANDJump", false);
    }
}
