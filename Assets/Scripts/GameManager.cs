using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public HeroController hero;

    void Awake()
    {
        Instance = this;        
    }
}
