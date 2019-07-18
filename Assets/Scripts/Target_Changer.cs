using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Target_Changer : MonoBehaviour
{
    [SerializeField] Dropdown follow_dropdown;
    [SerializeField] Dropdown lookat_dropdown;
    public Transform[] follow_targets_;
    public Transform[] lookat_targets_;

    public Get_Current_vcam get_current_vcam;

    private int vcam_len;
    //private GameObject[] vcam_obj;
    private GameObject current_vcam_obj;
    private CinemachineVirtualCamera current_vcam;

    Vector3[] init_pos;
    Vector3[] init_rot;

    int current_index;
//    private CinemachineTransposer vcam_trans;


    private void Awake()
    {
        // init each vcam pos&rot
        vcam_len = get_current_vcam.Vcam_len;
        init_pos = new Vector3[vcam_len];
        init_rot = new Vector3[vcam_len];


        for (int i=0; i<vcam_len; i++)
        {
            Debug.Log($"{i}");
            Debug.Log($"{get_current_vcam.Get_init_transform(i).position}");
            init_pos[i] = get_current_vcam.Get_init_transform(i).position;
            init_rot[i] = get_current_vcam.Get_init_transform(i).localEulerAngles;
        }

        // add Drop Dpwn Options
        foreach(var f_target in follow_targets_)
        {
            follow_dropdown.options.Add(new Dropdown.OptionData { text = f_target.name });
        }
        follow_dropdown.options.Add(new Dropdown.OptionData { text = "None" });
        follow_dropdown.options.Add(new Dropdown.OptionData { text = "None & re-pos" });

        foreach (var l_target in lookat_targets_)
        {
            lookat_dropdown.options.Add(new Dropdown.OptionData { text = l_target.name });
        }
        lookat_dropdown.options.Add(new Dropdown.OptionData { text = "None" });
        lookat_dropdown.options.Add(new Dropdown.OptionData { text = "None & re-rot" });


    }

    // Start is called before the first frame update
    void Start()
    {
        current_vcam = current_vcam_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        follow_dropdown.value = follow_targets_.Length;
        lookat_dropdown.value = lookat_targets_.Length;

    }

    // Update is called once per frame
    void Update()
    {
        current_vcam_obj = get_current_vcam.Current_vcam;
        current_index = get_current_vcam.Current_index;
    }


    public void ChangeFollow()
    {
        Debug.Log("Change Follow Method");
        current_vcam = current_vcam_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        if (follow_dropdown.value < follow_targets_.Length)
        {
            current_vcam.m_Follow = follow_targets_[follow_dropdown.value];
        }

        else if(follow_dropdown.value == follow_targets_.Length)
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

    public void ChangeLookAt()
    {
        Debug.Log("Change LookAt Method");
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
