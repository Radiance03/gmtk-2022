using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new emotion",menuName = "emotion")]
public class Emotion : ScriptableObject
{
    public Sprite IMAGE;
    public string NAME;
    public int SPEED;
    public int STRENGTH;
    public float WALKSPEEDANIMATOR;
    public Color COLOR;

    public GameObject EFFECTS;
}
