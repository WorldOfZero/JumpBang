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
    public float horizontalSpeed;
    public float inAirHorizontalSpeed;
    public float friction;
    public float jumpStrength;

    public GameObject bomb;

    public int storedBombs = 3;
    private Queue<ExplosionController> bombs = new Queue<ExplosionController>();

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

        if (storedBombs > 0 && input.dropBomb)
        {
            var droppedBomb = Instantiate(bomb, this.transform.position, this.transform.rotation);
            bombs.Enqueue(droppedBomb.GetComponent<ExplosionController>());
            storedBombs -= 1;
        }

        if (bombs.Count > 0)
        {
            var tempBomb = bombs.Peek();
            tempBomb.Select();

            if (input.explodeBomb)
            {
                var bomb = bombs.Dequeue();
                bomb.Explode();
            }
        }
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
    }

    private bool IsGrounded()
    {
        return Physics2D.CircleCastAll(this.transform.position, 0.5f, Vector2.down, 0.1f).Any(body => body.transform != rigidbody.transform);
    }
}
