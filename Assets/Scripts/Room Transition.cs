using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    // Variables
    [SerializeField] private float shiftInX;
    [SerializeField] GameStepEvent gameStep;

    // References
    [SerializeField] private GameObject rooms;

    // Audio clips
    [SerializeField] AudioClip doorAudio;

    // Change Rooms
    public void ChangeRoom()
    {
        // Check game state here, is it valid to swap rooms yet?
        if(gameStep.CurrentState == GameStepEventState.Completed)
        {
            Vector3 currentPosition = rooms.transform.position;
            rooms.transform.position = new Vector3(currentPosition.x + shiftInX, currentPosition.y, currentPosition.z);

            AudioManager.Instance.Play(doorAudio);
        }
    }
}