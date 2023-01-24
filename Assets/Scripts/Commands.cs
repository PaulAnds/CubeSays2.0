using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//change to courutines to get rid of ifs/countdown
//hide from camera using raycasts

public class Commands : MonoBehaviour
{
    #region Variables
    
    [Header("What the command says")]
    public Text command;
    public float countdown;

    [HideInInspector]
    public Player player;
    public LevelManager LevelManager;
    public CameraMovement _camera;
    private bool oldCamera; 
    public bool coinIsSpawned;

    [Header("When you loose Change to these scene")]
    public string scene;
    private bool lost;
    private int random;

    [Header("GameObjects Needed")]
    public GameObject Coin;
    
    private bool m_HitDetect;
    private RaycastHit m_Hit;
    public GameObject rayCastBody;

    #endregion

    private void Start()
    {
        countdown = 10;
        _camera = FindObjectOfType<CameraMovement>();
        player = FindObjectOfType<Player>();
        LevelManager = FindObjectOfType<LevelManager>();
        lost = false;
        random = 0;
    }

    private void Update()
    {
        switch (random)
        {
            case 0:
                HideFromCamera();
                break;
            case 1:
                GetUp();
                break;
            case 2:
                FindCoins();
                break;
            case 3:
                ChangeCamera();
                break;
        }
    }

    #region Commands

    private void DontMove(){
        if (countdown > 5){
            command.text = "DONT MOVE";
        }
        else if (countdown > 0){
            command.text = "";
            if(player.rb.velocity != Vector3.zero){
                countdown = 4.0f;
                lost = true;           
            }
        }
        else{   
            if(lost){
                lost = false;
                SceneManager.LoadScene(scene); 
            }
            random = Random.Range(1, 4);
            switch(random)
            {
                case 0:
                    countdown = 8;
                    break;
                case 1:
                    countdown = 15;
                    break;
                case 2:
                    countdown = 21;
                    break;
                case 3:
                    countdown = 13;
                    break;
            
            }
        }
        countdown -= Time.deltaTime;
        if(lost){
                command.text = "YOU LOST";
        }
    }

    private void GetUp(){
        if(countdown > 12){
            command.text = "GET UP";
        }
        else if(countdown > 2){
            command.text = "";
        }
        else if(countdown > 0){
            if(player.transform.position.y <= 0.55f){
                lost = true;
                countdown = 4.0f;
            }
        }
        else{   
            if(lost){
                lost = false;
                SceneManager.LoadScene(scene); 
            }
            random = Random.Range(0, 4);
            if (random == 1){
                random = 3;
            }
            switch(random)
            {
                case 0:
                    countdown = 8;
                    break;
                case 1:
                    countdown = 15;
                    break;
                case 2:
                    countdown = 21;
                    break;
                case 3:
                    countdown = 13;
                    break;
            
            }
        }
        countdown -= Time.deltaTime;
        if(lost){
                command.text = "YOU LOST";
        }
    }

    private void FindCoins(){
        if(countdown > 18){
            SpawnCoin(Random.Range(0,6));
            LevelManager.isCoinAlive = true;
            command.text = "FIND COIN";
        }
        else if(countdown > 3){
            command.text = "";
            if(!LevelManager.isCoinAlive)
            {
                countdown = 3.0f;
            }
        }
        else if(countdown > 0){
            if(LevelManager.isCoinAlive){
                lost = true;
                SceneManager.LoadScene(scene); 
            }
        }
        else{   
            random = Random.Range(0, 4);
            if (random == 2){
                random = 0;
            }
            switch(random)
            {
                case 0:
                    countdown = 8;
                    break;
                case 1:
                    countdown = 15;
                    break;
                case 2:
                    countdown = 21;
                    break;
                case 3:
                    countdown = 13;
                    break;
            
            }
        }
        countdown -= Time.deltaTime;
        if(lost){
                lost = false;
                command.text = "YOU LOST";
        }

    }

    private void ChangeCamera(){
        if (countdown > 10){ 
            command.text = "CHANGE VIEW";
        }
        else if (countdown > 5)
        {
            command.text = "";
            oldCamera = _camera.Camera2D;
            if(oldCamera != _camera){//IF THEY DONT CHANGE CAMERA
                countdown = 3.0f;
            }
        }
        else if (countdown > 0){
            if(oldCamera == _camera){//IF THEY DONT CHANGE CAMERA
                lost = true;           
            }
        }
        else{   
            if(lost){
                lost = false;
                SceneManager.LoadScene(scene); 
            }
            random = Random.Range(0, 3);
            switch(random)
            {
                case 0:
                    countdown = 8;
                    break;
                case 1:
                    countdown = 15;
                    break;
                case 2:
                    countdown = 21;
                    break;
                case 3:
                    countdown = 13;
                    break;
            
            }
        }
        countdown -= Time.deltaTime;
        if(lost){
            command.text = "YOU LOST";
        }
    }

    private void HideFromCamera()
    {
        if (countdown > 10){ 
            command.text = "HIDE!";
        }
        else if (countdown > 5)
        {
            Debug.Log("cast raycast");
            command.text = "";
            //var layerMask = LayerMask.GetMask("Player");
            //Physics.OverlapSphere(rayCastBody.transform.position, 100f, layerMask);
            m_HitDetect = Physics.BoxCast(rayCastBody.transform.position, transform.localScale, transform.forward, out m_Hit, transform.rotation, 100f);
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);


        }
        countdown -= Time.deltaTime;
    }
    #endregion

    private void SpawnCoin(int x)
    {
        if (!coinIsSpawned && !LevelManager.isCoinAlive)
        {
            switch (x)
            {
                case 0:
                    Instantiate(Coin, new Vector3(-5.5f, 0.7f, 5.3f), Coin.transform.rotation);
                    coinIsSpawned = true;
                    break;
                case 1:
                    Instantiate(Coin, new Vector3(-4.2f, 0.7f, -7.0f), Coin.transform.rotation);
                    coinIsSpawned = true;
                    break;
                case 2:
                    Instantiate(Coin, new Vector3(0.2f, 2.45f, -7.5f), Coin.transform.rotation);
                    coinIsSpawned = true;
                    break;
                case 3:
                    Instantiate(Coin, new Vector3(4.0f, 2.45f, 6.3f), Coin.transform.rotation);
                    coinIsSpawned = true;
                    break;
                case 4:
                    Instantiate(Coin, new Vector3(0.0f, 0.7f, -3.4f), Coin.transform.rotation);
                    coinIsSpawned = true;
                    break;
                case 5:
                    Instantiate(Coin, new Vector3(5.0f, 0.7f, -2.44f), Coin.transform.rotation);
                    coinIsSpawned = true;
                    break;
            }
        }
    }
}
