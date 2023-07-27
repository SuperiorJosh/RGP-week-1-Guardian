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

    // Game steps
    [SerializeField] GameStepEvent initialTalkGhostGameStep;

    // Dialogue data
    [SerializeField] DialogueData initialTalkGhostDialogue;

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
        dialogueSender.DeliverDialogue(initialTalkGhostDialogue);
        CompleteGameStep(initialTalkGhostGameStep);
    }

    public void CompleteGameStep(GameStepEvent currentGameStep)
    {
        currentGameStep.ChangeContext(GameStepEventState.Completed);
    }

    private void GhostMovement(Vector3 _position)
    {
        spriteRenderer.DOFade(0f, 1f);
        gameObject.transform.position = _position;
        spriteRenderer.DOFade(0.2f, 1f);
    }
}
