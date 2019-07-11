using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Linq;

public class Camera_Selector : MonoBehaviour
{
    public Transform target;
    private CinemachineVirtualCamera[] virtualCameras;

    void Start()
    {
        this.virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        var button = this.GetComponent<Button>();
        var text = button.transform.Find("Text").GetComponent<Text>();
        button.onClick.AddListener(this.SwitchTarget);
        text.text = this.target.name;
    }

    // Switch Target
    private void SwitchTarget()
    {
        Debug.Log($"Target: {this.target.name}");

        foreach (var virtualCamera in this.virtualCameras)
        {
            virtualCamera.enabled = virtualCamera.Follow == this.target;
        }

    }

    public void setTarget(Transform target_)
    {
        target = target_;
    }
}
