using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class LookAt_Changer : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    public Transform[] targets_;
    [SerializeField] GameObject vcam_obj;
    private CinemachineVirtualCamera vcam;
    private Vector3 init_rot;

    void Start()
    {
        init_rot = vcam_obj.transform.localEulerAngles;
        vcam = vcam_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        dropdown.value = 4;
    }

    public void ChangeLookAt()
    {
        if(dropdown.value == 0)
        {
            vcam.m_LookAt = targets_[0];
        }
        else if (dropdown.value == 1)
        {
            vcam.m_LookAt = targets_[1];
        }
        else if (dropdown.value == 2)
        {
            vcam.m_LookAt = targets_[2];
        }
        else if (dropdown.value == 3)
        {
            vcam.m_LookAt = targets_[3];
        }
        else if (dropdown.value == 4)
        {
            vcam.m_LookAt = null;
        }
        else
        {
            vcam.m_LookAt = null;
            vcam_obj.transform.localEulerAngles = new Vector3(init_rot.x, init_rot.y, init_rot.z);
        }
    }
}
