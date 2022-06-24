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
    private Vector3 freezedTransform;

    private float mZCoord;

    private int innerType;

    private bool isDragging = false;
    private bool isChecked = false;
    private bool startPositionCheck = false;
    private bool pointsLost = false;
    private bool canMove = true;

    [SerializeField] private GameObject timerBar;
    [SerializeField] private GameObject hitParticles;
    
    [SerializeField] private PositionChecker _positionChecker;
    [SerializeField] private PointsSystem _pointsSystem;
    [SerializeField] private StateColorChanger _stateColorChanger;
    [SerializeField] private CinemachineShake _cinemachineShake;
    [SerializeField] private Material particlesMaterial;
    
    void OnMouseDown()
    {
        if (canMove && _positionChecker.canBeGrabbed)
        {
            isDragging = true; //Debug.Log("Is Grabbed");
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            _stateColorChanger.ChangeStateColor(1);
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
        if (startPositionCheck)
        {
            if (transform.position != freezedTransform && innerType != 1)
            {
                Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); //Debug.Log("position changed");
                if (currentPosition.y < freezedTransform.y && !pointsLost)
                {
                    Debug.Log("points loss");
                    _pointsSystem.LosePoints();
                    pointsLost = true;
                    PointsChecker();
                }
            }
        }
    }
    
    void OnMouseDrag()
    {
        //Debug.Log("Is Dragging");
        if (canMove && _positionChecker.canBeGrabbed)
            transform.position = GetMouseWorldPos() + mOffset;
    }

    void OnMouseUp()
    {
        if (canMove && _positionChecker.canBeGrabbed)
            isDragging = false;
        if (!isChecked)
            _stateColorChanger.ChangeStateColor(0);
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
                particlesMaterial.color = _stateColorChanger.previousColor;
                Instantiate(hitParticles, collision.transform.position, Quaternion.identity);
            }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isDragging && figureTransform.y > 3f)
            if (collider.gameObject.CompareTag("Podium"))
            {
                if(_positionChecker.freePodium)
                    FigurePlaced(1);
                else
                {
                    _pointsSystem.LosePoints();
                    _stateColorChanger.ChangeStateColor(1);
                }
                particlesMaterial.color = _stateColorChanger.previousColor;
                Instantiate(hitParticles, collider.transform.position, Quaternion.identity);
                _cinemachineShake.ShakeCamera(1f,0.3f);
            }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Figure"))
            Destroy(transform.GetChild(0).gameObject); //Debug.Log("left collision");
    }
    
    void FigurePlaced(int type)
    {
        if (!isChecked) //Debug.Log("Collision detected");
        {
            canMove = false;
            _stateColorChanger.ChangeStateColor(2);
            figureTransform = transform.position;
            if (type == 0)
            {
                _cinemachineShake.ShakeCamera(1f,0.3f);
                Instantiate(timerBar, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
                    Quaternion.identity).transform.SetParent(gameObject.transform);
                StartCoroutine(CheckPosition());
            }
            else if(_positionChecker.freePodium)
            {
                StartCoroutine(FreezePause());
                _positionChecker.freePodium = false;
            }
            innerType = type;
        }
    }

    public void FreezeFigure()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        freezedTransform = transform.position;
        startPositionCheck = true;
        if (!isChecked)
            _pointsSystem.GetPoints();
        isChecked = true;
        _stateColorChanger.ChangeStateColor(3);
    }
    
    IEnumerator CheckPosition()
    {
        _positionChecker.canBeGrabbed = false;
        yield return new WaitForSeconds(3f);
        _positionChecker.canBeGrabbed = true;
    }

    IEnumerator FreezePause()
    {
        yield return new WaitForSeconds(1f);
        FreezeFigure();
    }
    
    void PointsChecker()
    {
        if (transform.position.y >= freezedTransform.y - 0.1f)
            _pointsSystem.GetPoints();
    }
    
}
