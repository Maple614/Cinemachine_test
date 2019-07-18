using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Current_vcam : MonoBehaviour
{
    public GameObject[] vcams;
    public int Vcam_len { get { return vcams.Length; } }
    public GameObject Current_vcam { get { return m_current_vcam; } }
    public int Current_index { get { return m_index; } }

    private GameObject m_current_vcam;
    private int m_index;

    private void Awake()
    {
        m_current_vcam = vcams[0];
        m_index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {

            m_current_vcam = vcams[0];
            m_index = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            m_current_vcam = vcams[1];
            m_index = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            m_current_vcam = vcams[2];
            m_index = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            m_current_vcam = vcams[3];
            m_index = 3;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            m_current_vcam = vcams[4];
            m_index = 4;
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            m_current_vcam = vcams[5];
            m_index = 5;
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            m_current_vcam = vcams[6];
            m_index = 6;
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            m_current_vcam = vcams[7];
            m_index = 7;
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            m_current_vcam = vcams[8];
            m_index = 8;
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            m_current_vcam = vcams[9];
            m_index = 9;
        }

        //Debug.Log($"{m_index}");
    }

    public Transform Get_init_transform(int index)
    {
        return vcams[index].transform;
    }

}
