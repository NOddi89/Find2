using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool invertXAxis = true;
    public bool invertYAxis = true;
    public float zoomSpeed = 0.5f;
    public float maxZoomIn = 5.0f;
    public float maxZoomOut = 20.0f;
    public float rotationSpeed = 20.0f; 

    private bool m_moveCamera = false;
    private bool m_rotateCamera = false;
    private Vector3 m_rotatePoint;
    private Vector3 m_initialOffset;
    private Vector3 m_currentOffset;

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update ()
    {
        // Move camera
        if (Input.GetMouseButtonDown(0))
        {
            m_moveCamera = true;
        }

        if(Input.GetMouseButtonUp(0) && m_moveCamera)
        {
            m_moveCamera = false;
        }

        if(m_moveCamera)
        {
            float y = Input.GetAxis("Mouse Y");
            float x = Input.GetAxis("Mouse X");

            y = (invertYAxis ? (y * 1) : (y * (-1)));
            x = (invertXAxis ? (x * 1) : (x * (-1)));
            transform.Translate(-x, -y, 0);
        }

        // Zoom
        if(Input.GetAxis("Mouse ScrollWheel") > 0) // in
        {
            if (Camera.main.orthographicSize > maxZoomIn)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize - zoomSpeed;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0) // out
        {
            if (Camera.main.orthographicSize < maxZoomOut)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize + zoomSpeed;
            }
        }

        // Rotation
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 center = new Vector3((Screen.width / 2), (Screen.height / 2), 0);

            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(center);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                m_rotatePoint = objectHit.position + (new Vector3(-1.5f, 0f, -1.5f));               
            }

            m_initialOffset = transform.position - m_rotatePoint;
            m_currentOffset = m_initialOffset;

            m_rotateCamera = true;
        }

        if (Input.GetMouseButtonUp(1) && m_rotateCamera)
        {
            m_rotateCamera = false;
        }

        if(m_rotateCamera)
        {
            transform.position = m_rotatePoint + m_currentOffset;
            
            float movement = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            if(!Mathf.Approximately(movement, 0f))
            {
                transform.RotateAround(m_rotatePoint, Vector3.up, movement);
                m_currentOffset = transform.position - m_rotatePoint;
            }
        }

    }

}
