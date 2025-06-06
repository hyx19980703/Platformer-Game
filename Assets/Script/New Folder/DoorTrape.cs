using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DoorTrape : MonoBehaviour
{
    private BoxCollider2D collider2D;
    [SerializeField] private float doorHight;
    [SerializeField] private float maxHight;
    [SerializeField] private float minHight;

    [SerializeField] private float closingSpeed;
    [SerializeField] Transform doorPosition;
    [SerializeField] private DoorTrriger doorTrriger;
    private bool isClosing;
    private float targetDoorHight;
    void Start()
    {
        doorTrriger = GetComponentInChildren<DoorTrriger>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        targetDoorHight = doorTrriger.isClosing ? minHight : maxHight;

        doorHight = Mathf.Lerp(collider2D.size.y, targetDoorHight, closingSpeed * Time.deltaTime);

        DoorColliderHight(doorHight);
    }

    private void DoorColliderHight(float doorHight)
    {
        collider2D.size = new Vector2(collider2D.size.x, doorHight);

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(doorPosition.position, doorPosition.position + Vector3.down * maxHight);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(doorPosition.position, doorPosition.position + Vector3.down* minHight);
    }
}
