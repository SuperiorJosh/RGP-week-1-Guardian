using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GhostInteraction : MonoBehaviour
{
    // References
    private Interactable interactableComponent;
    private DialogueSender dialogueSender;
    private SpriteRenderer spriteRenderer;
    private TenantInteraction tenantInteraction;
    [SerializeField] private GameObject tenant;

    // Game steps
    [SerializeField] GameStepEvent initialTalkGhostGameStep;
    [SerializeField] GameStepEvent MugInteractionGameStep;

    // Dialogue data
    [SerializeField] DialogueData initialTalkGhostDialogue;
    [SerializeField] DialogueData MugInteractionDialogue;

    // Bedroom position for Emma to flee to
    [SerializeField] Vector3 BedroomPosition;

    // On awake
    private void Awake()
    {
        interactableComponent = GetComponent<Interactable>();
        interactableComponent.ItemInteraction.AddListener(OnInteract);

        dialogueSender = GetComponent<DialogueSender>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        tenantInteraction = tenant.GetComponent<TenantInteraction>();
    }

    private void Start()
    {
        MugInteractionGameStep.StepEventChanged.AddListener(FleeToBedroom);
    }

    private void OnInteract(ItemData itemData)
    {
        if (initialTalkGhostGameStep.CurrentState == GameStepEventState.NotStarted)
        {
            dialogueSender.DeliverDialogue(initialTalkGhostDialogue);
            CompleteGameStep(initialTalkGhostGameStep);
            GhostMovement(-7f, 1f);
            tenantInteraction.TenantMovement(-5f, 1f);
        }
    }

    public void CompleteGameStep(GameStepEvent currentGameStep)
    {
        currentGameStep.ChangeContext(GameStepEventState.Completed);
    }

    public void GhostMovement(float _xPosition, float _zPosition)
    {
        spriteRenderer.DOFade(0f, 1f).OnComplete(() => { spriteRenderer.DOFade(1f, 1f); });
        gameObject.transform.position = new Vector3(_xPosition, 1f, _zPosition);
    }

    private void FleeToBedroom(GameStepEvent _stepEvent)
    {
        if(MugInteractionGameStep.CurrentState != GameStepEventState.Completed) return;

        // Move emma to bedroom.
        transform.position = BedroomPosition;
    }
}
