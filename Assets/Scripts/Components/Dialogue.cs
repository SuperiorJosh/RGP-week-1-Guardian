using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    // Variables
    [SerializeField] private string dialogueText;
    [SerializeField] private GameObject prefab;

    // References
    private Interactable interactableComponent;

    private void Awake()
    {
        interactableComponent = GetComponent<Interactable>();
        interactableComponent.ItemInteraction.AddListener(OnInteract);
    }

    private void OnInteract(ItemData _heldItemData)
    {
        // Instantiate dialogue box and get reference to text component
        GameObject dialogueBox = Instantiate(prefab);
        TMP_Text textBox = dialogueBox.GetComponentInChildren<TMP_Text>();

        textBox.text = dialogueText;
    }
}
