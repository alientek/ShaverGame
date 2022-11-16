using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public float masterVolume;
    private float hitStrength;
    private AudioSource audioSource;
    private AudioClip tempFXSource;
    public AudioClip[] defaultFX;
    private AudioClip aClip;
    private GameObject[] props;
    public bool useCollision = true;
    public bool busy = true;

    private void Update()
    {
    }

    void OnCollisionStay(Collision collision)
    {
        tempFXSource = defaultFX[Random.Range(0, defaultFX.Length)];
        audioSource.clip = tempFXSource;

        foreach (ContactPoint contact in collision.contacts)
        {
            hitStrength = collision.impulse.magnitude * masterVolume;

            print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            // Visualize the contact point
            Debug.DrawRay(contact.point, contact.normal, Color.white);



            if (collision.impulse.magnitude > 1)
            {
                audioSource.clip = tempFXSource;
                audioSource.volume = hitStrength;
                audioSource.pitch = Random.Range(0.5f, 2.0f);
                audioSource.Play();
            }
            Debug.Log("Hitstrength: " + hitStrength);
        }
    }
}
