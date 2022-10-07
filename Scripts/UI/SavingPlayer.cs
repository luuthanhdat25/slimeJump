using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
[System.Serializable]
public class Player
{
    public string Name, ID;
    public double Score, Time;
}

[System.Serializable]
public class ListPlayer
{
    public List<Player> listPlayer;
}

public class SavingPlayer : MonoBehaviour
{
    static public string FileName = "Saving.json";
    static public string DirectionName = "/SaveData/";
    public CanvaEdit RequestUpdate;
    public static ListPlayer PlayerNow;
 /*   public string NamePlayer;
    public string IDPlayer;
    public double ScorePlayer;
    public double TimePlayer;
*/
    void Awake()
    {
        Load();
        SaveData();
    }
    void Update()
    { 
    }

    static public void RequestAddPalyer(string Name, string Idenf, double Score, double Time) 
    {
        Player NewPlayer = new Player();
        NewPlayer.Name = Name; NewPlayer.ID = Idenf;
        NewPlayer.Score = Score; NewPlayer.Time = Time;

        foreach (var Slime in PlayerNow.listPlayer) 
        {
            if (Slime.ID == Idenf && Slime.Name == Name)
            {
                if (Slime.Score < Score || Slime.Score == Score && Slime.Time > Time)
                {
                    Slime.Score = Score;
                    Slime.Time = Time;
                    SaveData();
                }
                return;
            }
        }

        PlayerNow.listPlayer.Add(NewPlayer);
        SaveData();
    }

    public (double, double) RequestGetPlayer(string Name, string Idenf)
    {
        foreach (var Slime in PlayerNow.listPlayer)
        {
            if (Slime.Name == Name && Slime.ID == Idenf)
                return (Slime.Score, Slime.Time);
        }
        return (-1, -1);
    }

    public void RemoveElenment(string NameRemove, string Idenf) 
    {
        foreach (var Element in PlayerNow.listPlayer)
            if (Element.Name == NameRemove && Element.ID == Idenf)
            {
                PlayerNow.listPlayer.Remove(Element);
                SaveData(); return;
            }
    }

    static public bool CheckExit(string Idef)
    {
        if (Idef == "") return false; 
        foreach (var Element in PlayerNow.listPlayer)
            if (Element.ID == Idef) return true;
        return false;
    }

    static public void SaveData() 
    {
        string DirectionPath = Application.persistentDataPath + DirectionName;

        if (Directory.Exists(DirectionPath) == false)
        {
            Directory.CreateDirectory(DirectionPath);
        }

        string Json = JsonUtility.ToJson(PlayerNow, true);
        File.WriteAllText(DirectionPath + FileName, Json);
    }

    static public void Load()
    {
        string DirectionPath = Application.persistentDataPath + DirectionName;

        string FilePath = DirectionPath + FileName;

        if (File.Exists(FilePath))
        {
            PlayerNow = JsonUtility.FromJson<ListPlayer>(File.ReadAllText(FilePath));
        }
        else PlayerNow = new ListPlayer();
    }
}