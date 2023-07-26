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

    // Game steps
    [SerializeField] GameStepEvent initialTalkTenantGameStep;
    [SerializeField] GameStepEvent familyPhotoFixedGameStep;

    // Dialogue data
    [SerializeField] DialogueData initialTalkDialogue;
    [SerializeField] DialogueData afterInitialTalkDialogue;
    [SerializeField] DialogueData afterPhotoFixedDialogue;

    // Unity Event
    public UnityEvent activateGhostVision;

    // On awake
    private void Awake()
    {
        interactableComponent = GetComponent<Interactable>();
        interactableComponent.ItemInteraction.AddListener(OnInteract);

        dialogueSender = GetComponent<DialogueSender>();
    }

    private void OnInteract(ItemData itemData)
    {
        if (initialTalkTenantGameStep.CurrentState == GameStepEventState.NotStarted)
        {
            dialogueSender.DeliverDialogue(initialTalkDialogue);
            CompleteGameStep(initialTalkTenantGameStep);
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

    public void CompleteGameStep(GameStepEvent currentGameStep)
    {
        currentGameStep.ChangeContext(GameStepEventState.Completed);
    }

    public void ActivateGhostVision()
    {
        activateGhostVision?.Invoke();
    }
}
