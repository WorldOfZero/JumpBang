using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathEffect : MonoBehaviour {

    public GameObject deathEffect;

    private bool enableParticleEffectsOnDisable = true;

    public void DisableParticles()
    {
        enableParticleEffectsOnDisable = false;
    }

    private void OnDisable()
    {
        if (enableParticleEffectsOnDisable)
        {
            var destroyed = Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            var particles = destroyed.GetComponent<ParticleSystem>();
            var theme = this.GetComponent<PlayerTheme>();
            particles.startColor = theme.playerTheme;
        }
    }
}
