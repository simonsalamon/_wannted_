using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlMaker : MonoBehaviour
{
    public GameObject[] policeCars;
    public float timeBetweenClone;
    public float timeClone;
    int lvl;
    float s;
    int time;
    void Start()
    {
        lvl = data.Lvl;
        timeClone = timeBetweenClone;
    }
    void Update()
    {
        addSecond();
        checkLvl();
        makeEnemy();
    }

    public void addSecond(){
        s += Time.fixedDeltaTime;
        if (s % 1 == 0 )
        {
            time++;
            s = 0;
        }
    }
    public void checkLvl(){
        if (time % 5 == 0 )
        {
            lvl++;
            data.Lvl++;
            if (timeBetweenClone >=1)
            {
                timeBetweenClone -= 0.5f;
            }
        }
    }
    public void makeEnemy(){
        timeClone -= Time.fixedDeltaTime;
        if (timeClone <= 0)
        {
            timeClone = timeBetweenClone;
            if (lvl <= 2)
            {
                //      frist index of array
                gameManager.Instance.initEnemy(policeCars[0]);            
            }
            else
            {
                //      from other places
                gameManager.Instance.initEnemy(policeCars[Random.Range(0 , policeCars.Length)]);
            }
        }
    }
}
