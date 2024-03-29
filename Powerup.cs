using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
  [SerializeField]
   private float _speed = 3.0f;    
  [SerializeField] // 0 = TripleShot, 1= Speed, 2 = Shileds
  private int _powerupID; 
  [SerializeField]
  private AudioClip _audioClip; 
    void Start()
    {
       
    }
     void Update()
    {
        transform.Translate(Vector3.down*_speed*Time.deltaTime);
       if(transform.position.y <= -5.5f) {
        Destroy(this.gameObject);
       } 
    } 

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") { 
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioClip,transform.position);
            if(player!= null) { 
                switch(_powerupID) 
                 {
                 case 0:
                 player.TripleShotActive();
                 break;
                 case 1:
                 player.SpeedBoostActive();
                 break;
                 case 2:
                 player.ShieldsActive();
                 break;
                 default: 
                 Debug.Log("Default Value");
                 break;
                }
            } 
            } 
            
          Destroy(this.gameObject);
    
}
}          