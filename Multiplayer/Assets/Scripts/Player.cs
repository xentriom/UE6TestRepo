using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    public float speed = 10f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;
    private float jumpForce;
    private bool isGrounded;

    void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            return;
        }
        rb = GetComponent<Rigidbody>();

        jumpForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * rb.mass);
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            Jump();
            CheckUnstuck();
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void CheckUnstuck()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.position += Vector3.up * 2;
            transform.rotation = Quaternion.identity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
