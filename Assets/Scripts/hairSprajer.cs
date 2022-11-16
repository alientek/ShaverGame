using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class hairSprajer : MonoBehaviour
{
    public SteamVR_Input_Sources inputController;
    public SteamVR_Action_Single triggerSqueezeForce;

    public Camera cam;
    public GameObject hairGo;
    public float sprajRange = 0.25f;
    public float hairSpawnDelay;
    private bool allowHairSpawn = true;

    public ParticleSystem sprayParticle;
    public AudioSource spraySound;
    private grabbable grabbableControl;

    public LayerMask layerToHit;

    private void Start()
    {
        grabbableControl = transform.GetComponent<grabbable>();
    }

    //I mean, mesh deformation for hair would be cool.
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
                if (hit.collider.tag == "hairColl" && allowHairSpawn)
                {
                    StartCoroutine(SpawnHairDelay());
                    var hairObject = Instantiate(hairGo);
                    hairObject.transform.parent = null;
                    hairObject.transform.localScale = new Vector3(100, 100, 100);
                    hairObject.transform.position = hit.point;
                    hairObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * Quaternion.Euler(new Vector3(-90, 0, 0));
                    hairObject.transform.parent = hit.transform;
                    hairObject.SetActive(true);
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
