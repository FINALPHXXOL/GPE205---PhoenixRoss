using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource deathAudio;
    public AudioClip deathClip;

    public AudioSource pickupAudio;
    public AudioClip pickupClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void PlayDeathSound()
    {
        deathAudio.PlayOneShot(deathClip);
    }

    public void PlayPickupSound()
    {
        pickupAudio.PlayOneShot(pickupClip);
    }
}