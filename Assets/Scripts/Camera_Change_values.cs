using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Camera_Change_values : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    public Canvas canvas;
    public Slider slider;
    float Body_x;
    // Start is called before the first frame update
    void Start()
    {
        vcam = this.GetComponent<CinemachineVirtualCamera>();

        //var slider = this.transform.Find("Slider").GetComponent<Slider>();
        slider.transform.SetParent(canvas.transform);
        slider.onValueChanged.AddListener(OnSlide);
    }

    void OnSlide(float value)
    {
        Debug.Log($"value : {value}");
        //vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = value;
        vcam.GetCinemachineComponent<CinemachineComposer>().m_SoftZoneHeight = value;
    }
}
