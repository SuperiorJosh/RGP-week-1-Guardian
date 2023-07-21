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
                if(InteractionManager.Instance.itemWasClicked)
                {
                    if(hit.collider.GetComponent<Usable>())
                    {
                        // Usable item. Trigger effect.
                        hit.collider.GetComponent<Usable>().UseItem(InteractionManager.Instance.clickedItemData);
                    }
                    else{
                        // Clicked item was not usable.
                    }
                    InteractionManager.Instance.itemWasClicked = false;
                }
                else if (hit.collider.GetComponent<RoomTransition>())
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
