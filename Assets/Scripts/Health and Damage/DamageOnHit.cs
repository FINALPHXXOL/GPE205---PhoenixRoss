using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;
    public AudioSource hitExplosion;
    public AudioClip hitExplode;

    public void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.gameObject.GetComponent<Health>();
        AudioSource otherAudio = other.gameObject.GetComponent<AudioSource>();
        if (otherHealth != owner.hp)
        {
            if (otherHealth != null)
            {
                Debug.Log(otherHealth.name);
                otherAudio.Play();
                //AudioSource.PlayClipAtPoint(hitExplode, otherHealth.transform.position, 0f);
                otherHealth.TakeDamage(damageDone, owner);
            }
            Destroy(gameObject);
        }
        
    }
}
