using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject  m_object;
    [SerializeField]
    private bool        m_isActive;
    [SerializeField]
    private float       m_dragIntervalSec;

    private bool        m_startDrag;
    private bool        m_isDragging;
    private Vector3     m_objectWorldOriginalPosition;
    private Vector3     m_objectLocalOriginalPosition;
    private Vector3     m_mouseStartPosition;
    private GameObject  m_lastObjectHover;
    private float       m_dragTimeStartSec;

    public GameObject   Object => m_object;
    public bool         IsDragging => m_isDragging;
    public bool         IsActive => m_isActive;

    private void Awake()
    {
        m_lastObjectHover = null;
        m_dragTimeStartSec = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_objectWorldOriginalPosition = m_object.transform.position;
        m_objectLocalOriginalPosition = m_object.transform.localPosition;

        m_startDrag = false;
        m_isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        m_mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_startDrag = true;
    }

    private void OnMouseDrag()
    {
        if (m_isActive && m_startDrag)
        {
            print("drag m_dragTimeStartMs: " + m_dragTimeStartSec + " with interval: " + m_dragIntervalSec);
            if (m_dragTimeStartSec < m_dragIntervalSec)
            {
                m_dragTimeStartSec += Time.deltaTime;
            }
            else
            {
                Vector3 movement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_mouseStartPosition;

                m_object.transform.position = new Vector3(movement.x + m_objectWorldOriginalPosition.x, movement.y + m_objectWorldOriginalPosition.y, m_objectWorldOriginalPosition.z);
                m_isDragging = true;
            }
        }
    }

    private void OnMouseUp()
    {
        if (m_lastObjectHover)
        {
            m_object.transform.SetParent(m_lastObjectHover.transform);
        }

        ResetLocalPosition();
        UpdateWorldPosition();

        Cancel();
    }

    public void SetDragDropActive(bool _active)
    {
        m_isActive = _active;
    }

    public void Cancel()
    {
        m_startDrag = false;
        m_isDragging = false;
        m_dragTimeStartSec = 0;
    }

    public void SetObjectHover(GameObject _object)
    {
        m_lastObjectHover = _object;
    }

    public void ResetLocalPosition()
    {
        m_object.transform.localPosition = m_objectLocalOriginalPosition;
    }

    public void UpdateWorldPosition()
    {
        m_objectWorldOriginalPosition = m_object.transform.position;
    }
}
