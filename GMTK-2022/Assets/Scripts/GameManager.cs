using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public AudioSource Happy;
    public AudioSource Sad;
    public AudioSource Angry;
    public AudioSource Stable;
    public AudioSource EnemyDeath;

    public Emotion[] emotions;
    public Emotion currentEmotion;
    string lastEmotion;


    //Timers for each emotion duration
    public int[] SwitchTimers;
    public float savedSwitch;

    public float deafultTime;
    int order;

    public GameObject Player;

    // -------------UI---------------
    public GameObject TextInfo;
    public GameObject TextInfo2;
    public GameObject TextInfo3;
    public GameObject EmotionDisplay;
    public string EmotionName;

    public GameObject panel;
    public GameObject dice;
    bool roll = false;
    float attackCooldown;

    public int enemyAmount = 0;
    public bool lastWave = false;
    
    public GameObject nextMsg;
    public string nextScene;

    private float endTimer = 5;

    //so it doesnt roll in the launch of the scene
    float rollCooldown = 2;
 

    public void Start()
    {
        order = -1; //Index for the SwitchTimers[]
        savedSwitch = 0;
        currentEmotion = emotions[0];

    }
    public void Update()
    {
        rollCooldown -= Time.deltaTime;
        savedSwitch -= Time.deltaTime;
        EndState_Check();

        if (savedSwitch <= 2 && roll && rollCooldown < 0)
        {
            dice.GetComponent<Animator>().Play("DiceRoll");
            roll = false;

        }
        if (savedSwitch <= 0) //time to change emotion!
        {
            ChangeEmotion();
        }

        RenderText();

        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            Player.GetComponent<Player>().AllowedToAttack = true;
            attackCooldown = currentEmotion.COOLDOWN;
        }

    }

    private void RenderText()
    {
        TextInfo3.GetComponent<Text>().text =
               "NEXT ROLL : " + savedSwitch.ToString("F1") + " SECONDS";
        TextInfo2.GetComponent<Text>().text =
            "COOLDOWN : " + currentEmotion.COOLDOWN + " SECONDS    HP : " + Player.GetComponent<Player>().HP;
    }

    private void ChangeEmotion()
    {
        //EmotionTransition.Play();
        roll = true;
        order++;
        if (order < SwitchTimers.Length)
        {
            savedSwitch = SwitchTimers[order];
        }
        else
        {
            savedSwitch = deafultTime;
        }



        SetNewEmotion();

        lastEmotion = currentEmotion.name;
        //Debug.Log(currentEmotion.name);

        //set speed, strength, picture, text, effect


        Player.GetComponent<Player>().speed = currentEmotion.SPEED;
        Player.GetComponent<Player>().strength = currentEmotion.STRENGTH;
        Player.GetComponent<Player>().WalkSpeed = currentEmotion.WALKSPEEDANIMATOR;
        Player.GetComponent<Player>().CurrentEmotionName = currentEmotion.NAME;
        EmotionDisplay.GetComponent<Image>().sprite = currentEmotion.IMAGE;
        EmotionName = currentEmotion.NAME;
        attackCooldown = 0;
        // set currentEmotion.effect active

        //TEXT INFO
        string speedInfo;
        string strengthinfo;
        if (currentEmotion.SPEED == 1) { speedInfo = "SLOWEST"; }
        else if (currentEmotion.SPEED == 2) { speedInfo = "SLOW"; }
        else if (currentEmotion.SPEED == 3) { speedInfo = "NORMAL"; }
        else if (currentEmotion.SPEED == 4) { speedInfo = "FAST"; }
        else if (currentEmotion.SPEED == 5) { speedInfo = "FASTEST"; } else { speedInfo = "Error"; };

        if (currentEmotion.STRENGTH == 1) { strengthinfo = "WEAKEST"; }
        else if (currentEmotion.STRENGTH == 2) { strengthinfo = "WEAK"; }
        else if (currentEmotion.STRENGTH == 3) { strengthinfo = "NORMAL"; }
        else if (currentEmotion.STRENGTH == 4) { strengthinfo = "STRONG"; }
        else if (currentEmotion.STRENGTH == 5) { strengthinfo = "STRONGEST"; } else { strengthinfo = "Error"; };


        panel.GetComponent<Image>().sprite = currentEmotion.Panel;


        TextInfo.GetComponent<Text>().text =
            "YOU ARE : " + currentEmotion.name.ToUpper() + "\n"
        + "SPEED : " + speedInfo.ToUpper() + "\n"
        + "STRENGTH : " + strengthinfo.ToUpper() + "\n";

        TextInfo2.GetComponent<Text>().text =
            "ATTACK : " + currentEmotion.ATTACKTYPE;

        if (currentEmotion.NAME == "Happy") { Happy.Play(); Sad.Pause(); Angry.Pause(); Stable.Pause(); }
        if (currentEmotion.NAME == "Sad") { Sad.Play(); Stable.Pause(); Angry.Pause(); Happy.Pause(); }
        if (currentEmotion.NAME == "Angry") { Angry.Play(); Sad.Pause(); Stable.Pause(); Happy.Pause(); }
        if (currentEmotion.NAME == "Stable") { Stable.Play(); Sad.Pause(); Angry.Pause(); Happy.Pause(); }
    }

    private void EndState_Check()
    {
        if (lastWave && enemyAmount <= 0) { endTimer -= Time.deltaTime; }
        else if (endTimer != 5)
        {
            endTimer = 5;
        }

        if (endTimer <= 0)
        {
            Debug.Log("end of level");
            nextMsg.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void SetNewEmotion()
    {
        currentEmotion = emotions[Random.Range(0, emotions.Length)];
        if (lastEmotion == currentEmotion.name)
        {
            SetNewEmotion();

        }
    }
}
