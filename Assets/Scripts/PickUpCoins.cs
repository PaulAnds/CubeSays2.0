using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoins : MonoBehaviour
{

    public LevelManager LevelManager;
    public Commands Commands;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
        Commands = FindObjectOfType<Commands>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,0.8f,0f,Space.Self);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            LevelManager.isCoinAlive = false;
            Commands.coinIsSpawned = false;
            Destroy(gameObject);
        }
    }

}
