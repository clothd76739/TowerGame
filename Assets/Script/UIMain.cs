using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;



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
    Text hpText;
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
    
    int hpvalue;    
    int lossvalue;
    int endlossvalue;
    int bestscore;    

    public List<int> RankList = new List<int>();
    
    public void Hpdownbutton() 
    {
        GameObject.Find("Canvas").GetComponent<HealthControl>().ReduceHealth();
    }
    
    public void RankAdd()
    {
        RankList.Add(endlossvalue);
    }    
    public void ShowBestScore()
    {
        RankList.Sort();        
        bestscore=RankList[0];       
        bestScore.text = bestscore.ToString();        
    }
    public void toptenRanklist() 
    { 
        RankList.Sort();
        if (RankList.Count > 10) 
        {
            RankList.RemoveAt(10);
        }
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
            gotoTheEnd(); 
        }
    }
    public void AddLoss()
    {
        lossvalue += 1;
        lossText.text = lossvalue.ToString();
        endlossvalue = lossvalue;
    }
    public void setHPandLoss()
    {
        GameObject.Find("Canvas").GetComponent<HealthControl>().Start();
        hpvalue = 100;
        hpText.text = hpvalue.ToString();
        lossvalue = 0;
        endlossvalue=0;
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
    public void OnRestart()
    {
        MenuPause.SetActive(false);
        setHPandLoss();         
    }
    public void ExitGame()
    {
        MenuPause.SetActive(false);        
        gamePanel.SetActive(false);
        openTopPage();
    }
    public void gotoTheEnd()
    {        
        endLossText.text = endlossvalue.ToString();               
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        RankAdd();
        toptenRanklist();
    }    
    public void returnTopPage()
    {        
        endPanel.SetActive(false);
        openTopPage();        
    }
    public void openTopPage()
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
        setHPandLoss();        
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
    // Update is called once per frame

    void Update()
    {

    }
}

