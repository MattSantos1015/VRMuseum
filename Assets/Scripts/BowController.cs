using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;
    
    private XRGrabInteractable interactable;

    [SerializeField] private Transform midPointGrabObject, midPointVisualObject, midPointParent;

    [SerializeField] private float bowStringStretchLimtit = .3f;
    private Transform interactor;
    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }
    private void Start()
    {
        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
    }
    private void Update()
    {
        if (interactor != null)
        {
            bowStringRenderer.CreateString(midPointGrabObject.transform.position);
            Vector3 midPointLocalSpace = midPointParent.InverseTransformPoint(midPointGrabObject.position);
            
            float midPointLocalZabs = Mathf.Abs(midPointLocalSpace.z);
            
            HandleStringPushedBackToStart(midPointLocalSpace);

            HandleStringPulledBackTolimit(midPointLocalZabs, midPointLocalSpace );

            HandlePullingString(midPointLocalZabs, midPointLocalSpace);

            bowStringRenderer.CreateString(midPointVisualObject.position);
        }
    }

    private void HandlePullingString(float midPointLocalZabs, Vector3 midPointLocalSpace)
    {
        if(midPointLocalSpace.z < 0 && midPointLocalZabs < bowStringStretchLimtit)
        {
            midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z); 
        }
    }

    private void HandleStringPulledBackTolimit(float midPointLocalZabs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z < 0 && midPointLocalZabs >= bowStringStretchLimtit)
        { 
         midPointVisualObject.localPosition = new Vector3(0,0, -bowStringStretchLimtit);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if ( midPointLocalSpace.z >= 0)
        {
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }
}