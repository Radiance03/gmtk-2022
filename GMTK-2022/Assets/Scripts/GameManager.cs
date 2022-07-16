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
    float savedSwitch;
    int order;

    public GameObject Player;

    public GameObject TextInfo;
    public GameObject EmotionDisplay;
    public string EmotionName;

    public GameObject panel;
    public GameObject dice;
    bool roll = true;
 

    public void Start()
    {
        order = -1;
        savedSwitch = 0;
        currentEmotion = emotions[0];
        dice.GetComponent<Animator>().Play("DiceIdle");



    }
    public void Update()
    {
        savedSwitch -= Time.deltaTime;

        if(savedSwitch <= 2 && roll)
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
            // set currentEmotion.effect active

            //TEXT INFO
            string speedInfo;
            string strengthinfo;
            if(currentEmotion.SPEED == 1) { speedInfo = "VERY SLOW"; }
            else if (currentEmotion.SPEED == 2) { speedInfo = "SLOW"; }
            else if (currentEmotion.SPEED == 3) { speedInfo = "NORMAL"; }
            else if (currentEmotion.SPEED == 4) { speedInfo = "FAST"; }
            else if (currentEmotion.SPEED == 5) { speedInfo = "SUPER FAST"; } else { speedInfo = "Error"; };

            if (currentEmotion.STRENGTH == 1) { strengthinfo = "VERY SLOW"; }
            else if (currentEmotion.STRENGTH == 2) { strengthinfo = "SLOW"; }
           else if (currentEmotion.STRENGTH == 3) { strengthinfo = "NORMAL"; }
           else if (currentEmotion.STRENGTH == 4) { strengthinfo = "FAST"; }
           else if (currentEmotion.STRENGTH == 5) { strengthinfo = "SUPER FAST"; } else { strengthinfo = "Error"; };

            
            panel.GetComponent<Image>().color = currentEmotion.COLOR;


            TextInfo.GetComponent<Text>().text =
                "YOU ARE " + currentEmotion.name + "\n"
            +"SPEED IS " + speedInfo + " \n"
             + "STRENGTH IS " + strengthinfo + "\n";



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
