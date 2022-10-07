using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaEdit : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform entryBoxPlayer;
    public Transform entryDetailPlayer;

    public List<Player> entryPlayerList;
    public List<Transform> TransList;
    public List<Player> ListPl;

    void Start()
    {
        UpdateDataBox();
        entryPlayerList = new List<Player>();
        TransList = new List<Transform>();
        entryDetailPlayer.gameObject.SetActive(false);
    }

    private void Update()
    {

    }

    public void UpdateDataBox()
    {
        SavingPlayer.Load();
        TransList.Clear();
        entryPlayerList.Clear();

        ListPl = new List<Player>();
        ListPl = SavingPlayer.PlayerNow.listPlayer;

        foreach (var Element in ListPl)
        {
            entryPlayerList.Add(Element);
        }

        for (int i = 0; i < entryPlayerList.Count; i++)
            for (int j = i + 1; j < entryPlayerList.Count; j++)
            {
                if (entryPlayerList[i].ID == "" && entryPlayerList[j].ID != "")
                {
                    Debug.Log("null");
                    Player Between = entryPlayerList[i];
                    entryPlayerList[i] = entryPlayerList[j];
                    entryPlayerList[j] = Between;
                }
                if (entryPlayerList[i].Score <= entryPlayerList[j].Score)
                {
                    if (entryPlayerList[j].ID == "" && entryPlayerList[i].ID != "") continue;

                    if (entryPlayerList[i].Score == entryPlayerList[j].Score && entryPlayerList[i].Time < entryPlayerList[j].Time)
                        continue;

                    Player Between = entryPlayerList[i];
                    entryPlayerList[i] = entryPlayerList[j];
                    entryPlayerList[j] = Between;
                }
            }

        for (int i = 0; i < Mathf.Min(entryPlayerList.Count, 7); i++)
        {
            CreateBoxForPlayer(entryPlayerList[i], entryBoxPlayer, TransList);
        }
    }

    private void CreateBoxForPlayer(Player Player, Transform TransformElement, List<Transform> transformList)
    {
        float HeightBox = 75f;
        Transform entryTransform = Instantiate(entryDetailPlayer, TransformElement);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

        entryRectTransform.anchoredPosition = new Vector2(0, -HeightBox * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        string NamePlayer = Player.Name;
        entryTransform.Find("NamePlayer").GetComponent<Text>().text = NamePlayer;

        Color Key = new Color();
        if (transformList.Count == 0) Key = new Color32(255, 255, 0, 255);
        if (transformList.Count == 1) Key = new Color32(190, 151, 169, 255);
        if (transformList.Count == 2) Key = new Color32(117, 172, 255, 255);
        if (transformList.Count > 2) Key = new Color32(253, 253, 253, 253);
        entryTransform.Find("BoxPlayer").GetComponent<Image>().color = Key;

        string IdPlayer = Player.ID;
        if (Player.ID == "")
        {
            IdPlayer = "Test Game";
            entryTransform.Find("IDPlayer").GetComponent<Text>().color = new Color32(255, 92, 90, 255);
        }
        entryTransform.Find("IDPlayer").GetComponent<Text>().text = IdPlayer;

        string ScorePlayer = Player.Score.ToString();
        entryTransform.Find("ScorePlayer").GetComponent<Text>().text = ScorePlayer;

        string RankPlayer = (transformList.Count + 1).ToString();
        entryTransform.Find("RankPlayer").GetComponent<Text>().text = RankPlayer;

        string TimePlayer = Player.Time.ToString("F");
        entryTransform.Find("TimePlayer").GetComponent<Text>().text = TimePlayer;

        TransList.Add(entryTransform);
    }
}
