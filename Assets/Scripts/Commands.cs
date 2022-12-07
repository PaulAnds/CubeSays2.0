using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Commands : MonoBehaviour
{
    [Header("What the command says")]
    public Text command;
    private float countdown;

    [HideInInspector]
    public Player player;
    [HideInInspector]
    public LevelManager LevelManager;
    [HideInInspector]
    public bool coinIsSpawned;

    [Header("When you loose Change to these scene")]
    public string scene;
    private bool lost;
    private int random;

    [Header("GameObjects Needed")]
    public GameObject Coin;

    // Start is called before the first frame update
    void Start()
    {
        countdown = 0;
        player = FindObjectOfType<Player>();
        LevelManager = FindObjectOfType<LevelManager>();
        lost = false;
        random = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(random == 0){
            DontMove();
        }
        else if(random == 1){
            GetUp();
        }
        else if(random == 2){
            findCoins();
        }


    }

    void DontMove(){
        if (countdown < 3){
            command.text = "DONT MOVE";
            countdown += Time.deltaTime;
        }
        else if (countdown < 8){
            command.text = "";
            countdown += Time.deltaTime;
            Debug.Log(player.rb.velocity);
            if(player.rb.velocity != Vector3.zero){
                countdown = 4.0f;
                Debug.Log("Lost");
                lost = true;           
        }
        }
        else{   
            if(lost){
                lost = false;
                //SceneManager.LoadScene(scene); 
            }
            countdown = 0.0f;
            random = Random.Range(1, 3);
        }

        if(lost){
                command.text = "YOU LOST";
        }
    }

    void GetUp(){
        if(countdown < 3){
            command.text = "GET UP";
            countdown += Time.deltaTime;
        }
        else if(countdown < 13){
            command.text = "";
            countdown += Time.deltaTime;
        }
        else if(countdown < 16){
            countdown += Time.deltaTime;
            if(player.transform.position.y <= 0.55f){
                lost = true;
                countdown = 4.0f;
            }
        }
        else{   
            if(lost){
                lost = false;
                //SceneManager.LoadScene(scene); 
            }
            countdown = 0.0f;
            if(Random.Range(0, 3) == 0){
                random = 0;
            }
            else{
                random = 2;
            }
        }

        if(lost){
                command.text = "YOU LOST";
        }
    }

    void findCoins(){
        if(countdown < 3){
            SpawnCoin(Random.Range(0,6));
            LevelManager.isCoinAlive = true;
            command.text = "FIND COIN";
            countdown += Time.deltaTime;
        }
        else if(countdown < 18){
            command.text = "";
            countdown += Time.deltaTime;
        }
        else if(countdown < 21){
            countdown += Time.deltaTime;
            if(LevelManager.isCoinAlive){
                lost = true;
                //SceneManager.LoadScene(scene); 
            }
        }
        else{   
            countdown = 0.0f;
            random = Random.Range(0, 2);
        }

        if(lost){
                lost = false;
                command.text = "YOU LOST";
        }

    }

    //change camera
    //hide from camera


    void SpawnCoin(int x){
        if(!coinIsSpawned && !LevelManager.isCoinAlive){
            if(x == 0){
                Debug.Log("CoinSpawned");
                Instantiate(Coin,new Vector3(-5.5f,0.7f,5.3f),Coin.transform.rotation);
                coinIsSpawned = true;
            }
            else if(x == 1){
                Debug.Log("CoinSpawned");
                Instantiate(Coin,new Vector3(-4.2f,0.7f,-7.0f),Coin.transform.rotation);
                coinIsSpawned = true;
            }
            else if(x == 2){
                Debug.Log("CoinSpawned");
                Instantiate(Coin,new Vector3(0.2f,2.45f,-7.5f),Coin.transform.rotation);
                coinIsSpawned = true;
            }
            else if(x == 3){
                Debug.Log("CoinSpawned");
                Instantiate(Coin,new Vector3(4.0f,2.45f,6.3f),Coin.transform.rotation);
                coinIsSpawned = true;
            }
            else if(x == 4){
                Debug.Log("CoinSpawned");
                Instantiate(Coin,new Vector3(0.0f,0.7f,-3.4f),Coin.transform.rotation);
                coinIsSpawned = true;
            }
            else{
                Debug.Log("CoinSpawned");
                Instantiate(Coin,new Vector3(5.0f,0.7f,-2.44f),Coin.transform.rotation);
                coinIsSpawned = true;
            }
        }
    }

}
