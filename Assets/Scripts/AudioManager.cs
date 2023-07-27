using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private ObjectPool<PooledAudio> m_pool;

    private PooledAudio m_musicSource;
    
    private void Awake()
    {
        m_pool = new ObjectPool<PooledAudio>(CreatePooledAudioSource, OnGetFromPool, OnReturnedToPool,
            OnDestroyPoolAudioSource);
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroyPoolAudioSource(PooledAudio obj)
    {
        Destroy(obj.gameObject);
    }

    private void OnReturnedToPool(PooledAudio obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGetFromPool(PooledAudio obj)
    {
        obj.gameObject.SetActive(true);
    }

    private PooledAudio CreatePooledAudioSource()
    {
        var go = new GameObject("Pooled Audio Source");
        var pooledAudio = go.AddComponent<PooledAudio>();
        var audioSource = go.AddComponent<AudioSource>();
        pooledAudio.Setup(audioSource);
        go.transform.SetParent(transform);
        pooledAudio.Pool = m_pool;
        return pooledAudio;
    }

    public void Play(AudioClip clip, float delay = 0.0f)
    {
        var source = m_pool.Get();
        source.Play(clip, delay);
    }

    public void PlayMusic(AudioClip musicClip, float delay = 0.0f, float fadeDuration = 0.0f, float endVolume = 1f)
    {
        if (!m_musicSource)
            m_musicSource = m_pool.Get();
        m_musicSource.Play(musicClip, delay, fadeDuration, endVolume);
    }
}