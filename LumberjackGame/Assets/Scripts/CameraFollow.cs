using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Camera cam;
    [SerializeField] float zoomValue;
    [SerializeField] float maxZoom;
    [SerializeField] float minZoom;

    private void Start()
    {
        cam = Camera.main;
        cam.transform.SetParent(transform);
    }

    private void Update()
    {
        zoom(Input.GetAxis("Mouse ScrollWheel") * zoomValue * Time.deltaTime);

        if (cam.orthographicSize > maxZoom) { cam.orthographicSize = maxZoom; }
        if (cam.orthographicSize < minZoom) { cam.orthographicSize = minZoom; }
    }

    void zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, minZoom, maxZoom);
    }
}
