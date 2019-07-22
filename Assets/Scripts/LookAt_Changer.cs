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
    GameObject current_vcam_obj;
    CinemachineVirtualCamera current_vcam;

    Vector3[] init_rot;

    int current_index;


    private void Awake()
    {

    }

    void Start()
    {
        current_vcam_obj = get_current_vcam.Current_vcam;
        current_vcam = current_vcam_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();


        // init each vcam rot
        vcam_len = get_current_vcam.Vcam_len;
        init_rot = new Vector3[vcam_len];


        for (int i = 0; i < vcam_len; i++)
        {
            init_rot[i] = get_current_vcam.Get_init_transform(i).localEulerAngles;
        }

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



    public void ChangeLookAt()
    {
        current_vcam_obj = get_current_vcam.Current_vcam;
        current_index = get_current_vcam.Current_index;
        current_vcam = current_vcam_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();

        if (lookat_dropdown.value < lookat_targets_.Length)
        {
            current_vcam.m_LookAt = lookat_targets_[lookat_dropdown.value];
        }

        else if (lookat_dropdown.value == lookat_targets_.Length)
        {
            current_vcam.m_LookAt = null;
        }
        else
        {
            current_vcam.m_LookAt = null;
            current_vcam_obj.transform.localEulerAngles = new Vector3(init_rot[current_index].x,
                init_rot[current_index].y, init_rot[current_index].z);
        }
    }
}
