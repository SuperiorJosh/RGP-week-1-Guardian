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
    }

    private void OnInteract(ItemData itemData)
    {
    }

    public void CompleteGameStep(GameStepEvent currentGameStep)
    {
        currentGameStep.ChangeContext(GameStepEventState.Completed);
    }
}
