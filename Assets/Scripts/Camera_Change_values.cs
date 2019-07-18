using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Camera_Change_values : MonoBehaviour
{
    public Get_Current_vcam get_current_vcam;
    CinemachineVirtualCamera vcam;
    private CinemachineComposer vcam_comp;
    int vcam_len;
    float[,] sliders_values;
    int current_index;
    Slider slider;

    void Start()
    {
        slider = this.GetComponent<Slider>();

        vcam_len = get_current_vcam.Vcam_len;
        sliders_values = new float[vcam_len,9];

        vcam = get_current_vcam.Current_vcam.GetComponent<CinemachineVirtualCamera>();
        vcam_comp = vcam.GetCinemachineComponent<CinemachineComposer>();

        for(int i=0; i<vcam_len; i++)
        {
            CinemachineComposer temp_vcam_comp = 
                get_current_vcam.Get_Vcam(i)
                .GetComponent<CinemachineVirtualCamera>()
                .GetCinemachineComponent<CinemachineComposer>();

            sliders_values[i,0] = temp_vcam_comp.m_TrackedObjectOffset.x;
            sliders_values[i,1] = temp_vcam_comp.m_TrackedObjectOffset.y;
            sliders_values[i,2] = temp_vcam_comp.m_TrackedObjectOffset.z;

            sliders_values[i,3] = temp_vcam_comp.m_DeadZoneWidth;
            sliders_values[i,4] = temp_vcam_comp.m_DeadZoneHeight;

            sliders_values[i,5] = temp_vcam_comp.m_SoftZoneWidth;
            sliders_values[i,6] = temp_vcam_comp.m_SoftZoneHeight;

            sliders_values[i,7] = temp_vcam_comp.m_BiasX;
            sliders_values[i,8] = temp_vcam_comp.m_BiasY;
        }
    }

    public void Tracked_Offset_X(float value)
    {
        get_vcam_Component();
        vcam_comp.m_TrackedObjectOffset.x = value;
    }
    public void Tracked_Offset_Y(float value)
    {
        get_vcam_Component();
        vcam_comp.m_TrackedObjectOffset.y = value;
    }
    public void Tracked_Offset_Z(float value)
    {
        get_vcam_Component();
        vcam_comp.m_TrackedObjectOffset.z = value;
    }

    public void DeadZone_Width(float value)
    {
        get_vcam_Component();
        vcam_comp.m_DeadZoneWidth = value;
    }

    public void DeadZone_Height(float value)
    {
        get_vcam_Component();
        vcam_comp.m_DeadZoneHeight = value;
    }

    public void SoftZone_Width(float value)
    {
        get_vcam_Component();
        vcam_comp.m_SoftZoneWidth = value;
    }

    public void SoftZone_Height(float value)
    {
        get_vcam_Component();
        vcam_comp.m_SoftZoneHeight = value;
    }

    public void Bias_X(float value)
    {
        get_vcam_Component();
        vcam_comp.m_BiasX = value;
    }

    public void Bias_Y(float value)
    {
        get_vcam_Component();
        vcam_comp.m_BiasY = value;
    }

    void get_vcam_Component()
    {
        current_index = get_current_vcam.Current_index;
        vcam = get_current_vcam.Current_vcam.GetComponent<CinemachineVirtualCamera>();
        vcam_comp = vcam.GetCinemachineComponent<CinemachineComposer>();
    }
}
