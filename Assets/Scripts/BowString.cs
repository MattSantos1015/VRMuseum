using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BowString : MonoBehaviour
{
    [SerializeField]
    private Transform endpoint_1, endpoint_2;

    private LineRenderer bowString;

    private void Awake(){
        bowString = GetComponent<LineRenderer>();
    }
    public void CreateString(Vector3? midPosition){
        Vector3[]linePoints = new Vector3[midPosition == null ? 2 : 3];
        linePoints[0] = endpoint_1.localPosition;
           if (midPosition != null){
            linePoints[1] = transform.InverseTransformPoint(midPosition.Value);
           } 
            linePoints[1] = endpoint_2.localPosition;
          
        bowString.positionCount = linePoints.Length;
            bowString.SetPositions(linePoints);
    }


    void Start()
    {
        CreateString(null);
    }

   
}
