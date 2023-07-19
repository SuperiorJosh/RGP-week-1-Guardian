using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // References
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.GetComponent<RoomTransition>())
                {
                    hit.collider.GetComponent<RoomTransition>().ChangeRoom();
                }
                else if (hit.collider.GetComponent<Interactable>())
                {
                    hit.collider.GetComponent<Interactable>().Interact(gameObject.GetComponent<PlayerData>().itemData);
                }
            }
        }
    }
}
