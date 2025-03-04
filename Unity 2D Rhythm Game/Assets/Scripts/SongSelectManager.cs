using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SongSelectManager : MonoBehaviour
{
    public Image musicImageUI;
    public Text musicTitleUI;
    public Text bpmUI;

    private int musicIndex;
    private int musicCount = 3;

    private void UpdateSong(int musicIndex)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        // ���ҽ����� ��Ʈ �ؽ�Ʈ ������ �ҷ��´�.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicIndex.ToString());
        StringReader stringReader = new StringReader(textAsset.text);
        // ù ��° �ٿ� ���� �� �̸��� �о UI�� ������Ʈ �մϴ�.
        musicTitleUI.text = stringReader.ReadLine();
        // �� ��° ���� �б⸸ �ϰ� �ƹ� ó�� ���� �ʱ�
        stringReader.ReadLine();
        // �� ��° �ٿ� ���� BPM�� �о� UI�� ������Ʈ �մϴ�.
        bpmUI.text = "BPM: " + stringReader.ReadLine().Split(' ')[0];
        // ���ҽ����� ��Ʈ ���� ������ �ҷ��� ����մϴ�.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicIndex.ToString());
        audioSource.clip = audioClip;
        audioSource.Play();
        // ���ҽ����� ��Ʈ �̹��� ������ �ҷ��ɴϴ�.
        musicImageUI.sprite = Resources.Load<Sprite>("Beats/" + musicIndex.ToString());
    }

    public void Right()
    {
        musicIndex = musicIndex + 1;
        if (musicIndex > musicCount) musicIndex = 1;
        UpdateSong(musicIndex);
    }
    public void Left()
    {
        musicIndex = musicIndex -1;
        if (musicIndex < 1) musicIndex = musicCount;
        UpdateSong(musicIndex);
    }
    void Start()
    {
        musicIndex = 1;
        UpdateSong(musicIndex);
    }

    public void GameStart()
    {
        PlayerInformation.selectedMusic = musicIndex.ToString();
        SceneManager.LoadScene("GameScene");
    }
}
