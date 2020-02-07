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
    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform player;
    [SerializeField] private float offcet; 
    [SerializeField] private List<Transform> pos;
    private gameState state;
    public gameState State{get{return state;}}
    private void Start() {
        state = gameState.play;
    }
    public void loseGame(){
        state = gameState.lose;
    }

    private void Update() {
        initEnemy();
    }

    public void initEnemy(){
        Transform newPos = pos[Random.Range(0,4)];
        GameObject enemy = Instantiate(Enemy ,newPos.position, Quaternion.identity) as GameObject;
        PoliceCar car =  enemy.GetComponent<PoliceCar>();
        car.PlayerCar = player;
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