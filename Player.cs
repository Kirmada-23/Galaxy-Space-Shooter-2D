using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
     private float _speed = 4f; 
     private float _speedMultiplier = 2;
    [SerializeField]
     private GameObject _laserPrefab;
     [SerializeField]
     private float _firerate = 0.5f;
    [SerializeField]
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField] 
    private bool _isTripleShotActive = false; 
    [SerializeField]
    private bool _isSpeedBoostActive = false; 
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]           
    private bool _isShieldActive = false; 
    [SerializeField]
    private GameObject _shieldVisualizer; 
    [SerializeField]
    private GameObject _leftEngine , _rightEngine ;
    [SerializeField]
    private int _score; 
     [SerializeField]
     private int _bestScore; 
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _laserSoundClip; 
    
    private AudioSource _audioSource;  
     void Start()
    {
       
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); 
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
       _audioSource = GetComponent<AudioSource>(); 
        if(_spawnManager==null) {
          Debug.LogError("The Spawn Manager is Null");
        }  
        if(_uiManager == null) 
        {
         Debug.Log("The UI Manager is null");
        }
        if(_audioSource == null)
        {
          Debug.LogError("Audio is not there"); 
        }  
        else 
        {
          _audioSource.clip = _laserSoundClip; 
        }
    }         
     
    void Update()
    { 
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.Space)&& Time.time > _canfire) {
          FireLaser();
        }
   
    } 
          
 
    void CalculateMovement () {
        float horizontalInput = Input.GetAxis("Horizontal");
         float verticalInput = Input.GetAxis("Vertical");
      
        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);
       
      transform.Translate(direction*_speed*Time.deltaTime);
       
       transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),0);
      
      if(transform.position.x > 11.3f) {
        transform.position = new Vector3(-11.3f,transform.position.y,0); 
      } 
      else if(transform.position.x < -11.3f) {
        transform.position = new Vector3(11.3f,transform.position.y,0);
      }
    }

    void FireLaser() {
          _canfire = Time.time + _firerate;

         if(_isTripleShotActive == true) {
            Instantiate(_tripleShotPrefab, transform.position,Quaternion.identity);
         } 
         else {
          Instantiate(_laserPrefab,transform.position + new Vector3(0,1.05f,0),Quaternion.identity); 
         } 
         _audioSource.Play();
           } 
                             
   public void Damage() {  

    if(_isShieldActive == true) {
      _isShieldActive = false;
      _shieldVisualizer.SetActive(false);
      return; 
    } 

  _lives--;  
     
   if(_lives == 2) {
    _leftEngine.SetActive(true); 
   } 
   else if(_lives == 1) {
    _rightEngine.SetActive(true);
   }

    _uiManager.UpdateLives(_lives);  
      if(_lives <= 0) {
     _spawnManager.OnPlayerDeath(); 
    //  _uiManager.CheckForBestScore(); 
      Destroy(this.gameObject);
      
     } 
        
    }      

    public void TripleShotActive() 
    {
      _isTripleShotActive = true; 
      StartCoroutine(TripleShotPowerDownRoutine());
    }

   IEnumerator TripleShotPowerDownRoutine() 
   {
    yield return new WaitForSeconds(5.0f); 
    _isTripleShotActive = false;
   }

public void SpeedBoostActive() {
  _isSpeedBoostActive = true; 
  _speed*= _speedMultiplier;
  StartCoroutine(SpeedBoostPowerDownRoutine());
}

IEnumerator SpeedBoostPowerDownRoutine () {
  yield return new WaitForSeconds(5.0f);
  _isSpeedBoostActive = false; 
  _speed/= _speedMultiplier;
} 

public void ShieldsActive() {
  _isShieldActive = true;
  _shieldVisualizer.SetActive(true);
}

public void AddScore(int points) 
{
  _score += points;
  _uiManager.UpdatedScore(_score);
}

// public void bestScore(int _score)
// {
//    if(_score >_bestScore )
//    {
//      _bestScore = _score;
//      _uiManager.CheckForBestScore(_bestScore);
       
//    }
// }
// Line 126 also comment out after solving issue
}  
                             