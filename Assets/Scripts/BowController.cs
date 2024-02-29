using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;
    
    private XRGrabInteractable interactable;
    
    [SerializeField]
    private Transform midPointGrabObject;
    
    private Transform interactor;
 
    private void Awake(){
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }


    void Start()
    {
        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
    }

    private void ResetBowString(SelectExitEventArgs arg0)  {
        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);
    }

    private void PrepareBowString(SelectEnterEventArgs arg0) {
        interactor = arg0.interactorObject.transform;
    }

   
    void Update() {
        if (interactor != null) {
            bowStringRenderer.CreateString(midPointGrabObject.transform.position);
            Debug.Log("working");
        }
    }
}
