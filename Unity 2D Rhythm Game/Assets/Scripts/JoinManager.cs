using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviour
{
    // ���̾�̽� ���� ��� ��ü
    private FirebaseAuth auth;

    // �̸��� �� �н����� UI
    public InputField emailInputField;
    public InputField passwordInputField;

    // ȸ������ ��� UI
    public Text messageUI;

    void Start()
    {
        // ���̾�̽� ���� ��ü�� �ʱ�ȭ �մϴ�.
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }

    bool InputCheck()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        if(email.Length < 8)
        {
            messageUI.text = "�̸����� 8�� �̻����� �����Ǿ�� �մϴ�.";
            return false;
        }
        else if(password.Length < 8)
        {
            messageUI.text = "��й�ȣ�� 8�� �̻����� �����Ǿ�� �մϴ�.";
            return false;
        }
        messageUI.text = "";
        return true;
    }

    public void Check()
    {
        InputCheck();
    }

    public void Join()
    {
        Debug.Log("Join() ȣ���");
        if (!InputCheck())
        {
            return;
        }
        string email = emailInputField.text;
        string password = passwordInputField.text;
        // ���� ��ü�� �̿��� �̸��ϰ� ��й�ȣ�� ȸ�������� �����մϴ�.
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(
            task =>
            {
                if(!task.IsCanceled && !task.IsFaulted)
                {
                    SceneManager.LoadScene("LoginScene");
                }
                else
                {
                    messageUI.text = "�̹� ��� ���̰ų� ������ �ٸ��� �ʽ��ϴ�.";
                }
            }
            );
    }
    public void Back()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
