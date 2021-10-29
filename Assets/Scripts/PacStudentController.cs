using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public GameObject Bonus_Pellet_Instance;
    public bool Started;
    private int[,] map;
    private string lastInput;
    private string currentInput;
    private float[] destination;
    private float[] source;
    private float deltatime;

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
        Started = false;
        dust.Stop();
        hit.Stop();
        deltatime = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Started)
        {
            Started = true;
            if (!Moving_Sound.isPlaying) Moving_Sound.Play();
        }
        if (Started)
        {
            GetInput();
            Move();
            PowerPellet();
        }
    }

    public void PowerPellet()
    {
        deltatime += Time.deltaTime;
        if (deltatime > 10)
        {
            deltatime = 0;
            int ran = Random.Range(0, 4);
            float x = int.MaxValue;
            float y = int.MaxValue;
            if (ran == 0)
            {
                x = -(map.GetLength(1) + 4) / 2;
                y = Random.Range(-(map.GetLength(0) + 4), (map.GetLength(0) + 4));
            }
            if (ran == 1)
            {
                x = (map.GetLength(1) + 4) / 2;
                y = Random.Range(-(map.GetLength(0) + 4), (map.GetLength(0) + 4));
            }
            if (ran == 2)
            {
                x = Random.Range(-(map.GetLength(1) + 4), (map.GetLength(1) + 4));
                y = -(map.GetLength(0) + 4) / 2;
            }
            if (ran == 3)
            {
                x = Random.Range(-(map.GetLength(1) + 4), (map.GetLength(1) + 4));
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
        float X = gameObject.transform.position.x;
        float Y = gameObject.transform.position.y;
        if (X != destination[0] || Y != destination[1]) { return; }
        int X_in_Map = (int)source[0] + Map_Size[0];
        int Y_in_Map = (int)-source[1] + Map_Size[1] - 1;

        bool canMove = false;
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
