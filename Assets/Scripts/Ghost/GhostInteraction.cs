using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GhostInteraction : MonoBehaviour
{
    // World dialogue variables
    [SerializeField] private GameObject prefab;
    [SerializeField] private float dialogueTimer;
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private Vector3 dialoguePosition;
    private GameObject dialogueBox = null;

    // Cutscene dialogue
    [SerializeField] private List<string> dadDialogue;
    [SerializeField] private List<string> familyDialogue;
    [SerializeField] private List<string> missedBirthdayDialogue;
    [SerializeField] private List<string> birthdayPartyDialogue;

    // In world dialogue
    [SerializeField] private string kitchenDialogue;
    [SerializeField] private string bedroomDialogue;
    [SerializeField] private string partyDialogue;

    // First time checks
    bool familyDialogueHasPlayed = false;
    bool missedBirthdayDialogueHasPlayed = false;

    // References
    private Interactable interactableComponent;

    // Game steps
    [SerializeField] GameStepEvent unlockDoorGameStep;
    [SerializeField] GameStepEvent entertainEmmaGameStep;

    // On awake
    private void Awake()
    {
        interactableComponent = GetComponent<Interactable>();
        interactableComponent.ItemInteraction.AddListener(OnInteract);
    }

    private void OnInteract(ItemData itemData)
    {
        if (entertainEmmaGameStep.CurrentState == GameStepEventState.Completed && !familyDialogueHasPlayed)
        {
            WorldDialogue(dadDialogue[0]);
            familyDialogueHasPlayed = true;
            CompleteGameStep(unlockDoorGameStep);
        }
        else
        {
            WorldDialogue(kitchenDialogue);
        }
    }

    private void WorldDialogue(string dialogueText)
    {
        if (dialogueBox == null)
        {
            // Instantiate dialogue box and get reference to text component
            dialogueBox = Instantiate(prefab);
            dialogueBox.transform.SetParent(UICanvas.transform, false);
            dialogueBox.transform.position = dialoguePosition;
            TMP_Text textBox = dialogueBox.GetComponentInChildren<TMP_Text>();
            textBox.text = dialogueText;

            StartCoroutine(TimerForDestroy());
        }
    }

    private IEnumerator TimerForDestroy()
    {
        yield return new WaitForSeconds(dialogueTimer);

        Destroy(dialogueBox);
    }

    public void CompleteGameStep(GameStepEvent currentGameStep)
    {
        currentGameStep.ChangeContext(GameStepEventState.Completed);
    }
}
