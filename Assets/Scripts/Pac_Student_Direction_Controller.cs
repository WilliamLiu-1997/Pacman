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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Pac_Animator.SetInteger("State",1);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            Pac_Animator.SetInteger("State",2);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            Pac_Animator.SetInteger("State",3);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            Pac_Animator.SetInteger("State",4);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Pac_Animator.SetTrigger("Die");
        }
    }
}
