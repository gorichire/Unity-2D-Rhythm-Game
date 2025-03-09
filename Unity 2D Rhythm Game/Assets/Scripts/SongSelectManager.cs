using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class SongSelectManager : MonoBehaviour
{
    public Text startUI;
    public Text disableAlertUI;
    public Image disablePanelUI;
    public Button purchaseButtonUI;
    private bool disabled = true;

    public Image musicImageUI;
    public Text musicTitleUI;
    public Text bpmUI;

    private int musicIndex;
    private int musicCount = 3;

    // ȸ������ ��� UI
    public Text userUI;
    private void UpdateSong(int musicIndex)
    {
        // ���� �ٲٸ�, �ϴ� �÷��� �Ҽ� ���� ���´�.
        disabled = true;
        disablePanelUI.gameObject.SetActive(true);
        disableAlertUI.text = "�����͸� �ҷ����� ���Դϴ�.";
        purchaseButtonUI.gameObject.SetActive(false);
        startUI.gameObject.SetActive(false);
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
        // ���̾�̽��� �����մϴ�.
        DatabaseReference reference;
        reference = FirebaseDatabase.DefaultInstance.GetReference("charges")
            .Child(musicIndex.ToString());
        // ������ ���� ��� �����͸� JSON ���·� �����´�.
        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // ���������� �����͸� ������ ���
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // �ش� ���� ���� ���� ���
                if(snapshot == null || !snapshot.Exists)
                {
                    disabled = false;
                    disablePanelUI.gameObject.SetActive(false);
                    disableAlertUI.text = "";
                    startUI.gameObject.SetActive(true);
                }
                else
                {
                    // ���� ����ڰ� ������ �̷��� �ִ� ��� ���� �÷��� �Ҽ� ����.
                    if (snapshot.Child(PlayerInformation.auth.CurrentUser.UserId).Exists)
                    {
                        disabled = false;
                        disablePanelUI.gameObject.SetActive(false);
                        disableAlertUI.text = "";
                        startUI.gameObject.SetActive(true);
                        purchaseButtonUI.gameObject.SetActive(false);
                    }
                    // ����ڰ� �ش� ���� �����ߴ��� Ȯ���Ͽ� ó��.
                    if (disabled)
                    {
                        purchaseButtonUI.gameObject.SetActive(true);
                        disableAlertUI.text = "�÷����� �� ���� ���Դϴ�.";
                        startUI.gameObject.SetActive(false);
                    }
                }
            }
        });
    }

    // ���� ������ ��� Charge Ŭ���� ����
    class Charge
    {
        public double timestamp;
        public Charge(double timestamp)
        {
            this.timestamp = timestamp;
        }
    }

    public void Purchase()
    {
        // �����ͺ��̼� ���� ����
        DatabaseReference reference = PlayerInformation.GetDatabaseReference();
        // ������ ������ �غ��ϱ�
        DateTime now = DateTime.Now.ToLocalTime();
        TimeSpan span = (now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        int timestamp = (int)span.TotalSeconds;
        Charge charge = new Charge(timestamp);
        string json = JsonUtility.ToJson(charge);
        // ��ŷ ���� ������ �����ϱ�
        reference.Child("charges").Child(musicIndex.ToString()).Child(PlayerInformation.auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
        UpdateSong(musicIndex);

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
        userUI.text = PlayerInformation.auth.CurrentUser.Email + " ��, ȯ���մϴ�.";
        musicIndex = 1;
        UpdateSong(musicIndex);
    }

    public void GameStart()
    {
        if (disabled) return;
        PlayerInformation.selectedMusic = musicIndex.ToString();
        SceneManager.LoadScene("GameScene");
    }

    public void LogOut()
    {
        PlayerInformation.auth.SignOut();
        SceneManager.LoadScene("LoginScene");
    }
}
