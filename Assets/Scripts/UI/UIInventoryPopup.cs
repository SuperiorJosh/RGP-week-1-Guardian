using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInventoryPopup : MonoBehaviour
{
    [SerializeField] private Button m_useBtn;
    [SerializeField] private Button m_combineBtn;
    private UIInventoryIcon m_activeIcon;
    private UIInventoryIcon m_combineIcon;
    private bool m_active = false;
    public bool Active => m_active;

    private UnityAction<UIInventoryIcon> m_onIconClickCallback;
    
    private void Start()
    {
        m_useBtn.onClick.AddListener(OnUseButtonClicked);
        m_combineBtn.onClick.AddListener(OnCombineButtonClicked);
        
        gameObject.SetActive(false);
    }

    public void Show(UIInventoryIcon icon, UnityAction<UIInventoryIcon> onIconClicked)
    {
        if (!m_active)
        {
            m_onIconClickCallback = onIconClicked;
            m_activeIcon = icon;
            m_active = true;
            m_combineBtn.interactable = false;
            gameObject.SetActive(m_activeIcon);
            UpdatePosition(m_activeIcon);
        }
    }

    public void UpdatePosition(UIInventoryIcon target)
    {
        var iconTrans = target.transform as RectTransform;
        var canvasRoot = GetComponentInParent<Canvas>();
        var pos = RectTransformUtility.CalculateRelativeRectTransformBounds(canvasRoot.transform, iconTrans).center;
        var trans = transform as RectTransform;
        trans.anchoredPosition = new Vector3(pos.x, trans.anchoredPosition.y, 0);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
        m_active = false;
        m_activeIcon = null;
        m_combineIcon = null;
    }

    private void OnCombineButtonClicked()
    {
        var itemOne = m_activeIcon.ItemData;
        var itemTwo = m_combineIcon.ItemData;
        var combined = Inventory.Instance.CombineItems(itemOne, itemTwo);
        if (combined == null) return;
        Debug.Log($"Combined {itemOne.Name} and {itemTwo.Name}");
        m_activeIcon.Setup(combined.Output);
        m_activeIcon.OnClicked.AddListener(m_onIconClickCallback);
        Hide();
        DialogueManager.Instance.ProcessDialogue(combined.dialogueData);
    }

    private void OnUseButtonClicked()
    {
        Debug.Log($"Used {m_activeIcon.ItemData.Name}");

        InteractionManager.Instance.useButtonClicked = true;
        //InteractionManager.Instance.clickedItemData = m_activeIcon.ItemData;
    }

    public void SetNewTarget(UIInventoryIcon icon)
    {
        // already shown, if active icon selected
        if (icon == m_activeIcon)
        {
            // if combine icon, replace combine icon with active
            if (m_combineIcon)
            {
                m_activeIcon = m_combineIcon;
                m_combineIcon = null;
                m_combineBtn.interactable = false;
                UpdatePosition(m_activeIcon);
                return;
            }
            m_activeIcon = null;
            Hide();
            return;
        }

        m_combineIcon = icon == m_combineIcon ? null : icon;
        m_combineBtn.interactable = m_combineIcon != null;
    }
}
