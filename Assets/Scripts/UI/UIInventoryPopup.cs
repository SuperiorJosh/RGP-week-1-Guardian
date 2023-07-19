using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPopup : MonoBehaviour
{
    [SerializeField] private Button m_useBtn;
    [SerializeField] private Button m_combineBtn;
    private UIInventoryIcon m_iconRef;
    private UIInventoryIcon m_combineTarget;
    private bool m_active = false;
    public bool Active => m_active;
    
    private void Start()
    {
        m_useBtn.onClick.AddListener(OnUseButtonClicked);
        m_combineBtn.onClick.AddListener(OnCombineButtonClicked);
        
        gameObject.SetActive(false);
    }

    public void Show(UIInventoryIcon active)
    {
        
        m_iconRef = active;
        m_active = true;
        var iconTrans = active.transform as RectTransform;
        var canvasRoot = GetComponentInParent<Canvas>();
        var pos = RectTransformUtility.CalculateRelativeRectTransformBounds(canvasRoot.transform, iconTrans).center;
        var trans = transform as RectTransform;
        trans.anchoredPosition = new Vector3(pos.x, trans.anchoredPosition.y, 0);
        m_combineBtn.interactable = false;
        gameObject.SetActive(true);
    }

    public void SetCombineTarget(UIInventoryIcon target)
    {
        m_combineBtn.interactable = target != null;
        m_combineTarget = target;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        m_active = false;
        m_iconRef = null;
    }

    private void OnCombineButtonClicked()
    {
        Debug.Log($"Combine {m_iconRef.ItemData.Name} and {m_combineTarget.ItemData.Name}");
    }

    private void OnUseButtonClicked()
    {
        Debug.Log($"Used {m_iconRef.ItemData.Name}");
    }
}
