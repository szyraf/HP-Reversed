using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    public ParticleSystem myParticleSystem;
    public bool hasPlayed = false;

    public Hitbox hitbox;

    private bool hit = false;
    private int count = -1;


    void Start()
    {
        myParticleSystem.Stop();
        hitbox.gameObject.SetActive(false);
    }

    public void Hit()
    {
        hasPlayed = true;
    }

    void Update()
    {
        if (hasPlayed && !hit)
        {
            count = 15;
            hit = true;
            hasPlayed = false;
            myParticleSystem.Play();
            hitbox.basicTransform = transform;
            hitbox.gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().PlayOneShot("explosion");
            //hitbox.CheckColliders();
        }

        if (count > 0)
        {
            count--;
        }
        else if (count == 0)
        {
            Destroy(gameObject);
        }
    }
}
