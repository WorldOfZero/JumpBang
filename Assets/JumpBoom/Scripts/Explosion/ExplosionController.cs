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
            controller.AddVelocity((controller.transform.position - this.transform.position), force);
        }

        var destroyedBlocks = colliders.Select(collider => collider.GetComponent<Rigidbody2D>()).Where(controller => controller != null).Where(body => body.gameObject.layer == LayerMask.NameToLayer("Level"));
        
        if (destroyedBlocks.Count() > 0)
        {
            GameObject gobj = new GameObject();
            var newRigidbody = gobj.AddComponent<Rigidbody2D>();
            var forceSum = Vector3.zero;
            foreach (var block in destroyedBlocks)
            {
                gobj.layer |= block.gameObject.layer;
                block.transform.parent = gobj.transform;
                Destroy(block);
                forceSum += (block.transform.position - this.transform.position).normalized * force;
            }
            newRigidbody.AddForce(forceSum);
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
}
