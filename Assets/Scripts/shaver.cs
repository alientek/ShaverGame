using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaver : MonoBehaviour
{
    public AudioSource cuttingSound;
    public ParticleSystem shavings;
    public float fxTimeOut;

    public bool fxBusy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "strandColl")
        {
            Destroy(other.gameObject);
            if (!fxBusy)
            {
                StartCoroutine(CuttingFx());
            }
        }
    }

    IEnumerator CuttingFx()
    {
        fxBusy = true;
        cuttingSound.Play();
        shavings.Play();
        yield return new WaitForSeconds(fxTimeOut);
        cuttingSound.Pause();
        fxBusy = false;
    }
}
