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

    private float m_currentZoomDistance;
    private float m_targetZoomDistance;
    private float m_zoomVelocity;

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

        if (Input.GetMouseButtonDown(0))
        {
            if(DialogueManager.Instance.IsUIShowing())
            {
                DialogueManager.Instance.NextLine();
            }
            else{
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
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
                    else if (hit.collider.GetComponent<RoomTransition>())
                    {
                        hit.collider.GetComponent<RoomTransition>().ChangeRoom();
                    }
                    else if (hit.collider.GetComponentInParent<Interactable>())
                    {
                        hit.collider.GetComponentInParent<Interactable>().Interact(gameObject.GetComponent<PlayerData>().itemData);
                    }
                }
            } 
        }
    }
}
