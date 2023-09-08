using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagment : MonoBehaviour
{
    public AudioSource source;
    float life;
    public bool DestroyItself;
    public bool changePitch;
    public bool RandomClips;
    public bool disableItself;
    public int clipsSize;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        if (RandomClips) source.clip = clips[Random.Range(0, clipsSize)];
        source = GetComponent<AudioSource>();
        life = source.clip.length;
        if(changePitch) source.pitch = Random.Range(.5f, 1.5f);
        if(DestroyItself) transform.parent = null;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (DestroyItself)
        {
            life -= Time.deltaTime;
            if (life <= 0) Destroy(gameObject);
        }
    }

    public void PlayDifferentPitch()
    {
        if (changePitch) source.pitch = Random.Range(.8f, 1.2f);
        this.GetComponent<AudioSource>().Play();
    }
}
