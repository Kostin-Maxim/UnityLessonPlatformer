using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpForce = 1.0f;

    private BoxCollider2D box;
    private Animator anim;
    private Rigidbody2D body;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        anim.SetFloat("Speed", Mathf.Abs(deltaX));
        Vector2 movement = new Vector2 (deltaX, body.velocity.y);
        body.velocity = movement;

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool isGround = false;
        if (hit != null) 
        { 
            isGround = true;
        }

        body.gravityScale = (isGround && Mathf.Approximately(deltaX, 0)) ? 0: 1;
        if (isGround && Input.GetKeyDown(KeyCode.Space)) 
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        MovingPlatform platform = null;
        if(hit != null) 
        { 
            platform = hit.GetComponent<MovingPlatform>();
        }
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        anim.SetFloat("Speed", Mathf.Abs(deltaX));

        Vector3 pScale = Vector3.one;
        if (platform != null)
        { 
            pScale = platform.transform.localScale;
        }


        if (!Mathf.Approximately(deltaX, 0.0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1/pScale.y, 1);
        }
    }
}
