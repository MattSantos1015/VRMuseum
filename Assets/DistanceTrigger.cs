using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DistanceTrigger : MonoBehaviour
{
    

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float activationDistance = 3.0f;
    [SerializeField]
    private float activationDelay = 10.0f;
    [SerializeField]
   
    private string triggerName = "startAnim";
    
    private float timer;

    private Animator anim;
    private AudioSource aud;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        aud = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, target.position);
        if (distance < activationDistance) 
        { 
            Activate(); 
        }
        if (timer > 0) timer -= Time.deltaTime;
    }
    void Activate()
    {
        anim.SetTrigger(triggerName);
        aud.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, activationDistance);
    }
}
