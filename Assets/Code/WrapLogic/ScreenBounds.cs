using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    public Camera mainCamera;
    BoxCollider2D boxCollider;
    [SerializeField]BoundsWrapper boundsWrapper;

    [SerializeField]
    private float teleportOffset = 0.2f;

    [SerializeField]
    private float cornerOffset = 1;

    private void Awake()
    {
        this.mainCamera.transform.localScale = Vector3.one;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
    }

    private void UpdateBoundsSize()
    {
        float ySize = mainCamera.orthographicSize * 2;
        Vector2 boxColliderSize = new Vector2(ySize * mainCamera.aspect, ySize);
        boxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        boundsWrapper.ColliderForWrapping(collision);
    }

    public bool AmIOutOfBounds(Vector3 worldPosition)
    {
        if (Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x) ||
             Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider.bounds.min.y))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult = 
            Mathf.Abs(worldPosition.x) > (Mathf.Abs(boxCollider.bounds.min.x) - cornerOffset);
        bool yBoundResult = 
            Mathf.Abs(worldPosition.y) > (Mathf.Abs(boxCollider.bounds.min.y) - cornerOffset);

        Vector2 signWorldPosition = 
            new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (xBoundResult && yBoundResult)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1) 
                + Vector2.Scale(new Vector2(teleportOffset, teleportOffset), 
                signWorldPosition);
        }
        else if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y) 
                + new Vector2(teleportOffset * signWorldPosition.x, teleportOffset);
        }
        else if (yBoundResult)
        {
            return new Vector2(worldPosition.x, worldPosition.y * -1) 
                + new Vector2(teleportOffset, teleportOffset * signWorldPosition.y);
        }
        else
        {
            return worldPosition;
        }
    }

}
