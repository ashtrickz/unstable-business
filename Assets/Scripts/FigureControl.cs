using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FigureControl : MonoBehaviour
{

    private Vector3 mOffset;

    private Vector3 figureTransform;

    private float mZCoord;

    private bool isDragging = false;

    private bool isChecked = false;

    [SerializeField] private GameObject timerBar;

    [SerializeField] private PositionChecker _positionChecker;

    [SerializeField] private bool canMove = true;

    void OnMouseDown()
    {
        if (canMove && _positionChecker.canBeGrabbed)
        {
            isDragging = true;
            Debug.Log("Is Dragging");
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    void Update()
    {
        figureTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (isDragging)
        {
            if (figureTransform.y > 3)
                figureTransform.z = 12f;
            else figureTransform.z = 10f;
        }
        transform.position = figureTransform;
    }

    void OnMouseDrag()
    {
        if (canMove && _positionChecker.canBeGrabbed)
            transform.position = GetMouseWorldPos() + mOffset;
    }

    void OnMouseUp()
    {
        if (canMove && _positionChecker.canBeGrabbed)
            isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDragging && figureTransform.y > 3f)
            if (collision.gameObject.CompareTag("Figure"))
            {
                FigurePlaced(0);
            }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isDragging && figureTransform.y > 3f)
            if (collider.gameObject.CompareTag("Podium"))
                FigurePlaced(1);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Figure"))
        {
            Debug.Log("left collision");
            Destroy(transform.GetChild(0).gameObject);
        }
    }
    
    void FigurePlaced(int type)
    {
        if (!isChecked)
        {
            Debug.Log("Collision detected");
            canMove = false;
            figureTransform = transform.position;
            if (type == 0)
            {
                Instantiate(timerBar, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
                    Quaternion.identity).transform.SetParent(gameObject.transform);
                StartCoroutine(CheckPosition());
            }

            isChecked = true;
        }
    }
    
    IEnumerator CheckPosition()
    {
        _positionChecker.canBeGrabbed = false;
        yield return new WaitForSeconds(4f);
        _positionChecker.canBeGrabbed = true;
    }
    
}
