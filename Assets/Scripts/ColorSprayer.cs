using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ColorSprayer : MonoBehaviour
{
    public SteamVR_Input_Sources inputController;
    public SteamVR_Action_Single triggerSqueezeForce;

    public Color hairColor;
    public Camera cam;

    public float sprajRange = 0.25f;
    public float hairSpawnDelay;
    private bool allowHairSpawn = true;

    public ParticleSystem sprayParticle;
    public AudioSource spraySound;
    private grabbable grabbableControl;

    private void Start()
    {
        var canRenderer = transform.GetComponent<Renderer>();
        canRenderer.material.SetColor("_Color", hairColor);
        grabbableControl = transform.GetComponent<grabbable>();
    }

    // Of course a texture painting shader would be better!
    void Update()
    {
        inputController = grabbableControl.inputController;

        if (triggerSqueezeForce[inputController].axis > 0.9f)
        {
            RaycastHit hit;
            cam.transform.localEulerAngles = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), Random.Range(-15, 15));
            sprayParticle.Play();

            if (!spraySound.isPlaying)
            {
                spraySound.Play();
            }


            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, sprajRange))
            {
                if (hit.collider.tag == "strandColl" && allowHairSpawn)
                {
                    StartCoroutine(SpawnHairDelay());
                    var hairRenderer = hit.collider.transform.GetComponent<Renderer>();
                    hairRenderer.material.SetColor("_Color", hairColor);
                }
            }
        }
        else
        {
            cam.transform.localEulerAngles = Vector3.zero;
            spraySound.Stop();
            sprayParticle.Stop();
        }
    }

    public void StopAll()
    {
        spraySound.Stop();
        sprayParticle.Stop();
    }

    IEnumerator SpawnHairDelay()
    {
        allowHairSpawn = false;
        yield return new WaitForSeconds(hairSpawnDelay);
        allowHairSpawn = true;
    }
}
