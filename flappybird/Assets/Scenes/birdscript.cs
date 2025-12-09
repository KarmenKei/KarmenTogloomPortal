using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class birdscript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jump = 10f;
    public float movespeed = 1f;
    public script lgc;
    public bool alive= true;
    public SpriteRenderer wingdown;
    public SpriteRenderer wingup;

    bool usingA = true;
    void Start()
    {
        lgc= GameObject.FindGameObjectWithTag("logic").GetComponent<script>();
        wingup = GameObject.FindGameObjectWithTag("wingup").GetComponent<SpriteRenderer>();
        wingdown = GameObject.FindGameObjectWithTag("wingdown").GetComponent<SpriteRenderer>();
        showWingDown();
    }

    void Update()
    {
        if (alive == false) return;
        if (Keyboard.current.spaceKey.wasPressedThisFrame && alive==true)
        {
            usingA = false;
            rb.linearVelocity = Vector2.up * jump;
            showWingUp();

        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            transform.position = transform.position + (Vector3.left * movespeed);
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            transform.position = transform.position + (Vector3.right * movespeed);
        }
        if (rb.linearVelocity.y < -0.01f) showWingUp();
        else if (rb.linearVelocity.y > 0.01f) showWingDown();
    }
    void showWingUp(){
        if (wingup) wingup.enabled = true;
        if (wingdown) wingdown.enabled = false;
    }
    void showWingDown() {
        if (wingup) wingup.enabled = false;
        if (wingdown) wingdown.enabled = true;
     }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lgc.gameover();
        alive = false;
    }
}
