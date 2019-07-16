using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Follow_Changer : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    public Transform[] targets_;
    [SerializeField] public GameObject vcam_obj;
    private CinemachineVirtualCamera vcam;
    private Vector3 init_pos;
    private CinemachineTransposer vcam_trans;

    void Start()
    {
        init_pos = vcam_obj.transform.position;
        vcam = vcam_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        vcam_trans = vcam.GetCinemachineComponent<CinemachineTransposer>();
        dropdown.value = 4;
    }

    public void ChangeFollow()
    {
        vcam_obj = this.GetComponent<GameObject>();

        if(dropdown.value == 0)
        {
            Debug.Log($"vcam_x {vcam_obj.transform.position.x}");
            Debug.Log($"vcam_y {vcam_obj.transform.position.y}");
            Debug.Log($"vcam_z {vcam_obj.transform.position.z}");

            vcam.m_Follow = targets_[0];
            vcam_trans.m_FollowOffset.x = vcam_obj.transform.position.x;
            vcam_trans.m_FollowOffset.y = vcam_obj.transform.position.y;
            vcam_trans.m_FollowOffset.z = vcam_obj.transform.position.z;
        }
        else if (dropdown.value == 1)
        {
            vcam.m_Follow = targets_[1];
        }
        else if (dropdown.value == 2)
        {
            vcam.m_Follow = targets_[2];
        }
        else if (dropdown.value == 3)
        {
            vcam.m_Follow = targets_[3];
        }
        else if (dropdown.value == 4)
        {
            vcam.m_Follow = null;
        }
        else
        {
            vcam.m_Follow = null;
            vcam_obj.transform.position = new Vector3(init_pos.x, init_pos.y, init_pos.z);
        }
    }
}
