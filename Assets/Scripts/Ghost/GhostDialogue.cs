using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDialogue : MonoBehaviour
{
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

    // Reference to player data
}
