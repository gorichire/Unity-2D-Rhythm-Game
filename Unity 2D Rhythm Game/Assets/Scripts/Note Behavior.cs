using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    // ��Ʈ ���� ������ 
    public int noteType;
    private GameManager.judges judge;
    private KeyCode KeyCode;

    // Start is called before the first frame update
    void Start()
    {
        if (noteType == 1) KeyCode = KeyCode.D;
        else if (noteType == 2) KeyCode = KeyCode.F;
        else if (noteType == 3) KeyCode = KeyCode.J;
        else if (noteType == 4) KeyCode = KeyCode.K;
    }

    public void Initialieze()
    {
        judge = GameManager.judges.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * GameManager.instance.notespeed);
        // ����ڰ� ��Ʈ Ű�� �Է��� ���
        if (Input.GetKey(KeyCode))
        {
            // �ش� ��Ʈ�� ���� ������ �����մϴ�.
            GameManager.instance.processJudge(judge, noteType);
            // ��Ʈ�� ���� ���� ��� ������ ���ķδ� �ش� ��Ʈ�� �����մϴ�.
            if (judge != GameManager.judges.NONE) gameObject.SetActive(false);
        }
    }

/*    public void Judge()
    {
        // �ش� ��Ʈ�� ���� ������ �����մϴ�.
        GameManager.instance.processJudge(judge, noteType);
        // ��Ʈ�� ���� ���� ��� ������ ���ķδ� �ش� ��Ʈ�� �����մϴ�.
        if (judge != GameManager.judges.NONE) gameObject.SetActive(false);
    }*/

    // �� ��Ʈ�� ���� ��ġ�� ���Ͽ� ������ �����մϴ�.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bad Line")
        {
            judge = GameManager.judges.BAD;
        }
        else if (other.gameObject.tag == "Good Line")
        {
            judge = GameManager.judges.GOOD;
        }
        else if (other.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judges.PERFECT;
            if (GameManager.instance.autoPerfect)
            {
                GameManager.instance.processJudge(judge, noteType);
                gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.tag == "Miss Line")
        {
            judge = GameManager.judges.MISS;
            GameManager.instance.processJudge(judge, noteType);
            gameObject.SetActive(false);
        }
    }
}
