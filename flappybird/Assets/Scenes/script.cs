using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class script : MonoBehaviour
{
    public int playerscore;
    public Text scoretext;
    public GameObject gameoverscreen;

    [ContextMenu("AddScore")]
    public void addscore(int scoretoadd){
        playerscore = playerscore + scoretoadd;
        scoretext.text = playerscore.ToString();
    }
    public void restartgame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameover(){
        gameoverscreen.SetActive(true);
    }
}
