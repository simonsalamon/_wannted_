using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState
{
    play,pause,lose
}

public class gameManager : singelton<gameManager>
{
    [SerializeField] private GameObject expo;
    private gameState state;
    public gameState State{get{return state;}}
    private void Start() {
        state = gameState.play;
    }
    public void loseGame(){
        state = gameState.lose;
    }
    public void explorecar(Vector3 position){
        // here we explore cars when collide
        GameObject go = Instantiate(expo , position , Quaternion.identity) as GameObject;
        go.transform.rotation = expo.transform.rotation;
        ParticleSystem particle = go.GetComponent<ParticleSystem>();
        particle.enableEmission = true;
        Destroy(go,10);
    }
}
