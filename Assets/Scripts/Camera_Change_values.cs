using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Camera_Change_values : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera vcam;
    private CinemachineComposer vcam_comp;

    void Start()
    {
        vcam = vcam.GetComponent<CinemachineVirtualCamera>();
        vcam_comp = vcam.GetCinemachineComponent<CinemachineComposer>(); 
    }

    //void OnSlide(float value)
    //{
    //    Debug.Log($"value : {value}");
    //    //vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = value;
    //    //vcam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = null;
    //}

    public void Tracked_Offset_X(float value)
    {
        vcam_comp.m_TrackedObjectOffset.x = value;
    }
    public void Tracked_Offset_Y(float value)
    {
        vcam_comp.m_TrackedObjectOffset.y = value;
    }
    public void Tracked_Offset_Z(float value)
    {
        vcam_comp.m_TrackedObjectOffset.z = value;
    }

    public void DeadZone_Width(float value)
    {
        vcam_comp.m_DeadZoneWidth = value;
    }

    public void DeadZone_Height(float value)
    {
        vcam_comp.m_DeadZoneHeight = value;
    }

    public void SoftZone_Width(float value)
    {
        vcam_comp.m_SoftZoneWidth = value;
    }

    public void SoftZone_Height(float value)
    {
        vcam_comp.m_SoftZoneHeight = value;
    }

    public void Bias_X(float value)
    {
        vcam_comp.m_BiasX = value;
    }

    public void Bias_Y(float value)
    {
        vcam_comp.m_BiasY = value;
    }
}
