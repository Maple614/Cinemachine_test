using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Target_Changer : MonoBehaviour
{
    Dropdown dropdown;
    public Transform[] targets_;
    

    public Get_Current_vcam get_current_vcam;

    private int vcam_len;
    private GameObject[] vcam_obj;
    private CinemachineVirtualCamera[] vcam;
    private CinemachineTransposer[] vcam_trans;
    private CinemachineComposer[] vcam_comp;
    private int[] vcam_target_num;
    private Transform[] pivots_pos;
    private bool firstFlag = true;
    private bool skipFlag = false;


    Vector3[] init_pos;
    Vector3[] init_rot;

    int current_index;


    void Start()
    {
        // init each vcam pos&rot
        vcam_len = get_current_vcam.Vcam_len;
        vcam_obj = new GameObject[vcam_len];
        vcam = new CinemachineVirtualCamera[vcam_len];
        vcam_trans = new CinemachineTransposer[vcam_len];
        vcam_comp = new CinemachineComposer[vcam_len];
        vcam_target_num = new int[vcam_len];

        init_pos = new Vector3[vcam_len];
        init_rot = new Vector3[vcam_len];

        pivots_pos = new Transform[vcam_len];

        current_index = get_current_vcam.Current_index;

        for (int i=0; i<vcam_len; i++)
        {
            vcam_obj[i] = get_current_vcam.Get_Vcam(i);
            vcam[i] = vcam_obj[i].GetComponent<CinemachineVirtualCamera>();
            vcam_trans[i] = vcam[i].GetCinemachineComponent<CinemachineTransposer>();
            vcam_comp[i] = vcam[i].GetCinemachineComponent<CinemachineComposer>();

            init_pos[i] = vcam_obj[i].transform.position;
            init_rot[i] = vcam_obj[i].transform.localEulerAngles;

            pivots_pos[i] = get_current_vcam.Get_Pivot(i);

            vcam_target_num[i] = targets_.Length;
        }

        dropdown = this.GetComponent<Dropdown>();

        // add Drop Dpwn Options
        foreach (var target in targets_)
        {
            dropdown.options.Add(new Dropdown.OptionData { text = target.name });
        }
        dropdown.options.Add(new Dropdown.OptionData { text = "None" });
        dropdown.options.Add(new Dropdown.OptionData { text = "None & re-pos" });

        dropdown.value = targets_.Length;
        dropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        // if change vcam_index reflect on dropdown
        int temp_index = get_current_vcam.Current_index;
        if (current_index != temp_index)
        {
            skipFlag = true;
            current_index = temp_index;
            dropdown.value = vcam_target_num[current_index];
            //Debug.Log("UPdate");
        }
    }

    // change follow target from uGUI
    public void ChangeFollow()
    {
        //Debug.Log($"ChangeFollow");
        if (!firstFlag && !skipFlag)
        {
            //Debug.Log("changefollow");
            if (dropdown.value < targets_.Length)
            {
                // copy relative position between vcam and pivot to between vcam and target
                Debug.Log($"{targets_[dropdown.value]}");
                vcam[current_index].m_Follow = targets_[dropdown.value];
                vcam_target_num[current_index] = dropdown.value;

                //Vector3 relate_pos = vcam_obj[current_index].transform.position
                //    - pivots_pos[current_index].position;
                Vector3 relate_pos = vcam_obj[current_index].transform.localPosition;

                //Vector3 relate_pos = vcam_obj[current_index].transform.position
                //    - targets_[current_index].position;

                vcam_trans[current_index].m_FollowOffset.x = relate_pos.x;
                vcam_trans[current_index].m_FollowOffset.y = relate_pos.y;
                vcam_trans[current_index].m_FollowOffset.z = relate_pos.z;
            }

            else if ((dropdown.value == targets_.Length) && (vcam_target_num[current_index] < targets_.Length))
            {
                // copy relative position between vcam and target to between vcam and pivot
                Vector3 current_target_pos = targets_[vcam_target_num[current_index]].position;
                Vector3 current_vcam_pos = new Vector3(
                    current_target_pos.x + vcam_trans[current_index].m_FollowOffset.x,
                    current_target_pos.y + vcam_trans[current_index].m_FollowOffset.y,
                    current_target_pos.z + vcam_trans[current_index].m_FollowOffset.z);

                vcam[current_index].m_Follow = null;
                vcam_target_num[current_index] = targets_.Length;

                pivots_pos[current_index].position = current_target_pos;
                vcam_obj[current_index].transform.position = current_vcam_pos;

            }
            else if (vcam_target_num[current_index] < targets_.Length)
            {
                vcam[current_index].m_Follow = null;
                vcam_target_num[current_index] = targets_.Length + 1;
                vcam_obj[current_index].transform.position = new Vector3(init_pos[current_index].x,
                    init_pos[current_index].y, init_pos[current_index].z);
            }
        }
        firstFlag = false;
        skipFlag = false;
    }

    // change look at target from uGUi
    public void ChangeLookAt()
    {
        //Debug.Log("ChangeLookAt");
        if (dropdown.value < targets_.Length)
        {
            vcam[current_index].m_LookAt = targets_[dropdown.value];
            vcam_target_num[current_index] = dropdown.value;
        }
        else if (dropdown.value == targets_.Length)
        {
            vcam[current_index].m_LookAt = null;
            vcam_target_num[current_index] = targets_.Length;
        }
        else
        {
            vcam[current_index].m_LookAt = null;
            vcam_target_num[current_index] = targets_.Length + 1;
            vcam_obj[current_index].transform.localEulerAngles = new Vector3(init_rot[current_index].x,
                init_rot[current_index].y, init_rot[current_index].z);
        }
    }
}
