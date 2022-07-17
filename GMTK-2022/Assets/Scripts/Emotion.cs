using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="new emotion",menuName = "emotion")]
public class Emotion : ScriptableObject
{
    public Sprite IMAGE;
    public string NAME;
    public int SPEED;
    public int STRENGTH;
    public float WALKSPEEDANIMATOR;
    public Sprite Panel;
    public float COOLDOWN;
    public string ATTACKTYPE;
   // public AudioSource Jam;

    //public GameObject EFFECTS;
}
