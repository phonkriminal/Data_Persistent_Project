using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    private InputField playerName;
    [SerializeField]
    private Text hiScoreText;
    public AudioClip[] audioClips;
    private char pad = '0';
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.LoadGame();
        string str = $"{gameManager.playerHiScore}";
        hiScoreText.text = $"HI-SCORE   {str.PadLeft(5, pad)}";
        playerName.text = gameManager.playerName;
        if (audioClips.Length != 0)
        {
            AudioSFX.instance.PlayBackground(audioClips[0]);
        }
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(playerName.text))
        {
            return;
        }

        gameManager.playerName = playerName.text;

        SceneManager.LoadScene(1);

    }
    public void ExitGame()
    {
        gameManager.ExitGame();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
