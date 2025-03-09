using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image RankUI;

    public Text rank1UI;
    public Text rank2UI;
    public Text rank3UI;


    void Start()
    {

        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "����: " + (int) PlayerInformation.score;
        maxComboUI.text = "�ִ� �޺�: " + PlayerInformation.maxCombo;
        // ���ҽ����� ��Ʈ �ؽ�Ʈ ������ �ҷ��ɴϴ�.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        // ù ���� �ٰ� �� ��° �� ����
        reader.ReadLine();
        reader.ReadLine();
        // �� ��° �ٿ� ���� ��Ʈ ���� (��ũ ����)�� �д´�.
        string beatInformation = reader.ReadLine();
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[3]);
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]);
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[5]);
        // ������ �´� ��ũ �̹����� �ҷ��ɴϴ�.
        if(PlayerInformation.score >= scoreS)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank S");
        }
        else if (PlayerInformation.score >= scoreA)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank A");
        }
        else if (PlayerInformation.score >= scoreB)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank B");
        }
        else
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank C");
        }
        rank1UI.text = "�����͸� �ҷ����� ���Դϴ�.";
        rank2UI.text = "�����͸� �ҷ����� ���Դϴ�.";
        rank3UI.text = "�����͸� �ҷ����� ���Դϴ�.";
        DatabaseReference reference = PlayerInformation.GetDatabaseReference().Child("ranks")
            .Child(PlayerInformation.selectedMusic);
        // ������ ���� ��� �����͸� JSON ���·� �����ɴϴ�.
        reference.OrderByChild("score").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // ���������� �����͸� ������ ���
            if (task.IsCompleted)
            {
                List<string> rankList = new List<string>();
                List<string> emailList = new List<string>();
                DataSnapshot snapshot = task.Result;
                // JSON �������� �� ���ҿ� �����մϴ�.
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary rank = (IDictionary)data.Value;
                    emailList.Add(rank["email"].ToString());
                    rankList.Add(rank["score"].ToString());
                }
                // ���� ���� ������ ������ �������� �����մϴ�.
                emailList.Reverse();
                rankList.Reverse();
                // �ִ� ���� 3���� ������ ���ʴ�� ȭ�鿡 ����մϴ�.
                rank1UI.text = "�÷����� ����ڰ� �����ϴ�.";
                rank2UI.text = "�÷����� ����ڰ� �����ϴ�.";
                rank3UI.text = "�÷����� ����ڰ� �����ϴ�.";
                List<Text> textList = new List<Text>();
                textList.Add(rank1UI);
                textList.Add(rank2UI);
                textList.Add(rank3UI);
                int count = 1;
                for(int i = 0; i < rankList.Count && i < 3; i++)
                {
                    textList[i].text = count + "�� : " + emailList[i] + " (" + rankList[i] + " ��)";
                    count = count + 1;
                }
            }
        });
    }

    public void Replay()
    {
        SceneManager.LoadScene("SongSelectScene");
    }
}
