using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac_Student_Controller : MonoBehaviour
{
    Animator Pac_Animator;
    public AudioSource Moving_Sound;
    private Tweener tweener;
    private int[] Map_Size;
    private Level_Generator[] LevelGenerator;
    private bool Moving;
    
    // Start is called before the first frame update
    void Start()
    {
        Pac_Animator=GetComponent<Animator>();
        LevelGenerator=GameObject.Find("Map").GetComponents<Level_Generator>();
        Map_Size=LevelGenerator[0].Get_Size();
        gameObject.transform.position=new Vector3(-Map_Size[0]+1,Map_Size[1]-2,0);
        tweener=GetComponent<Tweener>();
        Moving_Sound=GetComponent<AudioSource>();
        Moving=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Moving){
            Moving=true;
            Moving_Sound.Play();
        }
        if(Moving){
            if(gameObject.transform.position.y==Map_Size[1]-2&&gameObject.transform.position.x<-Map_Size[0]+6){
                ResetTrigger();
                Pac_Animator.SetTrigger("Right");
                tweener.AddTween(gameObject.transform,gameObject.transform.position,new Vector3(gameObject.transform.position.x+1,gameObject.transform.position.y,gameObject.transform.position.z),0.2f);
            }
            if(gameObject.transform.position.y==Map_Size[1]-6&&gameObject.transform.position.x>-Map_Size[0]+1){
                ResetTrigger();
                Pac_Animator.SetTrigger("Left");
                tweener.AddTween(gameObject.transform,gameObject.transform.position,new Vector3(gameObject.transform.position.x-1,gameObject.transform.position.y,gameObject.transform.position.z),0.2f);
            }
            if(gameObject.transform.position.x==-Map_Size[0]+1&&gameObject.transform.position.y<Map_Size[1]-2){
                ResetTrigger();
                Pac_Animator.SetTrigger("Up");
                tweener.AddTween(gameObject.transform,gameObject.transform.position,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,gameObject.transform.position.z),0.2f);
            }
            if(gameObject.transform.position.x==-Map_Size[0]+6&&gameObject.transform.position.y>Map_Size[1]-6){
                ResetTrigger();
                Pac_Animator.SetTrigger("Down");
                tweener.AddTween(gameObject.transform,gameObject.transform.position,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-1,gameObject.transform.position.z),0.2f);
            }
        }
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
