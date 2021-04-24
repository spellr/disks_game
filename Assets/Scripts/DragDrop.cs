using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private float distance;
    private Rigidbody m_rigidbody;
    private bool dragging = false;

    private void Start() {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPointerDown(PointerEventData pointerEventData) {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        m_rigidbody.useGravity = false;
        dragging = true;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        Vector3 move = new Vector3(rayPoint.x, Mathf.Max(rayPoint.y, 0.35f), transform.position.z);
        m_rigidbody.position = move;
        m_rigidbody.velocity = new Vector3(0,0,0);
    }

    public void Update() {
        if (dragging)
            m_rigidbody.velocity = new Vector3();
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        m_rigidbody.useGravity = true;
        dragging = false;
    }
}
