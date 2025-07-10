using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource, sfxSource;
    public AudioClip bgdSound, moneySound, timeSound, defeatSound, pullSound, targetSound, explosiveSound, winSound;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void PlayLoop(AudioSource source, AudioClip clip, float volumne)
    {
        source.clip = clip;
        source.volume = volumne;
        source.loop = true;
        source.Play();
    }
    public void PlayBackgroundSound()
    {
        PlayLoop(musicSource, bgdSound, 0.5f);
    }
    public void StopBackgroundSound()
    {
        musicSource.Stop();
    }
    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    public void PlayMoneySound()
    {
        PlaySFX(moneySound);
    }
    public void PlayTimeSound()
    {
        PlaySFX(timeSound);
    }
    public void PlayDefeatSound()
    {
        PlaySFX(defeatSound);
    }
    public void PlayPullSound()
    {
        PlaySFX(pullSound);
    }
    public void PlayTargetSound()
    {
        PlaySFX(targetSound);
    }
    public void PlayExplosiveSound()
    {
        PlaySFX(explosiveSound);
    }
    public void PlayWinSound()
    {
        PlaySFX(winSound);
    }
    public void PlayItemTouch(AudioClip clip)
    {
        PlaySFX(clip);
    }
}
