using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    GameManager gameManager;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public AudioClip[] audioClips;
    public Text ScoreText;
    public Text highScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    private readonly char pad = '0';
    private List<Brick> bricks= new List<Brick>();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        AudioSFX.instance.PlayBackground(audioClips[0]);
        string str = $"{gameManager.playerHiScore}";
        highScoreText.text = $"HIGH SCORE {str.PadLeft(5, pad)} {gameManager.playerName}";
        gameManager.currentScore = 0;
        str = gameManager.currentScore.ToString();
        ScoreText.text = $"Score {str.PadLeft(5, pad)}";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        bricks.Clear();
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 3f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i] * 10;
                brick.onDestroyed.AddListener(AddPoint);
                brick.onChange.AddListener(ChangeBricksList);
                bricks.Add(brick);
            }
        }
    }
    private void ChangeBricksList(Brick brick)
    {
        bricks.Remove(brick);
        Debug.Log(bricks.Count);
        if (bricks.Count == 0) { Debug.Log("New Level !!"); }
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        gameManager.currentScore = m_Points;
        string str = $"{gameManager.currentScore}";
        ScoreText.text = $"Score {str.PadLeft(5, pad)}";
    }

    public void GameOver()
    {
        gameManager.SaveGame();
        m_GameOver = true;
        GameOverText.SetActive(true);
        AudioSFX.instance.PlayBackground(audioClips[1]);
    }
}
