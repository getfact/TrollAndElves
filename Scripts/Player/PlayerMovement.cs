using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed = 50f;
    public float runSpeed = 100f;
    public float rotateSpeed = 10f;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private bool isRunning = false;

    private Rigidbody myRigidbody;
    private Animator myAnimator;

    void Awake () {

        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }

    void Update () {

        CheckInput();
        Move();
        Rotate();
    }

    private void CheckInput () {

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(horizontal, 0, vertical).normalized;

        if (isRunning)
            moveVelocity = moveInput * runSpeed;
        else
            moveVelocity = moveInput * walkSpeed;

        if (Input.GetButtonDown("Jump")) {

            isRunning = true;
        }

        if (Input.GetButtonUp("Jump")) {

            isRunning = false;
        }
    }

    private void Move () {

        myRigidbody.velocity = moveVelocity * Time.deltaTime;

        myAnimator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) +
                                      Mathf.Abs(Input.GetAxis("Horizontal"))));

        myAnimator.SetBool("IsRunning", isRunning);
    }

    private void Rotate () {

        if (moveVelocity != Vector3.zero) {

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(moveVelocity),
                                                  Time.deltaTime * rotateSpeed);
        }
    }
}
