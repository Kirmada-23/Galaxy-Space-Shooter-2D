using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    private Player _player;
    private Animator _enemyAnim;
    private AudioSource _audioSource; 
    private float _firerate = 3.0f;
    private float _canfire = -1; 
   
       void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>(); 
        if(_player==null){
          Debug.LogError("Player not found");
        }  
        _enemyAnim = GetComponent<Animator>();
        if(_enemyAnim==null){
          Debug.LogError("No Animation");
        }
    }      
  void Update()
    {
        CalculateMovement();
        if(Time.time > _canfire)
        {
         _firerate = Random.Range(3f,7f);
         _canfire = Time.time + _firerate; 
         
         GameObject enemyLaser =  Instantiate(_laserPrefab,transform.position - new Vector3(0,1,0),Quaternion.identity);
        
       
        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

        for(int i = 0; i < lasers.Length; i++)
        {
          lasers[i].AssignEnemyLaser();
        }
        } 
    }    
              
    void CalculateMovement()
    {
     transform.Translate(Vector3.down*_speed*Time.deltaTime);
        if(transform.position.y <-5.0f) {
          float randomX = Random.Range(-8f,8f); 
        transform.position = new Vector3(randomX,7,0);
        }
    }  
    private void OnTriggerEnter2D(Collider2D other) {
      if(other.tag == "Player") {
          Player player = other.transform.GetComponent<Player>();
        if( player != null){
          player.Damage();
        }
      _enemyAnim.SetTrigger("OnEnemyDeath");
      _speed = 0;
      _audioSource.Play();
        Destroy(this.gameObject,2.8f);
       }
        if(other.tag == "Laser") {
        Destroy(other.gameObject);
        if(_player != null) {
          _player.AddScore(10);
        }
        _enemyAnim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        _audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject,2.8f);
       }
    }
    
  
}   
             