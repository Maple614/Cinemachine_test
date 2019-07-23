using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Follow_Changer : MonoBehaviour
{
    [SerializeField] Dropdown follow_dropdown;
    public Transform[] follow_targets_;

    public Get_Current_vcam get_current_vcam;

    int m_vcam_len;
    GameObject[] vcam_obj;
    CinemachineVirtualCamera[] vcam;
    Vector3[] init_pos;
    int[] vcam_target;

    int current_index;


    // Start is called before the first frame update
    void Start()
    {

        // init each vcam pos
        m_vcam_len = get_current_vcam.Vcam_len;
        vcam_obj = new GameObject[m_vcam_len];
        vcam = new CinemachineVirtualCamera[m_vcam_len];
        init_pos = new Vector3[m_vcam_len];
        vcam_target = new int[m_vcam_len];

        current_index = get_current_vcam.Current_index;

        for (int i = 0; i < m_vcam_len; i++)
        {
            vcam_obj[i] = get_current_vcam.Get_Vcam(i);
            vcam[i] = vcam_obj[i].GetComponent<CinemachineVirtualCamera>();
            init_pos[i] = get_current_vcam.Get_init_transform(i).position;

            // set follow none
            vcam_target[i] = follow_targets_.Length;
        }


        follow_dropdown = this.GetComponent<Dropdown>();
        // add DropDown Options
        foreach (var f_target in follow_targets_)
        {
            follow_dropdown.options.Add(new Dropdown.OptionData { text = f_target.name });
        }
        follow_dropdown.options.Add(new Dropdown.OptionData { text = "None" });
        follow_dropdown.options.Add(new Dropdown.OptionData { text = "None & re-pos" });


        follow_dropdown.value = follow_targets_.Length;
        follow_dropdown.RefreshShownValue();

    }

    private void Update()
    {
        // if change vcam_index reflect on slider
        int temp_index = get_current_vcam.Current_index;
        if (current_index != temp_index)
        {
            current_index = temp_index;
            Reflect_DropDownValue();
        }
    }

    public void ChangeFollow()
    {
        if (follow_dropdown.value < follow_targets_.Length)
        {
            vcam[current_index].m_Follow = follow_targets_[follow_dropdown.value];
            vcam_target[current_index] = follow_dropdown.value;
        }

        else if (follow_dropdown.value == follow_targets_.Length)
        {
            vcam[current_index].m_Follow = null;
            vcam_target[current_index] = follow_targets_.Length;
        }
        else
        {
            vcam[current_index].m_Follow = null;
            vcam_target[current_index] = follow_targets_.Length + 1;
            vcam_obj[current_index].transform.position = new Vector3(init_pos[current_index].x,
                init_pos[current_index].y, init_pos[current_index].z);
        }
    }

    // value reflect on DropDown if change active vcam
    void Reflect_DropDownValue()
    {
        follow_dropdown.value = vcam_target[current_index];
    }
}
