using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_DoorKey : MonoBehaviour
{
    [Tooltip("True - red key object, false - blue key")]
    public bool isRedKey = true;
    AN_HeroInteractive hero;

    private void Start()
    {
        hero = FindObjectsByType<AN_HeroInteractive>(FindObjectsSortMode.None)[0]; // key will get up and it will saved in "inventary"
    }

    public void Execute()
    {
        if (isRedKey) hero.RedKey = true;
        else hero.BlueKey = true;
        Destroy(gameObject);
    }   
}
