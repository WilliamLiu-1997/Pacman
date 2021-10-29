using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Controller : MonoBehaviour
{
    Animator Ghost_Animator;
    private float duration_Normal;
    private float duration_Scare;
    private float duration_Awake;
    private float duration_Die;
    private float duration_Rotation;
    // Start is called before the first frame update
    void Start()
    {
        Ghost_Animator=GetComponent<Animator>();
        duration_Normal=0;
        duration_Scare=-1;
        duration_Awake=-1;
        duration_Die=-1;
        duration_Rotation=0;
    }

    // Update is called once per frame
    void Update()
    {
        // if(duration_Normal>=3){
        //     Ghost_Animator.SetTrigger("Scare");
        //     duration_Normal=-1;
        //     duration_Scare=0;
        // }
        // if(duration_Scare>=3){
        //     Ghost_Animator.SetTrigger("Awake");
        //     duration_Scare=-1;
        //     duration_Awake=0;
        // }
        // if(duration_Awake>=3){
        //     Ghost_Animator.SetTrigger("Die");
        //     duration_Awake=-1;
        //     duration_Die=0;
        // }
        // if(duration_Die>=3){
        //     Ghost_Animator.SetTrigger("Recover");
        //     duration_Die=-1;
        //     duration_Normal=0;
        // }
        // if(duration_Normal>=0)duration_Normal+=Time.deltaTime;
        // if(duration_Scare>=0)duration_Scare+=Time.deltaTime;
        // if(duration_Awake>=0)duration_Awake+=Time.deltaTime;
        // if(duration_Die>=0)duration_Die+=Time.deltaTime;
        
        // duration_Rotation+=Time.deltaTime;
        // if(duration_Rotation>=0&&gameObject.transform.eulerAngles.z!=0){
        //     gameObject.transform.eulerAngles=new Vector3(0, 0, 0);
        // }
        // if(duration_Rotation>=1&&gameObject.transform.eulerAngles.z!=270){
        //     gameObject.transform.eulerAngles=new Vector3(0, 0, 270);
        // }
        // if(duration_Rotation>=2&&gameObject.transform.eulerAngles.z!=180){
        //     gameObject.transform.eulerAngles=new Vector3(0, 0, 180);
        // }
        // if(duration_Rotation>=3&&gameObject.transform.eulerAngles.z!=90){
        //     gameObject.transform.eulerAngles=new Vector3(0, 0, 90);
        // }
        // if(duration_Rotation>4)duration_Rotation=0;
    }
}
