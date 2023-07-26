using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour
{
    // References
    [SerializeField] private GameObject tenant;
    private TenantInteraction tenantInteraction;

    private void Awake()
    {
        tenantInteraction = tenant.GetComponent<TenantInteraction>();
        tenantInteraction.activateGhostVision.AddListener(ShowButton);

        gameObject.SetActive(false);
    }

    private void ShowButton()
    {
        gameObject.SetActive(true);
    }
}
