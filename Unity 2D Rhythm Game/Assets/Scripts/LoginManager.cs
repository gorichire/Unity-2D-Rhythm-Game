using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    // ���̾�̽� ���� ��� ��ü
    private FirebaseAuth auth;

    // �̸��� �� �н����� UI
    public InputField emailInputField;
    public InputField passwordInputField;

    // �α��� ��� UI
    public Text messageUI;
    void Start()
    {
        // ���̾�̽� ���� ��ü�� �ʱ�ȭ �մϴ�.
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }

    public void Login()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        // ���� ��ü�� �̿��� �̸��ϰ� ��й�ȣ�� �α����� �����մϴ�.
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(
            task =>
            {
                if(task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                {
                    PlayerInformation.auth = auth;
                    SceneManager.LoadScene("SongSelectScene");
                }
                else
                {
                    messageUI.text = "������ �ٽ� Ȯ���� �ּ���.";
                }    
            }
            );
    }

    public void GoToJoin()
    {
        SceneManager.LoadScene("JoinScene");
    }
}
