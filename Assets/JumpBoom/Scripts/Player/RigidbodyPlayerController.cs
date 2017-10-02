using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RigidbodyPlayerController : MonoBehaviour {

    public Rigidbody2D rigidbody;
    public PlayerInputController input;

    public bool grounded;
    public LayerMask groundedMask;
    public float horizontalSpeed;
    public float inAirHorizontalSpeed;
    public float friction;
    public float jumpStrength;

    public GameObject bomb;

    public int storedBombs = 3;
    private Queue<ExplosionController> bombs = new Queue<ExplosionController>();

    public float dashDistance = 0.5f;
    public float dashVelocity = 10f;
    private bool dashAvailable = true;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        var calculatedVelocity = Vector2.zero;
        calculatedVelocity.x += input.x * (grounded ? horizontalSpeed : inAirHorizontalSpeed);

        if (grounded)
        {
            //velocity.y = Mathf.Max(0, velocity.y);
            if (input.y > 0.5f)
            {
                Jump();
            }

            //velocity.x = Mathf.Sign(velocity.x) * Mathf.Max(Mathf.Abs(velocity.x) - Time.fixedDeltaTime * friction, 0);
        }
        else
        {
            //velocity += Physics2D.gravity * Time.fixedDeltaTime;
        }
        rigidbody.AddForce(calculatedVelocity * Time.fixedDeltaTime, ForceMode2D.Impulse);

        grounded = IsGrounded();

        if (input.dropBomb) //storedBombs > 0 && 
        {
            var droppedBomb = Instantiate(bomb, this.transform.position, this.transform.rotation);
            var theme = GetComponent<PlayerTheme>();
            var bombTheme = bomb.GetComponent<PlayerTheme>();
            bombTheme.playerTheme = theme.playerTheme;
            bombs.Enqueue(droppedBomb.GetComponent<ExplosionController>());

            if (bombs.Count > 3)
            {
                input.explodeBomb = true;
            }
            //storedBombs -= 1;
        }

        if (input.dash && dashAvailable)
        {
            dashAvailable = !Dash();
        }
        
        if (bombs.Count > 0)
        {
            var tempBomb = bombs.Peek();
            if (tempBomb == null)
            {
                bombs.Dequeue();
                return;
            }
            tempBomb.Select();

            if (input.explodeBomb)
            {
                var bomb = bombs.Dequeue();
                bomb.Explode();
            }
        }
    }

    private bool Dash()
    {
        if (Mathf.Abs(input.x) > 0.1f || Mathf.Abs(input.y) > 0.1f)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                {
                    // Right dash
                    Dash(Vector2.right);
                }
                else
                {
                    // Left dash
                    Dash(Vector2.left);
                }
            }
            else // Vertical Dash
            {
                if (input.y > 0)
                {
                    Dash(Vector2.up);
                }
                else
                {
                    Dash(Vector2.down);
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Dash(Vector2 direction)
    {
        rigidbody.velocity = direction * dashVelocity;
        rigidbody.MovePosition((Vector2)this.transform.position + direction * dashDistance);
    }

    internal void AddVelocity(Vector2 direction, float force)
    {
        //velocity += direction.normalized * force;
        rigidbody.AddForce(direction.normalized * force, ForceMode2D.Force);
    }

    private void Jump()
    {
        //velocity.y = jumpStrength;
        rigidbody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Force);
        grounded = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.CircleCastAll(this.transform.position, 0.5f, Vector2.down, 0.1f, groundedMask).Any(body => body.transform != rigidbody.transform);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        dashAvailable = true;
    }
}
