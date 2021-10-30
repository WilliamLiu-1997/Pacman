using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PacStudentController : MonoBehaviour
{
    Animator Pac_Animator;
    public AudioSource Moving_Sound;
    public AudioSource Hit_Sound;
    public AudioSource Eat_Sound;
    public ParticleSystem dust;
    public ParticleSystem hit;
    public Tweener tweener;
    public Tweener tweener_bonus;
    private int[] Map_Size;
    private LevelGenerator[] LevelGenerator;
    public GameObject Bonus_Pellet;
    public GameObject Score;
    public GameObject OverallTime;
    public GameObject StartTime;
    public GameObject ScaredTime;
    public GameObject Red;
    public GameObject Pink;
    public GameObject Green;
    public GameObject Blue;
    public GameObject Bonus_Pellet_Instance;
    public GameObject Background;
    public int[] state;

    public GameObject[] lifes;
    public float dyingTime;
    public bool Started;
    public float continueTime;
    public float lastTime;
    public float allTime;
    public float scareTime;
    private int[,] map;
    private string lastInput;
    private string currentInput;
    private float[] destination;
    private float[] source;
    private float deltatime;
    private bool gameover;
    private float[] ghost_die_time;
    private float endtime;
    private int pellets_num;

    void awake()
    {
    }
    void Start()
    {
        Pac_Animator = GetComponent<Animator>();
        LevelGenerator = GameObject.Find("Map").GetComponents<LevelGenerator>();
        Map_Size = LevelGenerator[0].Get_Size();
        map = LevelGenerator[0].Get_Map();
        if (!(dyingTime == 4))
        {
            gameObject.transform.position = new Vector3(-Map_Size[0] + 1, Map_Size[1] - 2, 0);
            destination = new float[2] { gameObject.transform.position.x, gameObject.transform.position.y };
            source = new float[2] { gameObject.transform.position.x, gameObject.transform.position.y };
            //Score.GetComponent<Text>().text = GameObject.Find("UserPrefrabs").GetComponent<Text>().text.Split(char.Parse(" "))[0];
            scareTime = 0;
            ghost_die_time = new float[4] { 0, 0, 0, 0 };
            state = new int[4] { 0, 0, 0, 0 };
            pellets_num = 0;
        }
        dust.Stop();
        hit.Stop();
        deltatime = 10;
        Started = false;
        Background.GetComponent<Background_Music_Controller>().Started = false;
        continueTime = 0;
        allTime = 0;
        dyingTime = 0;
        gameover = false;
        lifes = new GameObject[3]{
            GameObject.Find("life3"),
            GameObject.Find("life2"),
            GameObject.Find("life1"),
        };
        endtime = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (endtime != 10 && endtime > 0)
        {
            endtime -= Time.deltaTime;
        }
        if (endtime != 10 && endtime <= 0)
        {
            GameObject.Find("ExitButton").GetComponent<UIManager>().LoadStartLevel();
        }
        if (dyingTime > 0)
        {
            dyingTime -= Time.deltaTime;
            continueTime = 0;
            if (dyingTime <= 0)
            {
                gameObject.transform.position = new Vector3(-Map_Size[0] + 1, Map_Size[1] - 2, 0);
                destination = new float[2] { gameObject.transform.position.x, gameObject.transform.position.y };
                source = new float[2] { gameObject.transform.position.x, gameObject.transform.position.y };
                lastInput = null;
                currentInput = null;
            }
        }
        else if (!gameover)
        {
            continueTime += Time.deltaTime;
        }
        if (continueTime > 0 && continueTime < 1) { StartTime.GetComponent<Text>().text = "3"; }
        else if (continueTime >= 1 && continueTime < 2) { StartTime.GetComponent<Text>().text = "2"; }
        else if (continueTime >= 2 && continueTime < 3) { StartTime.GetComponent<Text>().text = "1"; }
        else if (continueTime >= 3 && continueTime < 4) { StartTime.GetComponent<Text>().text = "GO!"; }
        else if (continueTime >= 4 && continueTime < 5) { StartTime.GetComponent<Text>().text = ""; }
        if (!Started && continueTime >= 4)
        {
            Started = true;
            Background.GetComponent<Background_Music_Controller>().Started = true;
        }
        if (Started)
        {
            if (!Moving_Sound.isPlaying) Moving_Sound.Play();
            GetInput();
            Move();
            BonusPellet();
            increaseTime();
            GhostState();
            CheckDie();
        }
    }

    void CheckDie()
    {
        Debug.Log(pellets_num);
        if (pellets_num == 216)
        {
            Destroy(lifes[2]);
            StartTime.GetComponent<Text>().text = "Game Over";
            endtime = 3;
            Started = false;
            Background.GetComponent<Background_Music_Controller>().Started = false;
            Pac_Animator.SetTrigger("Die");
            Background.GetComponent<Background_Music_Controller>().playDie();
            Moving_Sound.Stop();
            dust.Stop();
            tweener.DeleteTween();
            tweener_bonus.DeleteTween();
            gameover = true;
            continueTime = 0;
            if (int.Parse(Score.GetComponent<Text>().text) > int.Parse(GameObject.Find("UserPrefrabs").GetComponent<Text>().text.Split(char.Parse(" "))[0]))
            {
                GameObject.Find("UserPrefrabs").GetComponent<Text>().text = Score.GetComponent<Text>().text + " " + OverallTime.GetComponent<Text>().text;
            }
            return;
        }
        GameObject[] Ghosts = new GameObject[4] { Red, Pink, Green, Blue };
        for (int i = 0; i < 4; i++)
        {
            if (state[i] == 0 && Ghosts[i] != null && ((Ghosts[i].transform.position.x - gameObject.transform.position.x) * (Ghosts[i].transform.position.x - gameObject.transform.position.x) + (Ghosts[i].transform.position.y - gameObject.transform.position.y) * (Ghosts[i].transform.position.y - gameObject.transform.position.y)) < 0.3)
            {
                if (lifes[0] != null)
                {
                    Destroy(lifes[0]);
                }
                else if (lifes[1] != null)
                {
                    Destroy(lifes[1]);
                }
                else if (lifes[2] != null)
                {
                    Destroy(lifes[2]);
                    StartTime.GetComponent<Text>().text = "Game Over";
                    endtime = 3;
                    Started = false;
                    Background.GetComponent<Background_Music_Controller>().Started = false;
                    Pac_Animator.SetTrigger("Die");
                    Background.GetComponent<Background_Music_Controller>().playDie();
                    Moving_Sound.Stop();
                    dust.Stop();
                    tweener.DeleteTween();
                    tweener_bonus.DeleteTween();
                    gameover = true;
                    continueTime = 0;
                    if (int.Parse(Score.GetComponent<Text>().text) > int.Parse(GameObject.Find("UserPrefrabs").GetComponent<Text>().text.Split(char.Parse(" "))[0]))
                    {
                        GameObject.Find("UserPrefrabs").GetComponent<Text>().text = Score.GetComponent<Text>().text + " " + OverallTime.GetComponent<Text>().text;
                    }
                    return;
                }
                Pac_Animator.SetTrigger("Die");
                Background.GetComponent<Background_Music_Controller>().playDie();
                Moving_Sound.Stop();
                dust.Stop();
                dyingTime = 4;
                float t = allTime;
                tweener.DeleteTween();
                tweener_bonus.DeleteTween();
                Start();
                allTime = t;
                dyingTime = 4;
            }
            if ((state[i] == 1 || state[i] == 2) && Ghosts[i] != null && ((Ghosts[i].transform.position.x - gameObject.transform.position.x) * (Ghosts[i].transform.position.x - gameObject.transform.position.x) + (Ghosts[i].transform.position.y - gameObject.transform.position.y) * (Ghosts[i].transform.position.y - gameObject.transform.position.y)) < 0.5)
            {
                state[i] = 3;
                ghost_die_time[i] = 5;
                Background.GetComponent<Background_Music_Controller>().playDeadGhost(i);
                Score.GetComponent<Text>().text = (int.Parse(Score.GetComponent<Text>().text) + 300).ToString();
            }
        }
    }

    void GhostState()
    {
        if (scareTime > 0)
        {
            scareTime -= Time.deltaTime;
        }
        for (int i = 0; i < 4; i++)
        {
            if (ghost_die_time[i] > 0)
            {
                ghost_die_time[i] -= Time.deltaTime;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (state[i] == 0 && scareTime > 3)
            {
                Background.GetComponent<Background_Music_Controller>().playScaredGhost(i);
                state[i] = 1;
            }
            else if (state[i] == 1 && scareTime <= 3)
            {
                Background.GetComponent<Background_Music_Controller>().playRecoverGhost(i);
                state[i] = 2;
            }
            else if (state[i] == 2 && scareTime <= 0)
            {
                Background.GetComponent<Background_Music_Controller>().playNormalGhost(i);
                scareTime = 0;
                state[i] = 0;
            };
        }
        for (int i = 0; i < 4; i++)
        {
            if (state[i] == 3 && ghost_die_time[i] > 0)
            {
                Background.GetComponent<Background_Music_Controller>().resetGhost(i);
                Background.GetComponent<Background_Music_Controller>().playDeadGhost(i);
            }
            if (state[i] == 3 && ghost_die_time[i] <= 0)
            {
                state[i] = 0;
                ghost_die_time[i] = 0;
                Background.GetComponent<Background_Music_Controller>().resetGhost(i);
                Background.GetComponent<Background_Music_Controller>().playNormalGhost(i);
            }
        }
        if (scareTime > 0)
        {
            ScaredTime.GetComponent<Text>().text = ((int)scareTime + 1).ToString();
        }
        else
        {
            ScaredTime.GetComponent<Text>().text = "";
        }
    }

    void increaseTime()
    {
        allTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(allTime);
        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        OverallTime.GetComponent<Text>().text = timeText;
    }

    public void BonusPellet()
    {
        deltatime += Time.deltaTime;
        if (deltatime > 10)
        {
            deltatime = 0;
            int ran = UnityEngine.Random.Range(0, 4);
            float x = int.MaxValue;
            float y = int.MaxValue;
            if (ran == 0)
            {
                x = -(map.GetLength(1) + 4) / 2;
                y = UnityEngine.Random.Range(-(map.GetLength(0) + 4), (map.GetLength(0) + 4));
            }
            if (ran == 1)
            {
                x = (map.GetLength(1) + 4) / 2;
                y = UnityEngine.Random.Range(-(map.GetLength(0) + 4), (map.GetLength(0) + 4));
            }
            if (ran == 2)
            {
                x = UnityEngine.Random.Range(-(map.GetLength(1) + 4), (map.GetLength(1) + 4));
                y = -(map.GetLength(0) + 4) / 2;
            }
            if (ran == 3)
            {
                x = UnityEngine.Random.Range(-(map.GetLength(1) + 4), (map.GetLength(1) + 4));
                y = (map.GetLength(0) + 4) / 2;
            }
            if (Bonus_Pellet_Instance != null)
            {
                Destroy(Bonus_Pellet_Instance);
                Bonus_Pellet_Instance = null;
            }
            Bonus_Pellet_Instance = Instantiate(Bonus_Pellet, new Vector3(x, y, 0), Quaternion.identity, GameObject.Find("Map").transform);
            if (Bonus_Pellet_Instance != null)
            {
                tweener_bonus.AddTween(Bonus_Pellet_Instance.transform, Bonus_Pellet_Instance.transform.position, new Vector3(-x, -y, 10), 10f);
            }
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
            if (!(Y_in_Map == Map_Size[1] * 2 - 2 || block.Contains(map[Y_in_Map + 1, X_in_Map]))) { currentInput = lastInput; }
        }
        if (lastInput == "left")
        {
            if (!(X_in_Map == 0 || block.Contains(map[Y_in_Map, X_in_Map - 1]))) { currentInput = lastInput; }
        }
        if (lastInput == "right")
        {
            if (!(X_in_Map == Map_Size[0] * 2 - 1 || block.Contains(map[Y_in_Map, X_in_Map + 1]))) { currentInput = lastInput; }
        }
    }

    public void Move()
    {
        int[] block = new int[5] { 1, 2, 3, 4, 7 };
        int[] pellets = new int[2] { 5, 6 };
        float X = gameObject.transform.position.x;
        float Y = gameObject.transform.position.y;
        if (X != destination[0] || Y != destination[1]) { return; }
        int X_in_Map = (int)source[0] + Map_Size[0];
        int Y_in_Map = (int)-source[1] + Map_Size[1] - 1;

        bool canMove = false;

        GameObject eaten_pellet = GameObject.Find((X_in_Map).ToString() + "_" + (Y_in_Map).ToString());
        if (pellets.Contains(map[Y_in_Map, X_in_Map]) && eaten_pellet != null)
        {
            Eat_Sound.Play();
            Destroy(eaten_pellet);
            if (map[Y_in_Map, X_in_Map] == 5) Score.GetComponent<Text>().text = (int.Parse(Score.GetComponent<Text>().text) + 10).ToString();
        }

        if (Bonus_Pellet_Instance != null && ((Bonus_Pellet_Instance.transform.position.x - gameObject.transform.position.x) * (Bonus_Pellet_Instance.transform.position.x - gameObject.transform.position.x) + (Bonus_Pellet_Instance.transform.position.y - gameObject.transform.position.y) * (Bonus_Pellet_Instance.transform.position.y - gameObject.transform.position.y)) < 2)
        {
            Eat_Sound.Play();
            DestroyImmediate(Bonus_Pellet_Instance);
            Bonus_Pellet_Instance = null;
            tweener_bonus.DeleteTween();
            Score.GetComponent<Text>().text = (int.Parse(Score.GetComponent<Text>().text) + 100).ToString();
        }
        if (currentInput == "up")
        {
            if (!(Y_in_Map == 0 || block.Contains(map[Y_in_Map - 1, X_in_Map])))
            {
                ResetTrigger();
                Pac_Animator.SetTrigger("Up");
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z), 0.2f);
                source = destination;
                destination[1] = gameObject.transform.position.y + 1;
                canMove = true;
                eaten_pellet = GameObject.Find((X_in_Map).ToString() + "_" + (Y_in_Map - 1).ToString());
                if (pellets.Contains(map[Y_in_Map - 1, X_in_Map]) && eaten_pellet != null)
                {
                    Eat_Sound.Play();
                    Destroy(eaten_pellet);
                    if (map[Y_in_Map - 1, X_in_Map] == 5)
                    {
                        pellets_num += 1;
                        Score.GetComponent<Text>().text = (int.Parse(Score.GetComponent<Text>().text) + 10).ToString();
                    }
                    if (map[Y_in_Map - 1, X_in_Map] == 6)
                    {
                        scareTime = 10;
                    }
                }
            }
        }
        if (currentInput == "down")
        {
            if (!(Y_in_Map == Map_Size[1] * 2 - 2 || block.Contains(map[Y_in_Map + 1, X_in_Map])))
            {
                ResetTrigger();
                Pac_Animator.SetTrigger("Down");
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z), 0.2f);
                source = destination;
                destination[1] = gameObject.transform.position.y - 1;
                canMove = true;
                eaten_pellet = GameObject.Find((X_in_Map).ToString() + "_" + (Y_in_Map + 1).ToString());
                if (pellets.Contains(map[Y_in_Map + 1, X_in_Map]) && eaten_pellet != null)
                {
                    Eat_Sound.Play();
                    Destroy(eaten_pellet);
                    if (map[Y_in_Map + 1, X_in_Map] == 5)
                    {
                        pellets_num += 1;
                        Score.GetComponent<Text>().text = (int.Parse(Score.GetComponent<Text>().text) + 10).ToString();
                    }
                    if (map[Y_in_Map + 1, X_in_Map] == 6)
                    {
                        scareTime = 10;
                    }
                }
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
                canMove = true;
                eaten_pellet = GameObject.Find((X_in_Map - 1).ToString() + "_" + (Y_in_Map).ToString());
                if (pellets.Contains(map[Y_in_Map, X_in_Map - 1]) && eaten_pellet != null)
                {
                    Eat_Sound.Play();
                    Destroy(eaten_pellet);
                    if (map[Y_in_Map, X_in_Map - 1] == 5)
                    {
                        pellets_num += 1;
                        Score.GetComponent<Text>().text = (int.Parse(Score.GetComponent<Text>().text) + 10).ToString();
                    }
                    if (map[Y_in_Map, X_in_Map - 1] == 6)
                    {
                        scareTime = 10;
                    }
                }
            }
        }
        if (currentInput == "right")
        {
            if (!(X_in_Map == Map_Size[0] * 2 - 1 || block.Contains(map[Y_in_Map, X_in_Map + 1])))
            {
                ResetTrigger();
                Pac_Animator.SetTrigger("Right");
                tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z), 0.2f);
                source = destination;
                destination[0] = (int)gameObject.transform.position.x + 1;
                canMove = true;
                eaten_pellet = GameObject.Find((X_in_Map + 1).ToString() + "_" + (Y_in_Map).ToString());
                if (pellets.Contains(map[Y_in_Map, X_in_Map + 1]) && eaten_pellet != null)
                {
                    Eat_Sound.Play();
                    Destroy(eaten_pellet);
                    if (map[Y_in_Map, X_in_Map + 1] == 5)
                    {
                        pellets_num += 1;
                        Score.GetComponent<Text>().text = (int.Parse(Score.GetComponent<Text>().text) + 10).ToString();
                    }
                    if (map[Y_in_Map, X_in_Map + 1] == 6)
                    {
                        scareTime = 10;
                    }
                }
            }
        }
        if (!canMove && !Teleport())
        {
            if (currentInput != null && lastInput != null)
            {
                hit.Play();
                Hit_Sound.Play();
            }
            currentInput = null;
            lastInput = null;
            ResetTrigger();
            Pac_Animator.SetTrigger("Stop");
            dust.Stop();
            Moving_Sound.Stop();
        }
        else
        {
            dust.Play();
            if (!Moving_Sound.isPlaying) Moving_Sound.Play();
        }
    }

    public bool Teleport()
    {
        if (gameObject.transform.position.x == -Map_Size[0] && currentInput == "left")
        {
            gameObject.transform.position = new Vector3(Map_Size[0] - 1, gameObject.transform.position.y, 0);
            tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z), 0.2f);
            source[0] = gameObject.transform.position.x;
            destination[0] = (int)gameObject.transform.position.x - 1;
            if (!Moving_Sound.isPlaying) Moving_Sound.Play();
            return true;
        }
        if (gameObject.transform.position.x == Map_Size[0] - 1 && currentInput == "right")
        {
            gameObject.transform.position = new Vector3(-Map_Size[0], gameObject.transform.position.y, 0);
            tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z), 0.2f);
            source[0] = gameObject.transform.position.x;
            destination[0] = (int)gameObject.transform.position.x + 1;
            if (!Moving_Sound.isPlaying) Moving_Sound.Play();
            return true;
        }
        return false;
    }

    public void ResetTrigger()
    {
        Pac_Animator.ResetTrigger("Up");
        Pac_Animator.ResetTrigger("Down");
        Pac_Animator.ResetTrigger("Left");
        Pac_Animator.ResetTrigger("Right");
        Pac_Animator.ResetTrigger("Die");
        Pac_Animator.ResetTrigger("Stop");
    }
}
