using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        BestScoreManager.SharedInstance.LoadBestScore();
        if (BestScoreManager.SharedInstance.bestPlayerName != null && BestScoreManager.SharedInstance.bestPlayerName != "")
        {
            bestScoreText.text = $"{BestScoreManager.SharedInstance.bestPlayerName}: {BestScoreManager.SharedInstance.bestPlayerScore} points";
        } else
        {
            bestScoreText.text = "Best Score";
        }
        bestScoreText.maxVisibleLines = 1;
    }

    private void Awake()
    {
        if (BestScoreManager.SharedInstance.currentPlayerName != null && BestScoreManager.SharedInstance.currentPlayerName != "")
        {
            inputField.text = BestScoreManager.SharedInstance.currentPlayerName;
        }
    }

    public void StartButtonPressed()
    {
        if (inputField.text != "")
        {
            BestScoreManager.SharedInstance.currentPlayerName = inputField.text;
            BestScoreManager.SharedInstance.SaveBestScore();
            SceneManager.LoadScene(1);
        }
    }

    public void ExitButtonPressed()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
    }
}
