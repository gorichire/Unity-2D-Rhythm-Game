using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image RankUI;

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
    }

    public void Replay()
    {
        SceneManager.LoadScene("SongSelectScene");
    }
}
