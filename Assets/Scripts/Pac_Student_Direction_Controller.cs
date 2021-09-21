using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac_Student_Direction_Controller : MonoBehaviour
{
    Animator Pac_Animator;
    // Start is called before the first frame update
    void Start()
    {
        Pac_Animator=GetComponent<Animator>();
        Pac_Animator.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.UpArrow)){
        //     ResetTrigger();
        //     Pac_Animator.SetTrigger("Up");
        // }
        // if(Input.GetKeyDown(KeyCode.DownArrow)){
        //     ResetTrigger();
        //     Pac_Animator.SetTrigger("Down");
        // }
        // if(Input.GetKeyDown(KeyCode.LeftArrow)){
        //     ResetTrigger();
        //     Pac_Animator.SetTrigger("Left");
        // }
        // if(Input.GetKeyDown(KeyCode.RightArrow)){
        //     ResetTrigger();
        //     Pac_Animator.SetTrigger("Right");
        // }
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     ResetTrigger();
        //     Pac_Animator.SetTrigger("Die");
        // }
    }

    public void ResetTrigger(){
            Pac_Animator.ResetTrigger("Up");
            Pac_Animator.ResetTrigger("Down");
            Pac_Animator.ResetTrigger("Left");
            Pac_Animator.ResetTrigger("Right");
            Pac_Animator.ResetTrigger("Die");
        
    }
}
