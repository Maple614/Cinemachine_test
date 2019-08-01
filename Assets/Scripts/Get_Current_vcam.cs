using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Current_vcam : MonoBehaviour
{
    public GameObject[] vcams;
    public GameObject[] pivots;
    public int Vcam_len { get { return vcams.Length; } }
    public int Current_index { get { return m_index; } }

    private GameObject m_current_vcam;
    private int m_index;

    private void Awake()
    {
        m_current_vcam = vcams[1];
        m_index = 1;
        unable_other_vcam();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            m_current_vcam = vcams[0];
            m_index = 0;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            m_current_vcam = vcams[1];
            m_index = 1;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            m_current_vcam = vcams[2];
            m_index = 2;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            m_current_vcam = vcams[3];
            m_index = 3;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            m_current_vcam = vcams[4];
            m_index = 4;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            m_current_vcam = vcams[5];
            m_index = 5;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            m_current_vcam = vcams[6];
            m_index = 6;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            m_current_vcam = vcams[7];
            m_index = 7;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            m_current_vcam = vcams[8];
            m_index = 8;
            unable_other_vcam();
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            m_current_vcam = vcams[9];
            m_index = 9;
            unable_other_vcam();
        }
    }

    public GameObject Get_Vcam(int index)
    {
        return vcams[index];
    }

    public Transform Get_Pivot(int index)
    {
        return pivots[index].transform;
    }

    public void unable_other_vcam()
    {
        vcams[Current_index].SetActive(true);
        for(int i=0; i<vcams.Length; i++)
        {
            if (i != Current_index)
            {
                vcams[i].SetActive(false);
            }
        }
    }

}
