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

    int vcam_len;
    GameObject current_vcam_obj;
    CinemachineVirtualCamera current_vcam;

    Vector3[] init_pos;

    int current_index;


    private void Awake()
    {
        // init each vcam pos
        vcam_len = get_current_vcam.Vcam_len;
        init_pos = new Vector3[vcam_len];


        for (int i = 0; i < vcam_len; i++)
        {
            init_pos[i] = get_current_vcam.Get_init_transform(i).position;
        }

        // add Drop Down Options
        foreach (var f_target in follow_targets_)
        {
            follow_dropdown.options.Add(new Dropdown.OptionData { text = f_target.name });
        }
        follow_dropdown.options.Add(new Dropdown.OptionData { text = "None" });
        follow_dropdown.options.Add(new Dropdown.OptionData { text = "None & re-pos" });

        follow_dropdown.value = follow_targets_.Length;
        follow_dropdown.RefreshShownValue();

    }

    // Start is called before the first frame update
    void Start()
    {
        current_vcam_obj = get_current_vcam.Current_vcam;
        current_vcam = current_vcam_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }


    public void ChangeFollow()
    {
        //Debug.Log($"{get_current_vcam.Current_vcam.name}");
        //Debug.Log($"{follow_dropdown.value}");
        current_vcam_obj = get_current_vcam.Current_vcam;
        current_index = get_current_vcam.Current_index;
        current_vcam = current_vcam_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();

        if (follow_dropdown.value < follow_targets_.Length)
        {
            current_vcam.m_Follow = follow_targets_[follow_dropdown.value];
        }

        else if (follow_dropdown.value == follow_targets_.Length)
        {
            current_vcam.m_Follow = null;
        }
        else
        {
            current_vcam.m_Follow = null;
            current_vcam_obj.transform.position = new Vector3(init_pos[current_index].x,
                init_pos[current_index].y, init_pos[current_index].z);
        }
    }
}
