using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level_Generator : MonoBehaviour
{
    private int[,] levelMap={
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7}, 
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4}, 
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4}, 
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4}, 
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3}, 
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5}, 
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4}, 
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3}, 
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4}, 
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4}, 
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3}, 
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0}, 
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0}, 
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0}, 
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}, 
    }; 
    private int[,] map_list;
    public Camera Camera;
    public GameObject Life;
    public GameObject Pellet;
    public GameObject Power_Pellet;
    public GameObject Bonus_Pellet;
    public GameObject Inside_Corner;
    public GameObject Inside_Wall;
    public GameObject Outside_Corner;
    public GameObject Outside_Wall;
    public GameObject Junction;
    private int size_x;
    private int size_y;
    private ArrayList Pellet_Positions=new ArrayList();
    private ArrayList Power_Pellet_Positions=new ArrayList();

    private GameObject Pac_student_Show_Die_Animation;
    // Start is called before the first frame update

    void Start(){}

    void Awake()
    {
        size_x=levelMap.GetLength(1);
        size_y=levelMap.GetLength(0);
        map_list=new int[levelMap.GetLength(0)*2-1,levelMap.GetLength(1)*2];
        for(int j=0;j<size_y;j++){
            for (int i=0;i<size_x;i++){
                map_list[j,i]=levelMap[j,i];
                map_list[j,size_x*2-i-1]=levelMap[j,i];
                map_list[size_y*2-2-j,i]=levelMap[j,i];
                map_list[size_y*2-2-j,size_x*2-i-1]=levelMap[j,i];
            }
        }
        int size_X=map_list.GetLength(1);
        int size_Y=map_list.GetLength(0);
        Pac_student_Show_Die_Animation=GameObject.Find("Pac_Student(Show Die Animation)");
        ArrayList inside_corner_info=new ArrayList();
        ArrayList outside_corner_info=new ArrayList();
        GameObject block;
        GameObject mapObject;
        int[] neighbor;
        int[] neighbor1;
        int[] neighbor2;
        Camera.orthographicSize =size_x>size_y?size_x+2:size_y+2;
        GameObject life1=Instantiate(Life,new Vector3(-size_x+1,-size_y,0), Quaternion.identity,GameObject.Find("Indictors").transform);
        GameObject life2=Instantiate(Life,new Vector3(-size_x+3,-size_y,0), Quaternion.identity,GameObject.Find("Indictors").transform);
        GameObject life3=Instantiate(Life,new Vector3(-size_x+5,-size_y,0), Quaternion.identity,GameObject.Find("Indictors").transform);
        GameObject bonus=Instantiate(Bonus_Pellet,new Vector3(size_x-1.5f,-size_y,0), Quaternion.identity,GameObject.Find("Indictors").transform);
        bonus.transform.localScale=new Vector3(2f,2f,2f);

        for(int j=0;j<size_Y;j++){
            for (int i=0;i<size_X;i++){
                switch (map_list[j,i]){
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        mapObject=Outside_Wall;
                        neighbor = new int[3]{1,2,7};
                        if((i<size_X-1&&neighbor.Contains(map_list[j,i+1]))&&(i>0&&neighbor.Contains(map_list[j,i-1]))){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i+1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i+1);
                                outside_corner_info.Add(0);
                            }
                            if(map_list[j,i-1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i-1);
                                outside_corner_info.Add(1);
                            }
                        }
                        else if((j<size_Y-1&&neighbor.Contains(map_list[j+1,i]))&&(j>0&&neighbor.Contains(map_list[j-1,i]))){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j+1,i]==1){
                                outside_corner_info.Add(j+1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(3);
                            }
                            if(map_list[j-1,i]==1){
                                outside_corner_info.Add(j-1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(4);
                            }
                        }


                        else if(i==size_X-1){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i-1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i-1);
                                outside_corner_info.Add(1);
                            }
                        }
                        else if(i==0){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i+1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i+1);
                                outside_corner_info.Add(0);
                            }
                        }
                        else if(j==size_Y-1){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j-1,i]==1){
                                outside_corner_info.Add(j-1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(4);
                            }
                        }
                        else if(j==0){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j+1,i]==1){
                                outside_corner_info.Add(j+1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(3);
                            }
                        }


                        else if(i>0&&neighbor.Contains(map_list[j,i-1])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i-1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i-1);
                                outside_corner_info.Add(1);
                            }
                        }
                        else if(i<size_X-1&&neighbor.Contains(map_list[j,i+1])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i+1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i+1);
                                outside_corner_info.Add(0);
                            }
                        }
                        else if(j>0&&neighbor.Contains(map_list[j-1,i])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j-1,i]==1){
                                outside_corner_info.Add(j-1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(4);
                            }
                        }
                        else if(j<size_Y-1&&neighbor.Contains(map_list[j+1,i])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j+1,i]==1){
                                outside_corner_info.Add(j+1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(3);
                            }
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        mapObject=Inside_Wall;
                        neighbor = new int[3]{3,4,7};
                        if((i<size_X-1&&neighbor.Contains(map_list[j,i+1]))&&(i>0&&neighbor.Contains(map_list[j,i-1]))){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i+1]==3){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i+1);
                                inside_corner_info.Add(0);
                            }
                            if(map_list[j,i-1]==3){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i-1);
                                inside_corner_info.Add(1);
                            }
                        }
                        else if((j<size_Y-1&&neighbor.Contains(map_list[j+1,i]))&&(j>0&&neighbor.Contains(map_list[j-1,i]))){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j+1,i]==3){
                                inside_corner_info.Add(j+1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(3);
                            }
                            if(map_list[j-1,i]==3){
                                inside_corner_info.Add(j-1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(4);
                            }
                        }


                        else if(i==size_X-1){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i-1]==3){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i-1);
                                inside_corner_info.Add(1);
                            }
                        }
                        else if(i==0){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i+1]==3){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i+1);
                                inside_corner_info.Add(0);
                            }
                        }
                        else if(j==size_Y-1){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j-1,i]==3){
                                inside_corner_info.Add(j-1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(4);
                            }
                        }
                        else if(j==0){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j+1,i]==3){
                                inside_corner_info.Add(j+1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(3);
                            }
                        }


                        else if(i>0&&neighbor.Contains(map_list[j,i-1])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i-1]==3){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i-1);
                                inside_corner_info.Add(1);
                            }
                        }
                        else if(i<size_X-1&&neighbor.Contains(map_list[j,i+1])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            if(map_list[j,i+1]==3){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i+1);
                                inside_corner_info.Add(0);
                            }
                        }
                        else if(j>0&&neighbor.Contains(map_list[j-1,i])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j-1,i]==3){
                                inside_corner_info.Add(j-1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(4);
                            }
                        }
                        else if(j<size_Y-1&&neighbor.Contains(map_list[j+1,i])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(map_list[j+1,i]==3){
                                inside_corner_info.Add(j+1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(3);
                            }
                        }
                        break;
                    case 5:
                        Instantiate(Pellet,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Pellets").transform);
                        Instantiate(Pellet,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity,GameObject.Find("Pellets").transform);
                        if(j!=size_y-1){
                        Instantiate(Pellet,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity,GameObject.Find("Pellets").transform);
                        Instantiate(Pellet,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity,GameObject.Find("Pellets").transform);
                        }
                        break;
                    case 6:
                        Instantiate(Power_Pellet,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("PowerPellets").transform);
                        Instantiate(Power_Pellet,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity,GameObject.Find("PowerPellets").transform);
                        if(j!=size_y-1){
                        Instantiate(Power_Pellet,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity,GameObject.Find("PowerPellets").transform);
                        Instantiate(Power_Pellet,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity,GameObject.Find("PowerPellets").transform);
                        }
                        break;
                    case 7:
                        mapObject=Junction;
                        neighbor1 = new int[2]{1,2};
                        neighbor2 = new int[2]{3,4};
                        if(i<size_X-1&&j<size_Y-1&&((neighbor1.Contains(map_list[j,i+1])&&neighbor2.Contains(map_list[j+1,i]))||(neighbor2.Contains(map_list[j,i+1])&&neighbor1.Contains(map_list[j+1,i])))){
                            if(neighbor1.Contains(map_list[j,i+1])&&neighbor2.Contains(map_list[j+1,i])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.localScale=new Vector3(-block.transform.localScale.x,block.transform.localScale.y,block.transform.localScale.z);
                            }
                            if(neighbor2.Contains(map_list[j,i+1])&&neighbor1.Contains(map_list[j+1,i])){
                                block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                                block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                                }
                                
                        }
                        else if(i>0&&j<size_Y-1&&((neighbor1.Contains(map_list[j,i-1])&&neighbor2.Contains(map_list[j+1,i]))||(neighbor2.Contains(map_list[j,i-1])&&neighbor1.Contains(map_list[j+1,i])))){
                            if(neighbor1.Contains(map_list[j,i-1])&&neighbor2.Contains(map_list[j+1,i])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            }
                            if(neighbor2.Contains(map_list[j,i-1])&&neighbor1.Contains(map_list[j+1,i])){
                                block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                                block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                                block.transform.localScale=new Vector3(block.transform.localScale.x,-block.transform.localScale.y,block.transform.localScale.z);
                                }
                        }
                        else if(i>0&&j>0&&((neighbor1.Contains(map_list[j,i-1])&&neighbor2.Contains(map_list[j-1,i]))||(neighbor2.Contains(map_list[j,i-1])&&neighbor1.Contains(map_list[j-1,i])))){
                            if(neighbor1.Contains(map_list[j,i-1])&&neighbor2.Contains(map_list[j-1,i])){
                            block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                            block.transform.localScale=new Vector3(block.transform.localScale.x,-block.transform.localScale.y,block.transform.localScale.z);
                            }
                            if(neighbor2.Contains(map_list[j,i-1])&&neighbor1.Contains(map_list[j-1,i])){
                                block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                                block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                                }
                        }
                        else if(i<size_X-1&&j>0&&((neighbor1.Contains(map_list[j,i+1])&&neighbor2.Contains(map_list[j-1,i]))||(neighbor2.Contains(map_list[j,i+1])&&neighbor1.Contains(map_list[j-1,i])))){
                            if(neighbor1.Contains(map_list[j,i+1])&&neighbor2.Contains(map_list[j-1,i])){
                                block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                                block.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                                }
                            if(neighbor2.Contains(map_list[j,i+1])&&neighbor1.Contains(map_list[j-1,i])){
                                block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                                block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                                block.transform.localScale=new Vector3(-block.transform.localScale.x,block.transform.localScale.y,block.transform.localScale.z);
                                }
                        }
                        break;
                }
            }
        }
        
        ArrayList inside_corner_filled=new ArrayList();
        while(true){
            bool have_updates=false;
            for(int j=0;j<size_Y;j++){
                for (int i=0;i<size_X;i++){
                    
                    bool updates=false;
                    if(map_list[j,i]!=3)continue;
                    mapObject=Inside_Corner;
                    neighbor = new int[3]{3,4,7};
                    neighbor1 = new int[2]{3,7};

                    bool filled=false;
                    for(int x=0;x<inside_corner_filled.Count;x+=2){
                        if((int)inside_corner_filled[x]==j&&(int)inside_corner_filled[x+1]==i){
                            filled=true;
                            break;
                        }
                    }
                    if(filled)continue;

                    int l_r=-1;
                    int t_b=-1;
                    for(int x=0;x<inside_corner_info.Count;x+=3){
                        if((int)inside_corner_info[x]==j&&(int)inside_corner_info[x+1]==i){
                            if(((int)inside_corner_info[x+2])<2)l_r=(int)inside_corner_info[x+2];
                            else t_b=(int)inside_corner_info[x+2];
                        }
                    }
                    if(l_r==1&&t_b==4){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        updates=true;
                    }
                    else if(l_r==0&&t_b==4){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                        updates=true;
                    }
                    else if(l_r==0&&t_b==3){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                        updates=true;
                    }
                    else if(l_r==1&&t_b==3){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        updates=true;
                    }



                    else if(l_r==1&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i])&&(j==0||!neighbor1.Contains(map_list[j-1,i]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        if(map_list[j+1,i]==3){
                            inside_corner_info.Add(j+1);
                            inside_corner_info.Add(i);
                            inside_corner_info.Add(3);
                        }
                        updates=true;
                    }
                    else if(t_b==4&&i<size_X-1&&neighbor1.Contains(map_list[j,i+1])&&(i==0||!neighbor1.Contains(map_list[j,i-1]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        if(map_list[j,i+1]==3){
                            inside_corner_info.Add(j);
                            inside_corner_info.Add(i+1);
                            inside_corner_info.Add(0);
                        }
                        updates=true;
                    }
                    else if(l_r==0&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i])&&(j==0||!neighbor1.Contains(map_list[j-1,i]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                        if(map_list[j+1,i]==3){
                            inside_corner_info.Add(j+1);
                            inside_corner_info.Add(i);
                            inside_corner_info.Add(3);
                        }
                        updates=true;
                    }
                    else if(t_b==4&&i>0&&neighbor1.Contains(map_list[j,i-1])&&(i==size_X-1||!neighbor1.Contains(map_list[j,i+1]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                        if(map_list[j,i-1]==3){
                            inside_corner_info.Add(j);
                            inside_corner_info.Add(i-1);
                            inside_corner_info.Add(1);
                        }
                        updates=true;
                    }
                    else if(l_r==0&&j>0&&neighbor1.Contains(map_list[j-1,i])&&(j==size_Y-1||!neighbor1.Contains(map_list[j+1,i]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                        if(map_list[j-1,i]==3){
                            inside_corner_info.Add(j-1);
                            inside_corner_info.Add(i);
                            inside_corner_info.Add(4);
                        }
                        updates=true;
                    }
                    else if(t_b==3&&i>0&&neighbor1.Contains(map_list[j,i-1])&&(i==size_X-1||!neighbor1.Contains(map_list[j,i+1]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                        if(map_list[j,i-1]==3){
                            inside_corner_info.Add(j);
                            inside_corner_info.Add(i-1);
                            inside_corner_info.Add(1);
                        }
                        updates=true;
                    }
                    else if(l_r==1&&j>0&&neighbor1.Contains(map_list[j-1,i])&&(j==size_Y-1||!neighbor1.Contains(map_list[j+1,i]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        if(map_list[j-1,i]==3){
                            inside_corner_info.Add(j-1);
                            inside_corner_info.Add(i);
                            inside_corner_info.Add(4);
                        }
                        updates=true;
                    }
                    else if(t_b==3&&i<size_X-1&&neighbor1.Contains(map_list[j,i+1])&&(i==0||!neighbor1.Contains(map_list[j,i-1]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        if(map_list[j,i+1]==3){
                            inside_corner_info.Add(j);
                            inside_corner_info.Add(i+1);
                            inside_corner_info.Add(0);
                        }
                        updates=true;
                    }
                    if(updates){
                        inside_corner_filled.Add(j);
                        inside_corner_filled.Add(i);
                    }
                    if(!have_updates&&updates)have_updates=true;
                }
            }
            if(!have_updates)break;
        }



        for(int j=0;j<size_Y;j++){
            for (int i=0;i<size_X;i++){
                if(map_list[j,i]!=3)continue;
                mapObject=Inside_Corner;
                neighbor = new int[3]{3,4,7};
                neighbor1 = new int[2]{3,7};
                
                bool filled=false;
                for(int x=0;x<inside_corner_filled.Count;x+=2){
                    if((int)inside_corner_filled[x]==j&&(int)inside_corner_filled[x+1]==i)
                    {filled=true;
                    break;}
                }
                if(filled)continue;

                int l_r=-1;
                int t_b=-1;
                for(int x=0;x<inside_corner_info.Count;x+=3){
                    if((int)inside_corner_info[x]==j&&(int)inside_corner_info[x+1]==i){
                        if(((int)inside_corner_info[x+2])<2)l_r=(int)inside_corner_info[x+2];
                        else t_b=(int)inside_corner_info[x+2];
                    }
                }
                if(l_r==1&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    }
                else if(t_b==4&&i<size_X-1&&neighbor1.Contains(map_list[j,i+1])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    }
                else if(l_r==0&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    }
                else if(t_b==4&&i>0&&neighbor1.Contains(map_list[j,i-1])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    }
                else if(l_r==0&&j>0&&neighbor1.Contains(map_list[j-1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    }
                else if(t_b==3&&i>0&&neighbor1.Contains(map_list[j,i-1])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    }
                else if(l_r==1&&j>0&&neighbor1.Contains(map_list[j-1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }
                else if(t_b==3&&i<size_X-1&&neighbor1.Contains(map_list[j,i+1])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }

                else if((i==size_X-1&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i]))||(i<size_X-1&&j==size_Y-1&&neighbor1.Contains(map_list[j,i+1]))||(i==size_X-1&&j==size_Y-1)){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                }
                else if((i==0&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i]))||(i>0&&j==size_Y-1&&neighbor1.Contains(map_list[j,i-1]))||(i==0&&j==size_Y-1)){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
                else if((i==0&&j>0&&neighbor1.Contains(map_list[j-1,i]))||(i>0&&j==0&&neighbor1.Contains(map_list[j,i-1]))||(i==0&&j==0)){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                }
                else if((i==size_X-1&&j>0&&neighbor1.Contains(map_list[j-1,i]))||(i<size_X-1&&j==0&&neighbor1.Contains(map_list[j,i+1]))||(i==size_X-1&&j==0)){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }

                else if(i<size_X-1&&j<size_Y-1&&neighbor1.Contains(map_list[j,i+1])&&neighbor1.Contains(map_list[j+1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                }
                else if(i>0&&j<size_Y-1&&neighbor1.Contains(map_list[j,i-1])&&neighbor1.Contains(map_list[j+1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
                else if(i>0&&j>0&&neighbor1.Contains(map_list[j,i-1])&&neighbor1.Contains(map_list[j-1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                }
                else if(i<size_X-1&&j>0&&neighbor1.Contains(map_list[j,i+1])&&neighbor1.Contains(map_list[j-1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }

            }
        }
        ArrayList outside_corner_filled=new ArrayList();
        while(true){
            bool have_updates=false;
            for(int j=0;j<size_Y;j++){
                for (int i=0;i<size_X;i++){
                    
                    bool updates=false;
                    if(map_list[j,i]!=1)continue;
                    mapObject=Outside_Corner;
                    neighbor = new int[3]{1,2,7};
                    neighbor1 = new int[2]{1,7};

                    bool filled=false;
                    for(int x=0;x<outside_corner_filled.Count;x+=2){
                        if((int)outside_corner_filled[x]==j&&(int)outside_corner_filled[x+1]==i){
                            filled=true;
                            break;
                        }
                    }
                    if(filled)continue;

                    int l_r=-1;
                    int t_b=-1;
                    for(int x=0;x<outside_corner_info.Count;x+=3){
                        if((int)outside_corner_info[x]==j&&(int)outside_corner_info[x+1]==i){
                            if(((int)outside_corner_info[x+2])<2)l_r=(int)outside_corner_info[x+2];
                            else t_b=(int)outside_corner_info[x+2];
                        }
                    }
                    if(l_r==1&&t_b==4){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        updates=true;
                    }
                    else if(l_r==0&&t_b==4){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                        updates=true;
                    }
                    else if(l_r==0&&t_b==3){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                        updates=true;
                    }
                    else if(l_r==1&&t_b==3){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        updates=true;
                    }



                    else if(l_r==1&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i])&&(j==0||!neighbor1.Contains(map_list[j-1,i]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        if(map_list[j+1,i]==1){
                            outside_corner_info.Add(j+1);
                            outside_corner_info.Add(i);
                            outside_corner_info.Add(3);
                        }
                        updates=true;
                    }
                    else if(t_b==4&&i<size_X-1&&neighbor1.Contains(map_list[j,i+1])&&(i==0||!neighbor1.Contains(map_list[j,i-1]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        if(map_list[j,i+1]==1){
                            outside_corner_info.Add(j);
                            outside_corner_info.Add(i+1);
                            outside_corner_info.Add(0);
                        }
                        updates=true;
                    }
                    else if(l_r==0&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i])&&(j==0||!neighbor1.Contains(map_list[j-1,i]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                        if(map_list[j+1,i]==1){
                            outside_corner_info.Add(j+1);
                            outside_corner_info.Add(i);
                            outside_corner_info.Add(3);
                        }
                        updates=true;
                    }
                    else if(t_b==4&&i>0&&neighbor1.Contains(map_list[j,i-1])&&(i==size_X-1||!neighbor1.Contains(map_list[j,i+1]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                        if(map_list[j,i-1]==1){
                            outside_corner_info.Add(j);
                            outside_corner_info.Add(i-1);
                            outside_corner_info.Add(1);
                        }
                        updates=true;
                    }
                    else if(l_r==0&&j>0&&neighbor1.Contains(map_list[j-1,i])&&(j==size_Y-1||!neighbor1.Contains(map_list[j+1,i]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                        if(map_list[j-1,i]==1){
                            outside_corner_info.Add(j-1);
                            outside_corner_info.Add(i);
                            outside_corner_info.Add(4);
                        }
                        updates=true;
                    }
                    else if(t_b==3&&i>0&&neighbor1.Contains(map_list[j,i-1])&&(i==size_X-1||!neighbor1.Contains(map_list[j,i+1]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                        if(map_list[j,i-1]==1){
                            outside_corner_info.Add(j);
                            outside_corner_info.Add(i-1);
                            outside_corner_info.Add(1);
                        }
                        updates=true;
                    }
                    else if(l_r==1&&j>0&&neighbor1.Contains(map_list[j-1,i])&&(j==size_Y-1||!neighbor1.Contains(map_list[j+1,i]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        if(map_list[j-1,i]==1){
                            outside_corner_info.Add(j-1);
                            outside_corner_info.Add(i);
                            outside_corner_info.Add(4);
                        }
                        updates=true;
                    }
                    else if(t_b==3&&i<size_X-1&&neighbor1.Contains(map_list[j,i+1])&&(i==0||!neighbor1.Contains(map_list[j,i-1]))){
                        block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                        block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                        if(map_list[j,i+1]==1){
                            outside_corner_info.Add(j);
                            outside_corner_info.Add(i+1);
                            outside_corner_info.Add(0);
                        }
                        updates=true;
                    }
                    if(updates){
                        outside_corner_filled.Add(j);
                        outside_corner_filled.Add(i);
                    }
                    if(!have_updates&&updates)have_updates=true;
                }
            }
            if(!have_updates)break;
        }



        for(int j=0;j<size_Y;j++){
            for (int i=0;i<size_X;i++){
                if(map_list[j,i]!=1)continue;
                mapObject=Outside_Corner;
                neighbor = new int[3]{1,2,7};
                neighbor1 = new int[2]{1,7};
                
                bool filled=false;
                for(int x=0;x<outside_corner_filled.Count;x+=2){
                    if((int)outside_corner_filled[x]==j&&(int)outside_corner_filled[x+1]==i)
                    {filled=true;
                    break;}
                }
                if(filled)continue;

                int l_r=-1;
                int t_b=-1;
                for(int x=0;x<outside_corner_info.Count;x+=3){
                    if((int)outside_corner_info[x]==j&&(int)outside_corner_info[x+1]==i){
                        if(((int)outside_corner_info[x+2])<2)l_r=(int)outside_corner_info[x+2];
                        else t_b=(int)outside_corner_info[x+2];
                    }
                }
                if(l_r==1&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    }
                else if(t_b==4&&i<size_X-1&&neighbor1.Contains(map_list[j,i+1])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    }
                else if(l_r==0&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    }
                else if(t_b==4&&i>0&&neighbor1.Contains(map_list[j,i-1])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    }
                else if(l_r==0&&j>0&&neighbor1.Contains(map_list[j-1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    }
                else if(t_b==3&&i>0&&neighbor1.Contains(map_list[j,i-1])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    }
                else if(l_r==1&&j>0&&neighbor1.Contains(map_list[j-1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }
                else if(t_b==3&&i<size_X-1&&neighbor1.Contains(map_list[j,i+1])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }

                else if((i==size_X-1&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i]))||(i<size_X-1&&j==size_Y-1&&neighbor1.Contains(map_list[j,i+1]))||(i==size_X-1&&j==size_Y-1)){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                }
                else if((i==0&&j<size_Y-1&&neighbor1.Contains(map_list[j+1,i]))||(i>0&&j==size_Y-1&&neighbor1.Contains(map_list[j,i-1]))||(i==0&&j==size_Y-1)){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
                else if((i==0&&j>0&&neighbor1.Contains(map_list[j-1,i]))||(i>0&&j==0&&neighbor1.Contains(map_list[j,i-1]))||(i==0&&j==0)){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                }
                else if((i==size_X-1&&j>0&&neighbor1.Contains(map_list[j-1,i]))||(i<size_X-1&&j==0&&neighbor1.Contains(map_list[j,i+1]))||(i==size_X-1&&j==0)){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }

                else if(i<size_X-1&&j<size_Y-1&&neighbor1.Contains(map_list[j,i+1])&&neighbor1.Contains(map_list[j+1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                }
                else if(i>0&&j<size_Y-1&&neighbor1.Contains(map_list[j,i-1])&&neighbor1.Contains(map_list[j+1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
                else if(i>0&&j>0&&neighbor1.Contains(map_list[j,i-1])&&neighbor1.Contains(map_list[j-1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                }
                else if(i<size_X-1&&j>0&&neighbor1.Contains(map_list[j,i+1])&&neighbor1.Contains(map_list[j-1,i])){
                    block=Instantiate(mapObject,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity,GameObject.Find("Map").transform);
                    block.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }

            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        Pac_student_Show_Die_Animation.GetComponent<Animator>().SetTrigger("Up");
        Pac_student_Show_Die_Animation.GetComponent<Animator>().SetTrigger("Die");
        Pac_student_Show_Die_Animation.GetComponent<Animator>().SetTrigger("Exit");
    }

    public int[] Get_Size(){
        return new int[2]{levelMap.GetLength(1),levelMap.GetLength(0)};
    }

    public int[,] Get_Map(){
        return map_list;
    }
}
