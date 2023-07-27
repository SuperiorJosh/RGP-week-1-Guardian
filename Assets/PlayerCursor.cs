using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCursor : MonoBehaviour
{
    private UnityEvent m_overrideEvent;

    [SerializeField] private Image m_image;
    
    private struct CursorTargets
    {
        public Type Type;
        public Sprite CursorSprite;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OverrideCursor(Sprite cursor, UnityEvent endOverrideEvent)
    {
        m_overrideEvent = endOverrideEvent;
    }
}

public static class UIHelpers
{
    public static Vector3 GetWorldPosition()
    {
        return Vector3.zero;
    }

    public static Vector3 GetScreenPosition()
    {
        return Vector3.zero;
    }
}