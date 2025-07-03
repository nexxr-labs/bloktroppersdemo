using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMovement : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    public Transform cam;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rotation tipping
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(h, 0f, v).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            rb.MovePosition(rb.position + moveDir.normalized * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }

        animator.SetBool("isWalking", moveDirection.magnitude >= 0.1f);
    }
}