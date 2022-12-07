using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Extension para manejar escenas

public class LevelChange : MonoBehaviour
{//START CLASS MG1_MAE_SceneLoader
    public void LevelChanger(string _levelToLoad)
    {//START LevelChanger
        SceneManager.LoadScene(_levelToLoad);
        PlayerPrefs.DeleteKey("score");
    }//END LevelChanger
}//END CLASS MG1_MAE_SceneLoader
