using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Camera cam;
    [SerializeField] float zoomValue;
    [SerializeField] float maxZoom;
    [SerializeField] float minZoom;

    bool FollowPlayer = true;

    private void Start()
    {
        cam = Camera.main;
        cam.transform.SetParent(transform);
    }

    private void FixedUpdate()
    {
        if(FollowPlayer){ transform.position = Vector2.Lerp(transform.position, player.position, 0.1f); }
    }

    public void StopFollowPlayer()
    {
        FollowPlayer = false;
    }

    public void StartFollowPlayer()
    {
        FollowPlayer = true;
    }

    public void CallGoToAnotherObject(Transform obj, float speed)
    {
        StartCoroutine(GoToAnotherObject(obj, speed));
    }

    public void SetPosition(Transform pos)
    {
        transform.position = pos.position;
    }
    
    public void CallSetZoom(float zoom, float speed)
    {
        StartCoroutine(SetZoom(zoom, speed));
    }
    

    public IEnumerator SetZoom(float zoom, float speed)
    {
        float elapsedTime = 0f;
        float startValue = cam.orthographicSize;
        while (elapsedTime < speed)
        {
            elapsedTime += Time.deltaTime;

            cam.orthographicSize = Mathf.Lerp(startValue, zoom, elapsedTime / speed);

            yield return null;
        }

        StopCoroutine("SetZoom");
    }
    public IEnumerator GoToAnotherObject(Transform target, float speed)
    {
        float elapsedTime = 0f;
        Transform startValue = transform;
        while (elapsedTime < speed)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector2.Lerp(startValue.position, target.position, elapsedTime / speed);

            yield return null;
        }

        StopCoroutine("SetZoom");
    }

    private void Update()
    {
        /*
        zoom(Input.GetAxis("Mouse ScrollWheel") * zoomValue * Time.deltaTime);
        */

        if (cam.orthographicSize > maxZoom) { cam.orthographicSize = maxZoom; }
        if (cam.orthographicSize < minZoom) { cam.orthographicSize = minZoom; }
    }

    void zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, minZoom, maxZoom);
    }
}
