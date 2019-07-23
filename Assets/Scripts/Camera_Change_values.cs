using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Camera_Change_values : MonoBehaviour
{
    public Get_Current_vcam get_current_vcam;
    private CinemachineComposer[] vcam_comp;
    int m_vcam_len;
    float[,] sliders_values;
    int current_index;
    int m_slider_index;
    Slider m_slider;
    

    void Start()
    {
        m_slider = this.GetComponent<Slider>();
        m_slider_index = int.Parse(m_slider.name.ToString().Substring(8, 1));

        current_index = get_current_vcam.Current_index;

        m_vcam_len = get_current_vcam.Vcam_len;
        sliders_values = new float[m_vcam_len, 9];
        vcam_comp = new CinemachineComposer[m_vcam_len];

        for(int i=0; i< m_vcam_len; i++)
        {
            vcam_comp[i] = 
                get_current_vcam.Get_Vcam(i)
                .GetComponent<CinemachineVirtualCamera>()
                .GetCinemachineComponent<CinemachineComposer>();


            sliders_values[i,0] = vcam_comp[i].m_DeadZoneWidth;
            sliders_values[i,1] = vcam_comp[i].m_DeadZoneHeight;

            sliders_values[i,2] = vcam_comp[i].m_SoftZoneWidth;
            sliders_values[i,3] = vcam_comp[i].m_SoftZoneHeight;

            sliders_values[i,4] = vcam_comp[i].m_BiasX;
            sliders_values[i,5] = vcam_comp[i].m_BiasY;

            sliders_values[i, 6] = vcam_comp[i].m_TrackedObjectOffset.x;
            sliders_values[i, 7] = vcam_comp[i].m_TrackedObjectOffset.y;
            sliders_values[i, 8] = vcam_comp[i].m_TrackedObjectOffset.z;
        }
    }

    private void Update()
    {
        // if change vcam_index reflect on slider
        int temp_index = get_current_vcam.Current_index;
        if(current_index != temp_index)
        {
            current_index = temp_index;
            Reflect_SliderValues();
        }
    }

    // change slider values from uGUI
    public void DeadZone_Width(float value)
    {
        vcam_comp[current_index].m_DeadZoneWidth = value;
        sliders_values[current_index, 0] = value;
    }
    public void DeadZone_Height(float value)
    {
        vcam_comp[current_index].m_DeadZoneHeight = value;
        sliders_values[current_index, 1] = value;
    }
    public void SoftZone_Width(float value)
    {
        vcam_comp[current_index].m_SoftZoneWidth = value;
        sliders_values[current_index, 2] = value;
    }
    public void SoftZone_Height(float value)
    {
        vcam_comp[current_index].m_SoftZoneHeight = value;
        sliders_values[current_index, 3] = value;
    }
    public void Bias_X(float value)
    {
        vcam_comp[current_index].m_BiasX = value;
        sliders_values[current_index, 4] = value;
    }
    public void Bias_Y(float value)
    {
        vcam_comp[current_index].m_BiasY = value;
        sliders_values[current_index, 5] = value;
    }
    public void Tracked_Offset_X(float value)
    {
        vcam_comp[current_index].m_TrackedObjectOffset.x = value;
        sliders_values[current_index, 6] = value;
    }
    public void Tracked_Offset_Y(float value)
    {
        vcam_comp[current_index].m_TrackedObjectOffset.y = value;
        sliders_values[current_index, 7] = value;
    }
    public void Tracked_Offset_Z(float value)
    {
        vcam_comp[current_index].m_TrackedObjectOffset.z = value;
        sliders_values[current_index, 8] = value;
    }

    // value reflect on slider if change active vcam
    void Reflect_SliderValues()
    {
        m_slider.value = sliders_values[current_index, m_slider_index];
    }
}
