using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 1.0f;

    private Animator anim;
    private Rigidbody2D body;
    void Start()
    {
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
        if (!Mathf.Approximately(deltaX, 0.0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }
    }
}
