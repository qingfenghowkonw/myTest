using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public float Moveforce = 500f;
    public float jumpForce = 600f;
    public float MaxSpeed = 5.0f;
    private Rigidbody2D player;
    private bool bFaceRight = true;
    private Transform mGroundCheck;
    private bool bJump = false;
    void Start()
    {
        mGroundCheck = transform.Find("GroundCheck");
        player = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float fInput = Input.GetAxis("Horizontal");
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        if (fInput * rigidBody.velocity.x < MaxSpeed)
        {
            rigidBody.AddForce(Vector2.right * fInput * Moveforce);
        }

        if (Mathf.Abs(rigidBody.velocity.x) > MaxSpeed)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * MaxSpeed, rigidBody.velocity.y);
        }
        if (bFaceRight && fInput < 0)
            Flip();

        else if (!bFaceRight && fInput > 0)
            Flip();

        if (bJump)
        {
            player.AddForce(Vector2.up * jumpForce);
            bJump = false;
        }

    }

    void Update()
    {
        if (Physics2D.Linecast(transform.position, mGroundCheck.position,
            1 << LayerMask.NameToLayer("ground")))
        {
            if (Input.GetButtonDown("Jump"))
                bJump = true;

        }
        Debug.DrawLine(transform.position, mGroundCheck.position, Color.red);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;

    }
}