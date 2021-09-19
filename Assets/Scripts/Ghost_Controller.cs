using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Controller : MonoBehaviour
{
    Animator Ghost_Animator;
    private float duration_Scare;
    private float duration_Awake;
    private float duration_Die;
    // Start is called before the first frame update
    void Start()
    {
        Ghost_Animator=GetComponent<Animator>();
        duration_Scare=-1;
        duration_Awake=-1;
        duration_Die=-1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Ghost_Animator.SetTrigger("Scare");
            duration_Scare=0;
        }
        if(duration_Scare>=3){
            Ghost_Animator.SetTrigger("Awake");
            duration_Scare=-1;
            duration_Awake=0;
        }
        if(duration_Awake>=3){
            Ghost_Animator.SetTrigger("Die");
            duration_Awake=-1;
            duration_Die=0;
        }
        if(duration_Die>=3){
            Ghost_Animator.SetTrigger("Recover");
            duration_Die=-1;
        }
        if(duration_Scare>=0)duration_Scare+=Time.deltaTime;
        if(duration_Awake>=0)duration_Awake+=Time.deltaTime;
        if(duration_Die>=0)duration_Die+=Time.deltaTime;
    }
}
