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
    [SerializeField] private float TimeBetweenClone;
    private float cloneTime;
    private gameState state;
    public gameState State{get{return state;}}
    private void Start() {
        state = gameState.play;
        cloneTime = TimeBetweenClone;
    }

    private void Update() {
        cloneTime -= Time.fixedDeltaTime;
        if(cloneTime <= 0f)
        {
            cloneTime = TimeBetweenClone;
            initEnemy();
        } 
    }

    public void initEnemy(){
        Transform newPos = pos[Random.Range(0,4)];
        GameObject enemy = Instantiate(Enemy ,newPos.position, Quaternion.identity) as GameObject;
        PoliceCar car =  enemy.GetComponent<PoliceCar>();
        car.GetComponent<Rigidbody>().centerOfMass = new Vector3(0,-1.9f,0); 
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
    public void loseGame(){
        state = gameState.lose;
    }
}