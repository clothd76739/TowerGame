using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Threading;

public class UIMain : MonoBehaviour
{
    [SerializeField]
    GameObject startPanel;
    [SerializeField]
    GameObject gamePanel;
    [SerializeField]
    GameObject endPanel;
    [SerializeField]
    GameObject MenuPause;
    [SerializeField]
    GameObject MenuRecords;    
    [SerializeField]
    public Text hpText;
    [SerializeField]
    Text lossText;
    [SerializeField]
    Text endLossText;
    [SerializeField]
    Text bestScore;
    [SerializeField]
    GameObject recordPrefab;
    [SerializeField]
    GameObject panelBoard;
    [SerializeField]
    GameObject panelhelp;
    [SerializeField]
    GameObject panelhelp1;
    [SerializeField]
    GameObject panelhelp2;
    [SerializeField]
    GameObject panelhelp3;
    public int hpvalue;
    int lossvalue;
    static public int endlossvalue;
    int bestscore; 
    public List<int> RankList = new List<int>();

    public bool gameover = false;

    //0520 :  tower_low-Canvas set new Tag "hpBar" 
    //UIManger open on Hierarchy
    private void Awake()
    {
        LoadGame();
        ShowBestScore();
    }
    public void RankAdd()
    {
        RankList.Add(endlossvalue);
    }
    // add if()
    public void ShowBestScore()
    {
        if (File.Exists(Application.persistentDataPath + "/byBin.txt"))
        {
            RankList.Sort();
            bestscore = RankList[0];
            bestScore.text = bestscore.ToString();
        }
    }
    // add creatsaveRanklist()
    public void toptenRanklist() 
    { 
        RankList.Sort();
        if (RankList.Count > 10) 
        {
            RankList.RemoveAt(10);
        }
        creatsaveRanklist();
    }
    // Save.cs  save RankList
    private Save creatsaveRanklist()
    {        
        Save save = new Save();
        save.rankTop10 = RankList;
        return save;
    }
    public void ShowScoreBoard()
    {        
        for (int i = 0; i > RankList.Count; i++)
        {
            RankList[i].ToString();
        }
        
        for (int i = 0; i < RankList.Count; i++)
        {
            
            GameObject record = Instantiate(recordPrefab, panelBoard.transform);
            
            record.gameObject.SetActive(true);
            record.transform.Find("Number").GetComponent<Text>().text = ("NO. "+(i + 1).ToString());
            record.transform.Find("Score").GetComponent<Text>().text = RankList[i].ToString();
        }
    }    
    public void DestroyScoreBoard() 
    {
        Transform transform;
        for (int i = 0; i < panelBoard.transform.childCount; i++)
        {
            transform = panelBoard.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }
    public void onStart()
    {
        setHPandLoss();
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        GameObject.Find("AR Session Origin").GetComponent<ARController>().towerTrue();//
    }    
    public void OpenRecords()
    {
        ShowScoreBoard();
        MenuRecords.SetActive(true);       
    }
    public void CloseRecords()
    {
        DestroyScoreBoard();
        MenuRecords.SetActive(false);
    }        
    public void SubHP()
    {
        hpvalue -= 10;
        hpText.text = hpvalue.ToString();
        if (hpvalue == 0) 
        {
            GameObject.FindWithTag("Tower").GetComponent<TowerBehaviour>().DestroyTower();
            GameObject[] tagObject = GameObject.FindGameObjectsWithTag("Soldier");
            for (int i = 0; i <= tagObject.Length; i++)
            {
                GameObject.Destroy(tagObject[i]);
            }
            var p = GameObject.FindWithTag("Soldier");
            if (p == null)
            {
                gotoTheEnd();
            }
        }
    }
    public void AddLoss()
    {
        lossvalue += 1;
        lossText.text = lossvalue.ToString();
        endlossvalue = lossvalue;
    }
    /*
    public void Hpdownbutton() // Find("Canvas")=>FindWithTag("hpBar") 
    {
        GameObject.FindWithTag("hpBar").GetComponent<HealthControl>().ReduceHealth();
    }*/
    /*
    public void setHPbar()// Add set_HealthBar()
    {
        GameObject.FindWithTag("hpBar").GetComponent<HealthControl>().Start();
    }*/
    public void setHPandLoss()// remove:GameObject.Find("Canvas").GetComponent<HealthControl>().Start();& add:set endlossvalue 
    {      
        hpvalue = 1000;
        hpText.text = hpvalue.ToString();
        lossvalue = 0;
        endlossvalue = 0; 
        lossText.text = lossvalue.ToString();
        Time.timeScale = 1f;
    }
    public void OnPause()
    {
        Time.timeScale = 0;
        MenuPause.SetActive(true);
    }
    public void OnResume()
    {
        Time.timeScale = 1f;
        MenuPause.SetActive(false);
    }
    public void OnRestart()// add setHPbar()
    {
        MenuPause.SetActive(false);
        //setHPbar();
        setHPandLoss();
        GameObject.Find("AR Session Origin").GetComponent<ARController>().towerTrue();//
    }
    public void ExitGame()// add setHPbar()
    {
        MenuPause.SetActive(false);        
        gamePanel.SetActive(false);
        openTopPage();
        //setHPbar();
    }
    public void gotoTheEnd()// add SaveGame()
    {        
        endLossText.text = endlossvalue.ToString();
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        RankAdd();
        toptenRanklist();
        SaveGame();
    }    
    public void returnTopPage()
    {        
        endPanel.SetActive(false);
        openTopPage();        
    }
    public void openTopPage()// remove: setHPandLoss(); & add: setHPbar();
    {
        if (RankList.Count==0) 
        { 
            bestScore.text = "0";
        }
        else 
        {         
            ShowBestScore();
        }
        startPanel.SetActive(true);
        //setHPbar();

    }
    
    public void help() 
    {
        startPanel.SetActive(false);
        panelhelp.SetActive(true);
        panelhelp1.SetActive(true);
    }
    public void step1right()
    { 
        panelhelp1.SetActive(false);
        panelhelp2.SetActive(true);
    }
    public void step2left() 
    {
        panelhelp2.SetActive(false);
        panelhelp1.SetActive(true);
    }
    public void step2right() 
    {
        panelhelp2.SetActive(false);
        panelhelp3.SetActive(true);
    }
    public void step3left() 
    {
        panelhelp3.SetActive(false);
        panelhelp2.SetActive(true);
    }    
    public void step3right() 
    {
        panelhelp3.SetActive(false);
        panelhelp.SetActive(false);
        startPanel.SetActive(true);
    }
    public void QuitGame() { Application.Quit(); }
    // save game
    public void SaveGame()
    {
        SaveByBin();
    }
    // load game
    public void LoadGame()
    {
        LoadByBin();
    }
    private void setRankList(Save save)
    {

        RankList = save.rankTop10;
    }
        
    private void SaveByBin()
    {
        //序列化,建立儲存文檔"/byBin.txt"
        Save save = creatsaveRanklist();        
        BinaryFormatter bf = new BinaryFormatter();        
        FileStream fileStream = File.Create(Application.persistentDataPath + "/byBin.txt");        
        bf.Serialize(fileStream, save);        
        fileStream.Close();
        Debug.Log("存檔文件成功");
    }
    private void LoadByBin()
    {
        if (File.Exists(Application.persistentDataPath + "/byBin.txt"))
        {
            //反序列化,"/byBin.txt"文檔存在則讀檔
            BinaryFormatter bf = new BinaryFormatter();            
            FileStream fileStream = File.Open(Application.persistentDataPath + "/byBin.txt", FileMode.Open);            
            Save save = (Save)bf.Deserialize(fileStream);            
            fileStream.Close();
            setRankList(save);
        }
        else { Debug.Log("存檔文件不存在"); }

    }
    // Update is called once per frame

    void Update()
    {

    }
}

