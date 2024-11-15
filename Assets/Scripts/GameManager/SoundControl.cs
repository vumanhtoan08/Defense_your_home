using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl instance;

    public AudioSource backgroundSource; 
    public AudioSource SFXSource;

    [Header("Clip background")]
    // background clip
    public AudioClip background1; 
    public AudioClip background2;

    [Header("Clip player")]
    // sfx clip 
    public AudioClip movingClip; 
    public AudioClip attackClip; 
    
    [Header("Clip enemy")]
    // enemy
    public AudioClip seleketonAttackClip;
    public AudioClip devilAttackClip;

    [Header("Clip die")]
    public AudioClip enemyDieClip;
    public AudioClip castleDieClip;

    private void Awake()
    {
        if (instance != null &&  instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayBGPeace();
    }

    public void PlayBGCombat()
    {
        backgroundSource.clip = background2;
        backgroundSource.Play();
    }

    public void PlayBGPeace()
    {
        backgroundSource.clip = background1;
        backgroundSource.Play();
    }

    public void PlayMovingClip()
    {
        SFXSource.PlayOneShot(movingClip);
    }

    public void PlayShootArrow()
    {
        SFXSource.PlayOneShot(attackClip);
    }

    public void PlayEnemyDie()
    {
        SFXSource.PlayOneShot(enemyDieClip);
    }

    public void PlayCastleDie()
    {
        SFXSource.PlayOneShot(castleDieClip);
    }

    public void PlaySeleketonAttack()
    {
        SFXSource.PlayOneShot(seleketonAttackClip);
    }

    public void PlayDevilAttack()
    {
        SFXSource.PlayOneShot(devilAttackClip);
    }

    public void MuteSound()
    {
        backgroundSource.mute = true; 
        SFXSource.mute = true; 
    }

    public void ActiveSound()
    {
        backgroundSource.mute = false;
        SFXSource.mute = false;
    }
}
