using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Target_Changer : MonoBehaviour
{
    [SerializeField] Dropdown m_dropdown;
    public Transform[] targets_;

    public Get_Current_vcam get_current_vcam;

    private int m_vcam_len;
    private GameObject[] vcam_obj;
    private CinemachineVirtualCamera[] vcam;
    private CinemachineTransposer[] vcam_trans;
    private CinemachineComposer[] vcam_comp;
    private int[] vcam_target;

    Vector3[] init_pos;
    Vector3[] init_rot;

    int current_index;


    void Start()
    {
        // init each vcam pos&rot
        m_vcam_len = get_current_vcam.Vcam_len;
        vcam_obj = new GameObject[m_vcam_len];
        vcam = new CinemachineVirtualCamera[m_vcam_len];
        vcam_trans = new CinemachineTransposer[m_vcam_len];
        vcam_comp = new CinemachineComposer[m_vcam_len];
        vcam_target = new int[m_vcam_len];

        init_pos = new Vector3[m_vcam_len];
        init_rot = new Vector3[m_vcam_len];

        current_index = get_current_vcam.Current_index;

        for (int i=0; i<m_vcam_len; i++)
        {
            vcam_obj[i] = get_current_vcam.Get_Vcam(i);
            vcam[i] = vcam_obj[i].GetComponent<CinemachineVirtualCamera>();
            vcam_trans[i] = vcam[i].GetCinemachineComponent<CinemachineTransposer>();
            vcam_comp[i] = vcam[i].GetCinemachineComponent<CinemachineComposer>();

            init_pos[i] = vcam_obj[i].transform.position;
            init_rot[i] = vcam_obj[i].transform.localEulerAngles;

            vcam_target[i] = targets_.Length;
        }

        m_dropdown = this.GetComponent<Dropdown>();

        // add Drop Dpwn Options
        foreach (var target in targets_)
        {
            m_dropdown.options.Add(new Dropdown.OptionData { text = target.name });
        }
        m_dropdown.options.Add(new Dropdown.OptionData { text = "None" });
        m_dropdown.options.Add(new Dropdown.OptionData { text = "None & re-pos" });

        m_dropdown.value = targets_.Length;
        m_dropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        // if change vcam_index reflect on dropdown
        int temp_index = get_current_vcam.Current_index;
        if (current_index != temp_index)
        {
            current_index = temp_index;
            m_dropdown.value = vcam_target[current_index];
        }
    }

    // change follow target from uGUI
    public void ChangeFollow()
    {

        if (m_dropdown.value < targets_.Length)
        {
            vcam[current_index].m_Follow = targets_[m_dropdown.value];
            vcam_target[current_index] = m_dropdown.value;
            smooth_follow();
        }

        else if (m_dropdown.value == targets_.Length)
        {
            vcam[current_index].m_Follow = null;
            vcam_target[current_index] = targets_.Length;
        }
        else
        {
            vcam[current_index].m_Follow = null;
            vcam_target[current_index] = targets_.Length + 1;
            vcam_obj[current_index].transform.position = new Vector3(init_pos[current_index].x,
                init_pos[current_index].y, init_pos[current_index].z);
        }
    }

    // change look at target from uGUi
    public void ChangeLookAt()
    {
        if (m_dropdown.value < targets_.Length)
        {
            vcam[current_index].m_LookAt = targets_[m_dropdown.value];
            vcam_target[current_index] = m_dropdown.value;
        }
        else if (m_dropdown.value == targets_.Length)
        {
            vcam[current_index].m_LookAt = null;
            vcam_target[current_index] = targets_.Length;
        }
        else
        {
            vcam[current_index].m_LookAt = null;
            vcam_target[current_index] = targets_.Length + 1;
            vcam_obj[current_index].transform.localEulerAngles = new Vector3(init_rot[current_index].x,
                init_rot[current_index].y, init_rot[current_index].z);
        }
    }

    void smooth_follow()
    {
        Debug.Log("AAAAAA");
        Vector3 vcam_pos = vcam_obj[current_index].transform.position;
        Vector3 target_pos = targets_[current_index].transform.position;

        Vector3 distance = new Vector3(vcam_pos.x - target_pos.x,
            vcam_pos.y - target_pos.y, vcam_pos.z - target_pos.z);

        vcam_trans[current_index].m_FollowOffset.x = distance.x;
        vcam_trans[current_index].m_FollowOffset.y = distance.y;
        vcam_trans[current_index].m_FollowOffset.z = distance.z;

        //Vector3 vcam_rot = vcam_obj[current_index].transform.eulerAngles;
        //Vector3 target_rot = targets_[current_index].transform.eulerAngles;

        //Vector3 rot_distance = new Vector3(vcam_rot.x - target_rot.x,
        //    vcam_rot.y - target_rot.y, vcam_rot.z - target_rot.z);

        //vcam_comp[current_index].m_TrackedObjectOffset.x = rot_distance.x;
        //vcam_comp[current_index].m_TrackedObjectOffset.y = rot_distance.y;
        //vcam_comp[current_index].m_TrackedObjectOffset.z = rot_distance.z;
    }

}
