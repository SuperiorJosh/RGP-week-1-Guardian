using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class PooledAudio : MonoBehaviour
{
    private AudioSource m_audioSource;
    public IObjectPool<PooledAudio> Pool;
    public AudioSource Source => m_audioSource;
    private bool m_started = false;
    private bool m_finished = false;

    public void Setup(AudioSource audioSource)
    {
        m_audioSource = audioSource;
    }

    public void Play(AudioClip clip, float delay = 0.0f)
    {
        m_audioSource.clip = clip;
        m_audioSource.PlayDelayed(delay);
        m_started = true;
    }

    public void Play(AudioClip clip, float delay = 0.0f, float fadeDuration = 0.0f, float endVolume = 1f)
    {
        m_audioSource.clip = clip;
        m_audioSource.volume = 0f;
        m_audioSource.Play();
        m_audioSource.DOFade(endVolume, fadeDuration);
    }

    private void Update()
    {
        if (m_started)
        {
            if (!m_audioSource.isPlaying)
            {
                m_started = false;
                Pool.Release(this);
            }
        }
    }
}