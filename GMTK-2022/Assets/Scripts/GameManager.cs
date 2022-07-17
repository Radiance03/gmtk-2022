using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public Emotion[] emotions;
    Emotion currentEmotion;
    string lastEmotion;

    public int[] SwitchTimers;
    public float savedSwitch;
    int order;

    public GameObject Player;

    public GameObject TextInfo;
    public GameObject TextInfo2;
    public GameObject TextInfo3;
    public GameObject EmotionDisplay;
    public string EmotionName;

    public GameObject panel;
    public GameObject dice;
    bool roll = false;
    float attackCooldown;

    //so it doesnt roll in the launch of the scene
    float rollCooldown = 2;
 

    public void Start()
    {
        order = -1;
        savedSwitch = 0;
        currentEmotion = emotions[0];
        //dice.GetComponent<Animator>().Play("DiceIdle");




    }
    public void Update()
    {
        rollCooldown -= Time.deltaTime;
        if(rollCooldown <= 0) { 
        }
        savedSwitch -= Time.deltaTime;

        
        if(savedSwitch <= 2 && roll && rollCooldown < 0)
        {
            dice.GetComponent<Animator>().Play("DiceRoll");
            roll = false;

        }
        if (savedSwitch <= 0) //time to change emotion!
        {
            roll = true;
            order++;
            savedSwitch = SwitchTimers[order];
            

            SetNewEmotion();

            lastEmotion = currentEmotion.name;
            Debug.Log(currentEmotion.name);

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
            if(currentEmotion.SPEED == 1) { speedInfo = "SLOWEST"; }
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
            +"SPEED : " + speedInfo.ToUpper() + "\n"
            + "STRENGTH : " + strengthinfo.ToUpper() + "\n";

            TextInfo2.GetComponent<Text>().text =
                "ATTACK : " + currentEmotion.ATTACKTYPE;

            TextInfo2.GetComponent<Text>().text =
             "COOLDOWN : " + currentEmotion.COOLDOWN + " SECONDS";





        }
        attackCooldown -= Time.deltaTime;
        if(attackCooldown <= 0)
        {
            Player.GetComponent<Player>().AllowedToAttack = true;
            attackCooldown = currentEmotion.COOLDOWN;
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
