using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState
{
    play,pause,lose
}

public class gameManager : singelton<gameManager>
{
    [SerializeField] private Transform player;
    [SerializeField] private float offcet; 
    [SerializeField] private List<Transform> pos;
    [SerializeField] private float TimeBetweenClone;
    private float cloneTime;
    private gameState state;
    public gameState State{get{return state;}}
    private void Start() {
        state = gameState.play;
        cloneTime = TimeBetweenClone;
    }
    public void loseGame(){
        state = gameState.lose;
    }
}