using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip swordSound, hurtSound, logHurtSound, bowSound, healthSound;
    static AudioSource audioSrc;
    
    // Start is called before the first frame update
    void Start()
    {
        swordSound = Resources.Load<AudioClip>("Sword");
        hurtSound = Resources.Load<AudioClip>("Hurt");
        logHurtSound = Resources.Load<AudioClip>("LogHurt");
        bowSound = Resources.Load<AudioClip>("Bow");
        healthSound = Resources.Load<AudioClip>("Health");
        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Sword": audioSrc.PlayOneShot(swordSound); break;
            case "Hurt": audioSrc.PlayOneShot(hurtSound); break;
            case "LogHurt": audioSrc.PlayOneShot(logHurtSound); break;
            case "Bow": audioSrc.PlayOneShot(bowSound); break;
            case "Health": audioSrc.PlayOneShot(healthSound); break;
        }
    }
}
