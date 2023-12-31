using System;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // References
    private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera m_virtualCam;
    private CinemachineOrbitalTransposer m_orbital;
    private bool camEnabled = false;

    private float m_xAxisLastValue = 0f;

    [SerializeField] private float m_maxOrtho = 1f;
    [SerializeField] private float m_minOrtho = 0.4f;
    [SerializeField] private float m_zoomSpeed = 1f;
    [SerializeField] private float m_zoomSmoothTime = 0.2f;
    [SerializeField] private Transform m_aimTarget;

    [SerializeField] private Transform m_panTarget;
    private Vector3 m_panTargetLastPos;
    [SerializeField] private float m_panSpeed = 1.0f;

    private float m_currentZoomDistance;
    private float m_targetZoomDistance;
    private float m_zoomVelocity;

    bool HasTalkedToTenant = false;

    // Audio clips
    [SerializeField] private AudioClip clickSound;

    private void Awake()
    {
        m_orbital = m_virtualCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        m_xAxisLastValue = 45f;
    }

    [Button]
    void SetTarget(Transform target)
    {
        InteractionManager.Instance.SetInteractionTarget(target);
    }
    
    private void Start()
    {
        CinemachineCore.GetInputAxis = GetInputAxis;
        m_currentZoomDistance = m_virtualCam.m_Lens.OrthographicSize;
        m_targetZoomDistance = m_currentZoomDistance;
        InteractionManager.Instance.InteractionTargetChanged.AddListener(OnInteractTargetChanged);
        InteractionManager.Instance.SetInteractionTarget(m_virtualCam.m_Follow);
    }

    private void OnInteractTargetChanged(Transform target)
    {
        m_aimTarget.position = target.position;
        //m_aimTarget.position
    }

    private float GetInputAxis(string axis)
    {
        if (axis == "Mouse X")
        {
            if (Input.GetMouseButton(1)){
                return Input.GetAxis("Mouse X");
            }

            return 0;
        }

        if (axis == "Mouse Y")
        {
            if (Input.GetMouseButton(1)){
                return Input.GetAxis("Mouse Y");
            }

            return 0;
        }

        return Input.GetAxis(axis);
    }

    private void Update()
    {
        m_targetZoomDistance -= Input.GetAxis("Mouse ScrollWheel") * m_zoomSpeed;
        
        m_currentZoomDistance = Mathf.SmoothDamp(m_currentZoomDistance, m_targetZoomDistance, ref m_zoomVelocity, m_zoomSmoothTime);
        m_virtualCam.m_Lens.OrthographicSize = m_currentZoomDistance;
        
        m_targetZoomDistance =
            Mathf.Clamp(m_targetZoomDistance, m_minOrtho, m_maxOrtho);

        Ray alwaysRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButton(1))
        {
            //UIManager.Instance.Cursor.OverrideCursor(Cursors.Rotate);
        } else if (Input.GetMouseButtonUp(1))
        {
            //UIManager.Instance.Cursor.OverrideCursor(Cursors.Default);
        } else 
        if (Physics.Raycast(alwaysRay, out RaycastHit hit2))
        {
            if (hit2.collider.GetComponentInParent<Interactable>())
            {
                //UIManager.Instance.Cursor.OverrideCursor(Cursors.Speak);
            }
            else
            {
                //UIManager.Instance.Cursor.OverrideCursor(Cursors.Default);
            }
        }
        

        if(Input.GetMouseButtonDown(2))
        {
            m_panTargetLastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_virtualCam.LookAt = m_panTarget;
        }
        if(Input.GetMouseButton(2))
        {
            Vector3 mouseDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_panTargetLastPos;
            mouseDelta *= m_panSpeed;
            m_panTarget.Translate(mouseDelta.x, 0, mouseDelta.z);
            m_panTargetLastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var position = m_panTarget.position;
            position.x = Mathf.Clamp(position.x, -2.5f, 2.5f);
            position.z = Mathf.Clamp(position.z, -2.5f, 2.5f);
            m_panTarget.position = position;
        }

        else{
             m_virtualCam.LookAt = m_aimTarget;
             m_panTarget.position = m_aimTarget.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(UIManager.Instance.IsUIShowing())
            {
                UIManager.Instance.NextLine();
            }
            else{
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // Blocker here. Want Tenant to be first interaction on game start. Link this properly to game step event.
                    if(!HasTalkedToTenant)
                    {
                        var tenant = hit.collider.GetComponent<TenantInteraction>();
                        if(tenant)
                        {
                            HasTalkedToTenant = true;
                        }
                        else{
                            return;
                        }
                    }

                    if(InteractionManager.Instance.useButtonClicked)
                    {
                        var usable = hit.collider.GetComponentInParent<Usable>();
                        if(usable)
                        {
                            // Usable item. Trigger effect.
                            usable.UseItem(InteractionManager.Instance.clickedItemData);
                        }
                        else{
                            // Clicked item was not usable.
                        }
                        InteractionManager.Instance.useButtonClicked = false;
                    }
                    else if (hit.collider.GetComponentInParent<RoomTransition>())
                    {
                        hit.collider.GetComponentInParent<RoomTransition>().ChangeRoom();
                    }
                    else if (hit.collider.GetComponentInParent<Interactable>())
                    {
                        hit.collider.GetComponentInParent<Interactable>().Interact(gameObject.GetComponent<PlayerData>().itemData);
                        AudioManager.Instance.Play(clickSound);
                    }
                }
            } 
        }
    }
}
