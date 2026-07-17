using System;
using UnityEngine;

public class CameraFollowSettings : MonoBehaviour
{
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] Transform square;
    [SerializeField] Transform square2;
    [SerializeField] float setZoom;
    [SerializeField] float setZoom_speed;
    [SerializeField] float goToAnotherObj_speed;

    public void SetZoom()
    {
        cameraFollow.CallSetZoom(setZoom, setZoom_speed);
    }
    public void SetPos()
    {
        cameraFollow.SetPosition(square2);
    }
    public void GoToAnotherObject()
    {
        cameraFollow.CallGoToAnotherObject(square, goToAnotherObj_speed);
    }
}
