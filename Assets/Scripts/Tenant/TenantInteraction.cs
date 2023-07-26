using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class TenantInteraction : MonoBehaviour
{
    // World dialogue variables
    [SerializeField] private GameObject prefab;
    [SerializeField] private float dialogueTimer;
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private Vector3 dialoguePosition;
    private GameObject dialogueBox = null;
    private string dialogueText = "I'll be right here if you need anything";

    // References
    private Interactable interactableComponent;
    private DialogueSender dialogueSender;

    // Game steps
    [SerializeField] GameStepEvent initialTalkTenant;
    [SerializeField] GameStepEvent familyPhotoGameStep;

    // Dialogue data
    [SerializeField] DialogueData initialTalkData;

    // On awake
    private void Awake()
    {
        interactableComponent = GetComponent<Interactable>();
        interactableComponent.ItemInteraction.AddListener(OnInteract);

        dialogueSender = GetComponent<DialogueSender>();
    }

    private void OnInteract(ItemData itemData)
    {
        if (initialTalkTenant.CurrentState == GameStepEventState.NotStarted)
        {
            Debug.Log("Interacted and playing intial talk dialogue");
            dialogueSender.DeliverDialogue(initialTalkData);
            CompleteGameStep(initialTalkTenant);
        }
        else if (familyPhotoGameStep.CurrentState == GameStepEventState.Completed)
        {
            dialogueText = "Please be careful with that photo";
            WorldDialogue();
        }
        else
        {
            WorldDialogue();
        }
    }

    private void WorldDialogue()
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
