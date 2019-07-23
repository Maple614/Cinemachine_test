using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class LookAt_Changer : MonoBehaviour
{
    [SerializeField] Dropdown lookat_dropdown;
    public Transform[] lookat_targets_;

    public Get_Current_vcam get_current_vcam;

    private int vcam_len;
    GameObject[] vcam_obj;
    CinemachineVirtualCamera[] vcam;
    Vector3[] init_rot;
    int[] vcam_target;

    int current_index;


    void Start()
    {

        // init each vcam rot
        vcam_len = get_current_vcam.Vcam_len;
        vcam_obj = new GameObject[vcam_len];
        vcam = new CinemachineVirtualCamera[vcam_len];
        init_rot = new Vector3[vcam_len];
        vcam_target = new int[vcam_len];


        for (int i = 0; i < vcam_len; i++)
        {
            vcam_obj[i] = get_current_vcam.Get_Vcam(i);
            vcam[i] = vcam_obj[i].GetComponent<CinemachineVirtualCamera>();
            init_rot[i] = get_current_vcam.Get_init_transform(i).localEulerAngles;

            vcam_target[i] = lookat_targets_.Length;
        }

        lookat_dropdown = this.GetComponent<Dropdown>();
        // add Drop Down Options
        foreach (var l_target in lookat_targets_)
        {
            lookat_dropdown.options.Add(new Dropdown.OptionData { text = l_target.name });
        }
        lookat_dropdown.options.Add(new Dropdown.OptionData { text = "None" });
        lookat_dropdown.options.Add(new Dropdown.OptionData { text = "None & re-rot" });

        lookat_dropdown.value = lookat_targets_.Length;
        lookat_dropdown.RefreshShownValue();
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


    public void ChangeLookAt()
    {
        if (lookat_dropdown.value < lookat_targets_.Length)
        {
            vcam[current_index].m_LookAt = lookat_targets_[lookat_dropdown.value];
            vcam_target[current_index] = lookat_dropdown.value;
        }

        else if (lookat_dropdown.value == lookat_targets_.Length)
        {
            vcam[current_index].m_LookAt = null;
            vcam_target[current_index] = lookat_targets_.Length;
        }
        else
        {
            vcam[current_index].m_LookAt = null;
            vcam_target[current_index] = lookat_targets_.Length + 1;
            vcam_obj[current_index].transform.localEulerAngles = new Vector3(init_rot[current_index].x,
                init_rot[current_index].y, init_rot[current_index].z);
        }
    }


    // value reflect on DropDown if change active vcam
    void Reflect_DropDownValue()
    {
        lookat_dropdown.value = vcam_target[current_index];
    }

}
