using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class WpAnimScript : MonoBehaviour
{
    private Shooting fatherShooting;
    public ParticleSystem shootParticles;
    public GameObject source;
    private CameraShaker cam;

    [Header("Shake")]
    public float magnitude;
    public float roughness;

    private void Start()
    {
        fatherShooting = GameObject.Find("Player").GetComponent<Shooting>();
        cam = GameObject.Find("Shaker").GetComponent<CameraShaker>();
    }

    public void Shoot()
    {
        fatherShooting.Shoot();
    }
    public void ReadyToFire()
    {
        fatherShooting.readyToFire = true;
        transform.GetComponent<Animator>().SetBool("Shooting", false);
    }

    public void shootParticle()
    {
        shootParticles.Play();
    }

    public void playAudio()
    {
        source.GetComponent<AudioSource>().volume = .5f;
        source.GetComponent<AudioManagment>().PlayDifferentPitch();
    }

    public void ShakeCamera()
    {
        cam.ShakeOnce(magnitude, roughness, .1f, .5f);
    }

    public void Hide()
    {
        GameObject.Find("Player").GetComponent<WeaponTypes>().AfterHide();
        gameObject.SetActive(false);
    }

}
