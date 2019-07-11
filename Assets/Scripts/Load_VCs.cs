using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Load_VCs : MonoBehaviour
{

    public Transform[] targets;
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Awake()
    {
        Load_vcs();
    }

    private void Load_vcs()
    {

        //RectTransform content = canvas.GetComponent<RectTransform>();
        //float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
        //float btnHight = 



        foreach (var target in targets)
        {
            // instantiate vc's Prefabs and init target, LookAt object
            CinemachineVirtualCamera virtualcamera =
                (CinemachineVirtualCamera)Resources.Load<CinemachineVirtualCamera>("Prefabs/CM vcam");
            var vc = Instantiate(virtualcamera, new Vector3(0f, 0f, 0f), Quaternion.identity);
            //Debug.Log($"Instantiate VC target: {target.name}");
            //Debug.Log($"Instantiate VC type: {virtualcamera.GetType()}");

            vc.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow =
            vc.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = target.transform;


            // instantiate button's Prefabs and init placement and target object
            GameObject button =
                (GameObject)Resources.Load("Prefabs/Button");
            var btn = Instantiate(button) as GameObject;
            btn.transform.SetParent(canvas.transform, false);

            var btn_text = btn.GetComponent<Button>().transform.Find("Text").GetComponent<Text>();
            btn_text.text = target.name;

            btn.GetComponent<Camera_Selector>().setTarget(target);

        }
        Debug.Log($"Load finished");
    }

}
