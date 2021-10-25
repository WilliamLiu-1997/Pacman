using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PacStudentController : MonoBehaviour
{
    Animator Pac_Animator;
    public AudioSource Moving_Sound;
    private Tweener tweener;
    private int[] Map_Size;
    private LevelGenerator[] LevelGenerator;
    private bool Moving;
    private int[,] map;
    private string lastInput;
    private string currentInput;
    private float[] destination;
    private float[] source;

    // Start is called before the first frame update
    void Start()
    {
        Pac_Animator = GetComponent<Animator>();
        LevelGenerator = GameObject.Find("Map").GetComponents<LevelGenerator>();
        Map_Size = LevelGenerator[0].Get_Size();
        map = LevelGenerator[0].Get_Map();
        gameObject.transform.position = new Vector3(-Map_Size[0] + 1, Map_Size[1] - 2, 0);
        destination = new float[2] { gameObject.transform.position.x, gameObject.transform.position.y };
        source = new float[2] { gameObject.transform.position.x, gameObject.transform.position.y };
        tweener = GetComponent<Tweener>();
        Moving_Sound = GetComponent<AudioSource>();
        Moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Moving)
        {
            Moving = true;
            Moving_Sound.Play();
        }
        if (Moving)
        {
            GetInput();
            Move();
            // if(gameObject.transform.position.y==Map_Size[1]-2&&gameObject.transform.position.x<-Map_Size[0]+6){
            //     ResetTrigger();
            //     Pac_Animator.SetTrigger("Right");
            //     tweener.AddTween(gameObject.transform,gameObject.transform.position,new Vector3(gameObject.transform.position.x+1,gameObject.transform.position.y,gameObject.transform.position.z),0.2f);
            // }
            // if(gameObject.transform.position.y==Map_Size[1]-6&&gameObject.transform.position.x>-Map_Size[0]+1){
            //     ResetTrigger();
            //     Pac_Animator.SetTrigger("Left");
            //     tweener.AddTween(gameObject.transform,gameObject.transform.position,new Vector3(gameObject.transform.position.x-1,gameObject.transform.position.y,gameObject.transform.position.z),0.2f);
            // }
            // if(gameObject.transform.position.x==-Map_Size[0]+1&&gameObject.transform.position.y<Map_Size[1]-2){
            //     ResetTrigger();
            //     Pac_Animator.SetTrigger("Up");
            //     tweener.AddTween(gameObject.transform,gameObject.transform.position,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,gameObject.transform.position.z),0.2f);
            // }
            // if(gameObject.transform.position.x==-Map_Size[0]+6&&gameObject.transform.position.y>Map_Size[1]-6){
            //     ResetTrigger();
            //     Pac_Animator.SetTrigger("Down");
            //     tweener.AddTween(gameObject.transform,gameObject.transform.position,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-1,gameObject.transform.position.z),0.2f);
            // }
        }
    }

    public void GetInput()
    {
        int[] block = new int[5] { 1, 2, 3, 4, 7 };
        float X = gameObject.transform.position.x;
        float Y = gameObject.transform.position.y;
        int X_in_Map = (int)destination[0] + Map_Size[0];
        int Y_in_Map = (int)-destination[1] + Map_Size[1] - 1;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            lastInput = "up";
            if (currentInput == null) { currentInput = lastInput; }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            lastInput = "down";
            if (currentInput == null) { currentInput = lastInput; }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            lastInput = "left";
            if (currentInput == null) { currentInput = lastInput; }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            lastInput = "right";
            if (currentInput == null) { currentInput = lastInput; }
        }
        if (lastInput == "up")
        {
            if (!(Y_in_Map == 0 || block.Contains(map[Y_in_Map - 1, X_in_Map]))) { currentInput = lastInput; }
        }
        if (lastInput == "down")
        {
            if (!(Y_in_Map == Map_Size[1] * 2 - 1 || block.Contains(map[Y_in_Map + 1, X_in_Map]))) { currentInput = lastInput; }
        }
        if (lastInput == "left")
        {
            if (!(X_in_Map == 0 || block.Contains(map[Y_in_Map, X_in_Map - 1]))) { currentInput = lastInput; }
        }
        if (lastInput == "right")
        {
            if (!(X_in_Map == Map_Size[0] * 2 || block.Contains(map[Y_in_Map, X_in_Map + 1]))) { currentInput = lastInput; }
        }
    }

    public void Move()
    {
        int[] block = new int[5] { 1, 2, 3, 4,7 };
        float X = gameObject.transform.position.x;
        float Y = gameObject.transform.position.y;
        if (X != destination[0] || Y != destination[1]) { return; }
        int X_in_Map = (int)source[0] + Map_Size[0];
        int Y_in_Map = (int)-source[1] + Map_Size[1] - 1;
        Debug.Log("x:"+X_in_Map.ToString() +" Y:"+ Y_in_Map.ToString());
        if (currentInput == "up")
        {
            if (!(Y_in_Map == 0 || block.Contains(map[Y_in_Map - 1, X_in_Map])))
            {
                ResetTrigger();
                Pac_Animator.SetTrigger("Up");
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z), 0.2f);
                source = destination;
                destination[1] = gameObject.transform.position.y + 1;
            }
        }
        if (currentInput == "down")
        {
            if (!(Y_in_Map == Map_Size[1] * 2 - 1 || block.Contains(map[Y_in_Map + 1, X_in_Map])))
            {
                ResetTrigger();
                Pac_Animator.SetTrigger("Down");
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z), 0.2f);
                source = destination;
                destination[1] = gameObject.transform.position.y - 1;
            }
        }
        if (currentInput == "left")
        {
            if (!(X_in_Map == 0 || block.Contains(map[Y_in_Map, X_in_Map - 1])))
            {
                ResetTrigger();
                Pac_Animator.SetTrigger("Left");
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z), 0.2f);
                source = destination;
                destination[0] = (int)gameObject.transform.position.x - 1;
            }
        }
        if (currentInput == "right")
        {
            if (!(X_in_Map == Map_Size[0] * 2 || block.Contains(map[Y_in_Map, X_in_Map + 1])))
            {
                ResetTrigger();
                Pac_Animator.SetTrigger("Right");
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z), 0.2f);
                source = destination;
                destination[0] = (int)gameObject.transform.position.x + 1;
            }
        }
    }

    public void ResetTrigger()
    {
        Pac_Animator.ResetTrigger("Up");
        Pac_Animator.ResetTrigger("Down");
        Pac_Animator.ResetTrigger("Left");
        Pac_Animator.ResetTrigger("Right");
        Pac_Animator.ResetTrigger("Die");
    }
}
