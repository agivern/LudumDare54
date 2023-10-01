using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Alien))]
public class AlienDragDrop : MonoBehaviour
{
    [SerializeField] private LayerMask droppableLayersToCheck;
    public static AlienDragDrop CurrentlyDragging { get; private set; }


    private bool isDragging = false;

    private Alien alien;

    private void Awake()
    {
        alien = GetComponent<Alien>();
    }

    private void Update()
    {
        // On mouse down, start dragging
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                if (CurrentlyDragging == null)
                {
                    isDragging = true;
                    CurrentlyDragging = this;
                }
            }
        }

        // While dragging, move with mouse
        if (isDragging)
        {
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            transform.position = mousePos + (Vector3.down * 0.2f);
        }

        // On mouse release, stop dragging and check drop
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            CurrentlyDragging = null;

            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)),
                Vector2.zero,
                Mathf.Infinity, droppableLayersToCheck);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Room"))
            {
                var room = hit.collider.gameObject.GetComponent<Room>();
                alien.MoveToRoom(room);
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Lobby"))
            {
                alien.MoveToLobby();
            }
            else
            {
                //Alien is in space
                alien.MoveToSpace();
            }
        }
    }
}