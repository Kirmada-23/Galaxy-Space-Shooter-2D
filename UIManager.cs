using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText, bestText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private TextMeshProUGUI _gameOverText; 
     [SerializeField]
     private TextMeshProUGUI _reStartKey;
     private GameManager _gameManager;
  
     
   
      void Start()
    {
        _scoreText.text = "Score: " + 0; 
        _gameOverText.gameObject.SetActive(false); 
       _reStartKey.gameObject.SetActive(false); 
       _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>(); 

     if(_gameManager == null) {
      Debug.LogError("Game Manager is Null");
     }
    }
  
    public void UpdatedScore(int playerScore) 
    {
      _scoreText.text = "Score: " + playerScore.ToString();
    }
     
    // public void CheckForBestScore(int damage)
    // {
    // bestText.text = "Best: " + _bestScore.ToString();
    //   } 
   
public void UpdateLives(int cuurentLives) 
{
  _LivesImg.sprite = _livesSprites[cuurentLives];  
  if(cuurentLives==0) {
    GameOverSequence();
  } 
  }  
    
void GameOverSequence() 
{
  _gameManager.GameOver();
  _gameOverText.gameObject.SetActive(true);
    StartCoroutine(GameOverFlickerRoutine());
    _reStartKey.gameObject.SetActive(true);
}

IEnumerator GameOverFlickerRoutine() 
{
  while(true) 
  {
    _gameOverText.text = "GAME OVER";
    yield return new WaitForSeconds(0.5f);
    _gameOverText.text = "";
    yield return new WaitForSeconds(0.5f); 
  }
}

//Resume Play
public void ResumePlay()
{
  GameManager gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
  gm.ResumeGame();
}

// BackToMainMenu
public void BackToMainMenu()
{
 SceneManager.LoadScene("Game");
}

} 
           