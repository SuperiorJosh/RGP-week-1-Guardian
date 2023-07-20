using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    // Variables
    [SerializeField] private string dialogueText;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float dialogueTimer;
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private Vector3 dialoguePosition;
    private GameObject dialogueBox = null;

    // References
    private Interactable interactableComponent;

    private void Awake()
    {
        interactableComponent = GetComponent<Interactable>();
        interactableComponent.ItemInteraction.AddListener(OnInteract);
    }

    private void OnInteract(ItemData _heldItemData)
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
}
