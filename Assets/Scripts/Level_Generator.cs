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
    public GameObject Pac_student;
    public GameObject Blue_Ghost;
    public GameObject Gree_Ghost;
    public GameObject Pink_Ghost;
    public GameObject Red_Ghost;
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
    // Start is called before the first frame update
    void Start()
    {
        ArrayList inside_corner_info=new ArrayList();
        ArrayList outside_corner_info=new ArrayList();
        GameObject left_top;
        GameObject right_top;
        GameObject left_bottom;
        GameObject right_bottom;
        GameObject map_object;
        int[] neighbor;
        int[] neighbor1;
        int[] neighbor2;
        size_x=levelMap.GetLength(1);
        size_y=levelMap.GetLength(0);
        for(int j=0;j<size_y;j++){
            for (int i=0;i<size_x;i++){
                switch (levelMap[j,i]){
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        map_object=Outside_Wall;
                        neighbor = new int[3]{1,2,7};
                        if((i<size_x-1&&neighbor.Contains(levelMap[j,i+1]))&&(i>0&&neighbor.Contains(levelMap[j,i-1]))){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i+1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i+1);
                                outside_corner_info.Add(0);
                            }
                            if(levelMap[j,i-1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i-1);
                                outside_corner_info.Add(1);
                            }
                        }
                        else if((j<size_y-1&&neighbor.Contains(levelMap[j+1,i]))&&(j>0&&neighbor.Contains(levelMap[j-1,i]))){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j+1,i]==1){
                                outside_corner_info.Add(j+1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(3);
                            }
                            if(levelMap[j-1,i]==1){
                                outside_corner_info.Add(j-1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(4);
                            }
                        }
                        else if(i>0&&neighbor.Contains(levelMap[j,i-1])){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i-1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i-1);
                                outside_corner_info.Add(1);
                            }
                        }
                        else if(i<size_x-1&&neighbor.Contains(levelMap[j,i+1])){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i+1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i+1);
                                outside_corner_info.Add(0);
                            }
                        }
                        else if(j>0&&neighbor.Contains(levelMap[j-1,i])){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j-1,i]==1){
                                outside_corner_info.Add(j-1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(4);
                            }
                        }
                        else if(j<size_y-1&&neighbor.Contains(levelMap[j+1,i])){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j-1,i]==1){
                                outside_corner_info.Add(j+1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(3);
                            }
                        }
                        else if(i==size_x-1){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i-1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i-1);
                                outside_corner_info.Add(1);
                            }
                        }
                        else if(i==0){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i+1]==1){
                                outside_corner_info.Add(j);
                                outside_corner_info.Add(i+1);
                                outside_corner_info.Add(0);
                            }
                        }
                        else if(j==size_y-1){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j-1,i]==1){
                                outside_corner_info.Add(j-1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(4);
                            }
                        }
                        else if(j==0){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j-1,i]==1){
                                outside_corner_info.Add(j+1);
                                outside_corner_info.Add(i);
                                outside_corner_info.Add(3);
                            }
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        map_object=Inside_Wall;
                        neighbor = new int[3]{3,4,7};
                        if((i<size_x-1&&neighbor.Contains(levelMap[j,i+1]))&&(i>0&&neighbor.Contains(levelMap[j,i-1]))){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i+1]==3){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i+1);
                                inside_corner_info.Add(0);
                            }
                            if(levelMap[j,i-1]==3){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i-1);
                                inside_corner_info.Add(1);
                            }
                        }
                        else if((j<size_y-1&&neighbor.Contains(levelMap[j+1,i]))&&(j>0&&neighbor.Contains(levelMap[j-1,i]))){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j+1,i]==3){
                                inside_corner_info.Add(j+1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(3);
                            }
                            if(levelMap[j-1,i]==3){
                                inside_corner_info.Add(j-1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(4);
                            }
                        }
                        else if(i>0&&neighbor.Contains(levelMap[j,i-1])){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i-1]==1){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i-1);
                                inside_corner_info.Add(1);
                            }
                        }
                        else if(i<size_x-1&&neighbor.Contains(levelMap[j,i+1])){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i+1]==1){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i+1);
                                inside_corner_info.Add(0);
                            }
                        }
                        else if(j>0&&neighbor.Contains(levelMap[j-1,i])){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j-1,i]==1){
                                inside_corner_info.Add(j-1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(4);
                            }
                        }
                        else if(j<size_y-1&&neighbor.Contains(levelMap[j+1,i])){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j-1,i]==1){
                                inside_corner_info.Add(j+1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(3);
                            }
                        }
                        else if(i==size_x-1){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i-1]==1){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i-1);
                                inside_corner_info.Add(1);
                            }
                        }
                        else if(i==0){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            if(levelMap[j,i+1]==1){
                                inside_corner_info.Add(j);
                                inside_corner_info.Add(i+1);
                                inside_corner_info.Add(0);
                            }
                        }
                        else if(j==size_y-1){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j-1,i]==1){
                                inside_corner_info.Add(j-1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(4);
                            }
                        }
                        else if(j==0){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                            right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                            left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                            right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            if(levelMap[j-1,i]==1){
                                inside_corner_info.Add(j+1);
                                inside_corner_info.Add(i);
                                inside_corner_info.Add(3);
                            }
                        }
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        map_object=Junction;
                        neighbor1 = new int[2]{1,2};
                        neighbor2 = new int[2]{3,4};
                        if(i<size_x-1&&j<size_y-1&&((neighbor1.Contains(levelMap[j,i+1])&&neighbor2.Contains(levelMap[j+1,i]))||(neighbor2.Contains(levelMap[j,i+1])&&neighbor1.Contains(levelMap[j+1,i])))){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            if(neighbor1.Contains(levelMap[j,i+1])&&neighbor2.Contains(levelMap[j+1,i]))left_top.transform.localScale=new Vector3(-left_top.transform.localScale.x,left_top.transform.localScale.y,left_top.transform.localScale.z);
                            if(neighbor2.Contains(levelMap[j,i+1])&&neighbor1.Contains(levelMap[j+1,i]))left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), left_top.transform.rotation);
                            right_top.transform.localScale=new Vector3(-left_top.transform.localScale.x,left_top.transform.localScale.y,left_top.transform.localScale.z);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), left_top.transform.rotation);
                            left_bottom.transform.localScale=new Vector3(left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), left_top.transform.rotation);
                            right_bottom.transform.localScale=new Vector3(-left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                        }
                        else if(i>0&&j<size_y-1&&((neighbor1.Contains(levelMap[j,i-1])&&neighbor2.Contains(levelMap[j+1,i]))||(neighbor2.Contains(levelMap[j,i-1])&&neighbor1.Contains(levelMap[j+1,i])))){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            if(neighbor1.Contains(levelMap[j,i-1])&&neighbor2.Contains(levelMap[j+1,i])){};
                            if(neighbor2.Contains(levelMap[j,i-1])&&neighbor1.Contains(levelMap[j+1,i])){
                                left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                                left_top.transform.localScale=new Vector3(left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                                }
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), left_top.transform.rotation);
                            right_top.transform.localScale=new Vector3(-left_top.transform.localScale.x,left_top.transform.localScale.y,left_top.transform.localScale.z);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), left_top.transform.rotation);
                            left_bottom.transform.localScale=new Vector3(left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), left_top.transform.rotation);
                            right_bottom.transform.localScale=new Vector3(-left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                        }
                        else if(i>0&&j>0&&((neighbor1.Contains(levelMap[j,i-1])&&neighbor2.Contains(levelMap[j-1,i]))||(neighbor2.Contains(levelMap[j,i-1])&&neighbor1.Contains(levelMap[j-1,i])))){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            if(neighbor1.Contains(levelMap[j,i-1])&&neighbor2.Contains(levelMap[j-1,i]))left_top.transform.localScale=new Vector3(left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                            if(neighbor2.Contains(levelMap[j,i-1])&&neighbor1.Contains(levelMap[j-1,i]))left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), left_top.transform.rotation);
                            right_top.transform.localScale=new Vector3(-left_top.transform.localScale.x,left_top.transform.localScale.y,left_top.transform.localScale.z);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), left_top.transform.rotation);
                            left_bottom.transform.localScale=new Vector3(left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), left_top.transform.rotation);
                            right_bottom.transform.localScale=new Vector3(-left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                        }
                        else if(i<size_x-1&&j>0&&((neighbor1.Contains(levelMap[j,i+1])&&neighbor2.Contains(levelMap[j-1,i]))||(neighbor2.Contains(levelMap[j,i+1])&&neighbor1.Contains(levelMap[j-1,i])))){
                            left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                            if(neighbor1.Contains(levelMap[j,i+1])&&neighbor2.Contains(levelMap[j-1,i]))left_top.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                            if(neighbor2.Contains(levelMap[j,i+1])&&neighbor1.Contains(levelMap[j-1,i])){
                                left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                                left_top.transform.localScale=new Vector3(-left_top.transform.localScale.x,left_top.transform.localScale.y,left_top.transform.localScale.z);
                                }
                            right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), left_top.transform.rotation);
                            right_top.transform.localScale=new Vector3(-left_top.transform.localScale.x,left_top.transform.localScale.y,left_top.transform.localScale.z);
                            left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), left_top.transform.rotation);
                            left_bottom.transform.localScale=new Vector3(left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                            right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), left_top.transform.rotation);
                            right_bottom.transform.localScale=new Vector3(-left_top.transform.localScale.x,-left_top.transform.localScale.y,left_top.transform.localScale.z);
                        }
                        break;
                }
            }
        }
        
        for(int j=0;j<size_y;j++){
            for (int i=0;i<size_x;i++){
                if(levelMap[j,i]!=3)continue;
                map_object=Inside_Corner;
                neighbor = new int[3]{3,4,7};

                int l_r=-1;
                int t_b=-1;
                for(int x=0;x<inside_corner_info.Count;x+=3){
                    if((int)inside_corner_info[x]==j&&(int)inside_corner_info[x+1]==i){
                        if(((int)inside_corner_info[x+2])<2)l_r=(int)inside_corner_info[x+2];
                        else t_b=(int)inside_corner_info[x+2];
                    }
                }
                if(l_r==1&&t_b==4){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);}
                else if(l_r==0&&t_b==4){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);}
                else if(l_r==0&&t_b==3){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);}
                else if(l_r==1&&t_b==3){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);}



                else if(i<size_x-1&&j<size_y-1&&neighbor.Contains(levelMap[j,i+1])&&neighbor.Contains(levelMap[j+1,i])){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                }
                else if(i>0&&j<size_y-1&&neighbor.Contains(levelMap[j,i-1])&&neighbor.Contains(levelMap[j+1,i])){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }
                else if(i>0&&j>0&&neighbor.Contains(levelMap[j,i-1])&&neighbor.Contains(levelMap[j-1,i])){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                }
                else if(i<size_x-1&&j>0&&neighbor.Contains(levelMap[j,i+1])&&neighbor.Contains(levelMap[j-1,i])){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
                else if((i==size_x-1&&j<size_y-1&&neighbor.Contains(levelMap[j+1,i]))||(i<size_x-1&&j==size_y-1&&neighbor.Contains(levelMap[j,i+1]))||(i==size_x-1&&j==size_y-1)){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                }
                else if((i==0&&j<size_y-1&&neighbor.Contains(levelMap[j+1,i]))||(i>0&&j==size_y-1&&neighbor.Contains(levelMap[j,i-1]))||(i==0&&j==size_y-1)){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }
                else if((i==0&&j>0&&neighbor.Contains(levelMap[j-1,i]))||(i>0&&j==0&&neighbor.Contains(levelMap[j,i-1]))||(i==0&&j==0)){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                }
                else if((i==size_x-1&&j>0&&neighbor.Contains(levelMap[j-1,i]))||(i<size_x-1&&j==0&&neighbor.Contains(levelMap[j,i+1]))||(i==size_x-1&&j==0)){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
            }
        }

        for(int j=0;j<size_y;j++){
            for (int i=0;i<size_x;i++){
                if(levelMap[j,i]!=1)continue;
                map_object=Outside_Corner;
                neighbor = new int[3]{1,2,7};

                int l_r=-1;
                int t_b=-1;
                for(int x=0;x<outside_corner_info.Count;x+=3){
                    if((int)outside_corner_info[x]==j&&(int)outside_corner_info[x+1]==i){
                        if(((int)outside_corner_info[x+2])<2)l_r=(int)outside_corner_info[x+2];
                        else t_b=(int)outside_corner_info[x+2];
                    }
                }
                if(l_r==1&&t_b==4){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);}
                else if(l_r==0&&t_b==4){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);}
                else if(l_r==0&&t_b==3){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);}
                else if(l_r==1&&t_b==3){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);}



                else if(i<size_x-1&&j<size_y-1&&neighbor.Contains(levelMap[j,i+1])&&neighbor.Contains(levelMap[j+1,i])){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                }
                else if(i>0&&j<size_y-1&&neighbor.Contains(levelMap[j,i-1])&&neighbor.Contains(levelMap[j+1,i])){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }
                else if(i>0&&j>0&&neighbor.Contains(levelMap[j,i-1])&&neighbor.Contains(levelMap[j-1,i])){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                }
                else if(i<size_x-1&&j>0&&neighbor.Contains(levelMap[j,i+1])&&neighbor.Contains(levelMap[j-1,i])){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
                else if((i==size_x-1&&j<size_y-1&&neighbor.Contains(levelMap[j+1,i]))||(i<size_x-1&&j==size_y-1&&neighbor.Contains(levelMap[j,i+1]))||(i==size_x-1&&j==size_y-1)){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                }
                else if((i==0&&j<size_y-1&&neighbor.Contains(levelMap[j+1,i]))||(i>0&&j==size_y-1&&neighbor.Contains(levelMap[j,i-1]))||(i==0&&j==size_y-1)){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                }
                else if((i==0&&j>0&&neighbor.Contains(levelMap[j-1,i]))||(i>0&&j==0&&neighbor.Contains(levelMap[j,i-1]))||(i==0&&j==0)){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                }
                else if((i==size_x-1&&j>0&&neighbor.Contains(levelMap[j-1,i]))||(i<size_x-1&&j==0&&neighbor.Contains(levelMap[j,i+1]))||(i==size_x-1&&j==0)){
                    left_top=Instantiate(map_object,new Vector3(i-size_x,size_y-j-1,0), Quaternion.identity);
                    left_top.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
                    right_top=Instantiate(map_object,new Vector3(size_x-i-1,size_y-j-1,0), Quaternion.identity);
                    right_top.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
                    left_bottom=Instantiate(map_object,new Vector3(i-size_x,j-size_y+1,0), Quaternion.identity);
                    left_bottom.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
                    right_bottom=Instantiate(map_object,new Vector3(size_x-i-1,j-size_y+1,0), Quaternion.identity);
                    right_bottom.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
                }
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
