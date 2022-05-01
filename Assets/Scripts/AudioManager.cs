using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip gravitySwitch;
    [SerializeField] List<AudioClip> landingSounds;
    [SerializeField] AudioClip explode1;

    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void playGravitySwitch(AudioSource au)
    {
        au.PlayOneShot(gravitySwitch, .5f);
    }

    public void playGravitySwitch()
    {
        audio.PlayOneShot(gravitySwitch, .5f);
    }

    public void playLandingSound(AudioSource au)
    {
        float pitch = au.pitch;
        au.pitch = Random.Range(0f, 3f);
        au.PlayOneShot(landingSounds[Random.Range(0, landingSounds.Count)], .4f);
        au.pitch = pitch;
    }

    public void playFootsteps(AudioSource au)
    {
        au.UnPause();
    }
    public void stopFootsteps(AudioSource au)
    {
        au.Pause();
    }

    public void bgmOff()
    {
        audio.Pause();
    }

    public void bgmOn()
    {
        audio.UnPause();
    }

    public void playExplode(AudioSource au)
    {
        au.PlayOneShot(explode1, 1f);
    }

}
