using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMovement : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    public Transform cam;

    private Rigidbody rb;
    private Vector3 moveDirection;

    // UI drag input
    public Vector2 uiInput; // This should be set by your UI drag script

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float h = uiInput.x != 0 ? uiInput.x : Input.GetAxisRaw("Horizontal");
        float v = uiInput.y != 0 ? uiInput.y : Input.GetAxisRaw("Vertical");

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

    public void SetUIInput(Vector2 input)
    {
        uiInput = input;
    }
}
