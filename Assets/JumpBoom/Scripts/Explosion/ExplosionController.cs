using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExplosionController : MonoBehaviour {

    public AnimationCurve sizeOverTime;
    public float time;
    public float maxSize;

    private bool exploding = false;
    private float timer = 0;

    public Animator explosionAnimator;
    public float force;
	
	// Update is called once per frame
	void Update () {
        if (!exploding) return;

        timer += Time.deltaTime;
        if (timer > time)
        {
            Destroy(this.gameObject);
        }

        this.transform.localScale = Vector3.one * maxSize * sizeOverTime.Evaluate(timer);
	}

    private void HandleExplosionPhysics()
    {
        var colliders = Physics2D.OverlapCircleAll(this.transform.position, maxSize * 0.5f);
        foreach (var controller in colliders.Select(collider => collider.GetComponent<RigidbodyPlayerController>()).Where(controller => controller != null))
        {
            var theme = controller.GetComponent<PlayerTheme>();
            var bulletTheme = GetComponent<PlayerTheme>();
            if (theme.playerTheme == bulletTheme.playerTheme)
            {
                controller.AddVelocity((controller.transform.position - this.transform.position), force);
            }
            else
            {
                Destroy(controller.gameObject);
            }
        }

        var destroyedBlocks = colliders.Select(collider => collider.GetComponent<Rigidbody2D>()).Where(controller => controller != null).Where(body => body.gameObject.layer == LayerMask.NameToLayer("Level"));
        
        if (destroyedBlocks.Count() > 0)
        {
            //GameObject gobj = new GameObject();
            //var newRigidbody = gobj.AddComponent<Rigidbody2D>();
            //var forceSum = Vector3.zero;
            foreach (var block in destroyedBlocks)
            {
                //    gobj.layer |= block.gameObject.layer;
                block.bodyType = RigidbodyType2D.Dynamic;
                block.transform.parent = null; // gobj.transform;
            //    Destroy(block);
                block.AddForce((block.transform.position - this.transform.position).normalized * force);
            }
            //newRigidbody.AddForce(forceSum);
        }

        var bombsInProximity = colliders.Select(collider => collider.GetComponent<Rigidbody2D>()).Where(controller => controller != null).Where(body => body.gameObject.layer == LayerMask.NameToLayer("Bomb"));
        foreach (var bomb in bombsInProximity)
        {
            bomb.AddForce((bomb.transform.position - this.transform.position).normalized * force);
        }
    }

    public void Explode()
    {
        exploding = true;
        explosionAnimator.SetTrigger("Explode");
        HandleExplosionPhysics();
    }

    public void Select()
    {
        explosionAnimator.SetTrigger("Selected");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }
}
