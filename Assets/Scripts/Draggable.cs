using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public Vector3 startPos;
    private bool snapped;
    private int currentSortOrder;
    private int currentPipeSortOrder;
    public SpriteRenderer currentSpriteRenderer;
    public SpriteRenderer pipeSpriteRenderer;
    private Transform currentNearbyTile;
    private void Start()
    {
        currentSortOrder = currentSpriteRenderer.sortingOrder;
        if(pipeSpriteRenderer != null)
        {
            currentPipeSortOrder = pipeSpriteRenderer.sortingOrder;
        }
       
        startPos = transform.position;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }

        if (isDragging)
        {
            OnMouseDrag();
        }
    }

    private void OnMouseDown()
    {

        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 objectPosition = transform.position;

        // Calculate the offset between the mouse position and the sprite position
        offset = objectPosition - mousePosition;
        if (offset.magnitude < 0.5f)
        {
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
       // Debug.Log("Dragging");
        // Move the sprite to the current mouse position with the offset
        transform.position = GetMouseWorldPosition() + offset;
        SnapToOthers();
       // Debug.Log("Set sort order for : " + transform.name + " " + 4);
        currentSpriteRenderer.sortingOrder = 4;
        if(pipeSpriteRenderer != null)
        {
            pipeSpriteRenderer.sortingOrder = 5;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        currentSpriteRenderer.sortingOrder = currentSortOrder;
        if (pipeSpriteRenderer != null)
        {
            pipeSpriteRenderer.sortingOrder = currentPipeSortOrder;
        }
        
        if(snapped == false)
        {
            transform.position = startPos;
        }
        else
        {
            transform.position = currentNearbyTile.position;
        }
        snapped = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void SnapToOthers()
    {
        Draggable[] otherSprites = FindObjectsOfType<Draggable>();
        List<Draggable> others = new List<Draggable>();
        foreach (Draggable oj in otherSprites)
        {
            if (oj.gameObject != gameObject)
            {
                others.Add(oj);
            }
        }
        snapped = false;
        foreach (Draggable sprite in others)
        {
            Draggable otherSprite = sprite;
            if (Vector3.Distance(transform.position, otherSprite.transform.position) < 0.2f)
            {
                currentNearbyTile = otherSprite.transform;
                transform.position = otherSprite.transform.position;
                //Debug.Log("Snapping " + transform.name + "To " + otherSprite.transform.position);
                otherSprite.transform.position = startPos;
               // Debug.Log("Snapping " + otherSprite.transform.name + "To " + startPos);
                startPos = transform.position;
                otherSprite.startPos = otherSprite.transform.position;
               // isDragging = false;
                snapped = true;
            }
        }

    }

}
