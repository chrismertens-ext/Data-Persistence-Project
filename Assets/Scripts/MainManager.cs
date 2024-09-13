using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public int playerScore;

    public Text ScoreText;
    public Text HighScoreText;
    public string playerName;
    public GameObject GameOverText;
    
    public bool m_Started = false;
    private int m_Points;
    public int m_highScore = 0;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "main");

        BrickInit();
        LoadScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }

        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                StartCoroutine(StartGame());
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;

        if (m_Points > m_highScore)
        {
            m_highScore = m_Points;
            GameOverText.GetComponent<Text>().text = "New High Score!\nPress Space to Restart";
            SaveScore();
        }

        GameOverText.SetActive(true);
    }

    private void BrickInit()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int playerScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerScore = m_highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + $"/{playerName}savefile.json", json);
    }

    public void LoadScore()
    {
        m_Points = 0;
        string path = Application.persistentDataPath + $"/{playerName}savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_highScore = data.playerScore;
            HighScoreText.text = $"High Score : {m_highScore}";
        }

        else
        {
            HighScoreText.text = $"High Score : 0";
        }
    }
}
