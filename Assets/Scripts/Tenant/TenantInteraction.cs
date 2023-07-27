using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class TenantInteraction : MonoBehaviour
{
    // References
    private Interactable interactableComponent;
    private DialogueSender dialogueSender;
    private SpriteRenderer spriteRenderer;

    // Game steps
    [SerializeField] GameStepEvent initialTalkTenantGameStep;
    [SerializeField] GameStepEvent familyPhotoFixedGameStep;
    [SerializeField] GameStepEvent intialTalkGhostGameStep;
    [SerializeField] private GameStepEvent m_ghostVisionAvailableGameStep;

    // Dialogue data
    [SerializeField] DialogueData initialTalkDialogue;
    [SerializeField] DialogueData afterInitialTalkDialogue;
    [SerializeField] DialogueData afterPhotoFixedDialogue;
    [SerializeField] DialogueData afterGhostTalkDialogue;

    // Unity Event
    public UnityEvent activateGhostVision;

    // On awake
    private void Awake()
    {
        interactableComponent = GetComponent<Interactable>();
        interactableComponent.ItemInteraction.AddListener(OnInteract);

        dialogueSender = GetComponent<DialogueSender>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnInteract(ItemData itemData)
    {
        if (initialTalkTenantGameStep.CurrentState == GameStepEventState.NotStarted)
        {
            dialogueSender.DeliverDialogue(initialTalkDialogue);
            CompleteGameStep(initialTalkTenantGameStep);
        }
        else if (intialTalkGhostGameStep.CurrentState == GameStepEventState.Completed)
        {
            dialogueSender.DeliverDialogue(afterGhostTalkDialogue);
        }
        else if (familyPhotoFixedGameStep.CurrentState == GameStepEventState.Completed)
        {
            dialogueSender.DeliverDialogue(afterPhotoFixedDialogue);
        }
        else
        {
            dialogueSender.DeliverDialogue(afterInitialTalkDialogue);
        }
    }

    public void CompleteGameStep(GameStepEvent _currentGameStep)
    {
        _currentGameStep.ChangeContext(GameStepEventState.Completed);
    }

    public void ActivateGhostVision()
    {
        //activateGhostVision?.Invoke();
        
        m_ghostVisionAvailableGameStep.ChangeContext(GameStepEventState.Completed);
    }

    public void TenantMovement(float _xPosition, float _zPosition)
    {
        spriteRenderer.DOFade(0f, 1f).OnComplete(() => { spriteRenderer.DOFade(1f, 1f); });
        gameObject.transform.position = new Vector3(_xPosition, 1f, _zPosition);
    }
}
