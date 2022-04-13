using UnityEngine;
using System.Collections;
using System;  //Serializable
using System.Runtime.Serialization.Formatters.Binary; //BinaryFormatter
using System.IO;  //FileStream

public class ScoreBoardDataControl : MonoBehaviour
{
    //Singleton Pattern，可使任何程式呼叫ScoreBoardDataControl.instance取得該物件
    public static ScoreBoardDataControl instance;

    //實際儲存分數資料的物件(ScoreData定義在下方)
    private ScoreData data;

    //要顯示的名次數量(3表示只顯示前三名)     
    private const int Places = 3;

    void Awake()
    {
        //Singleton Pattern，只能擁有一個ScoreBoardDataControl物件
        if (instance == null)
        {

            //轉換場景時不要移除此Game Object
            DontDestroyOnLoad(gameObject);

            //載入排行榜資料
            LoadData();

            //把instance設好，以供其他地方以ScoreBoardDataControl.instance取得物件
            instance = this;

        }
        else if (instance != this)
        {

            //第二次進入開始畫面，把新的ScoreBoardDataControl刪掉
            Destroy(gameObject);
        }
    }

    void LoadData()
    {
        //如果檔案存在(表示不是第一次開啟遊戲)
        if (File.Exists(Application.persistentDataPath + "/scoreInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/scoreInfo.dat", FileMode.Open);

            //把裝置中的二進位檔案反序列化，存入data變數中
            data = (ScoreData)bf.Deserialize(file);
            file.Close();
        }
        else
        {     //如果檔案不存在(第一次開啟遊戲)
            InitData(); //初始化資料
            SaveData(); //儲入裝置
        }
    }

    void InitData()
    {
        //初始化data、data的陣列
        data = new ScoreData();
        data.scores = new int[Places];
        data.playerTypes = new int[Places];

        for (int i = 0; i < Places; i++)
        {
            data.scores[i] = 0;
            data.playerTypes[i] = -1;
        }
    }

    void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();

        //新增檔案
        FileStream file = File.Create(Application.persistentDataPath + "/scoreInfo.dat");

        //序列化，存入裝置
        bf.Serialize(file, data);
        file.Close();
    }

    public void NewScore(int playerType, int score)
    {
        //判斷此分數能排在第幾名
        int place = Places - 1;

        while (place >= 0 && score > data.scores[place])
        {
            place--;
        }

        place++;

        //無法進入排行榜
        if (place >= Places)
            return;

        //把此分數之後的排名向後挪一名
        for (int i = Places - 2; i >= place; i--)
        {
            data.scores[i + 1] = data.scores[i];
            data.playerTypes[i + 1] = data.playerTypes[i];
        }

        //更新名次
        data.scores[place] = score;
        data.playerTypes[place] = playerType;

        //存入裝置
        SaveData();
    }

    //取得place名次的分數
    public int GetScore(int place)
    {
        return data.scores[place];
    }


    //取得place名次所使用的角色
    public int GetPlayerType(int place)
    {
        return data.playerTypes[place];
    }
}

[Serializable]  //可序列化的類別
class ScoreData
{
    public int[] scores; //儲存前三名的分數
    public int[] playerTypes; //儲存前三名使用的角色
}