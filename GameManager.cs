using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
[SerializeField]
private bool _isGameOver; 
[SerializeField]
private GameObject _pauseMenuPanel;
[SerializeField]
private Animator _pauseAnimator;

private void Start()
{
   _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
   _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
}

 
private void Update()
{
 if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true) {
    SceneManager.LoadScene(1); // Current Game Scene
 }

if(Input.GetKeyDown(KeyCode.P))
{
   Time.timeScale = 0;
  _pauseMenuPanel.SetActive(true);
  _pauseAnimator.SetBool("isPause",true);
   
}     

if(Input.GetKeyDown(KeyCode.Escape)) 
{
 Application.Quit();  
}
} 

public void GameOver() 
{
   _isGameOver = true;
}

public void ResumeGame()
{
   _pauseMenuPanel.SetActive(false);
   Time.timeScale = 1;
} 
  

}

 