using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clothes : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string borderTagName = "";
    public string uiLayer = "Name of UI layer here";
    public float throwForce = 100f;
    public Rigidbody2D rb;
    public float gravityScale = 10f;
    public Image spriteIcon;
    public Sprite pileIcon;
    public Sprite dragIcon;

    bool canDrag = true;
    Vector3 offset;
    Vector3 mouseDelta = Vector3.zero;
    Vector3 lastMousePos = Vector3.zero;

    // Clothes functions
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.layer = LayerMask.NameToLayer(uiLayer);
        offset = transform.position - Input.mousePosition; // Gets position relative to where mouse clicked
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        spriteIcon.sprite = dragIcon;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            mouseDelta = Input.mousePosition - lastMousePos;
            lastMousePos = Input.mousePosition;

            transform.position = Input.mousePosition + offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rb.AddForceAtPosition(mouseDelta * throwForce, lastMousePos, ForceMode2D.Force);
        rb.AddTorque(mouseDelta.magnitude * mouseDelta.normalized.x * throwForce, ForceMode2D.Force);
        rb.gravityScale = gravityScale;
        spriteIcon.sprite = pileIcon;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(borderTagName))
        {
            canDrag = false;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(borderTagName))
        {
            canDrag = true;
        }
    }
}
