using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 4;
    public Vector3 LaunchOffset;
    public bool Thrown;
    public float Damage = 1;
    public float SplashRange = 1;
    
    void Start()
    {
        if (Thrown){
            
            
            var direction = -transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * Speed, ForceMode2D.Impulse); 
        }
        transform.Translate(LaunchOffset);
        Destroy(gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Thrown){
            Debug.Log(transform.right);
            
            transform.position += -transform.right * Speed * Time.deltaTime;
            
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (SplashRange > 0){
            var hitcolliders = Physics2D.OverlapCircleAll(transform.position,SplashRange);
            foreach (var hitcollider in hitcolliders){
                var enemy = hitcollider.gameObject.tag;
                if (enemy == "Enemy"){
                    var closest = hitcollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closest,transform.position);
                    var dmg = Mathf.InverseLerp(SplashRange,0,distance);
                    Debug.Log("Eneemy took hit");
                    Debug.Log(dmg);
                }
            }
        }
        else{
            var enemy = other.gameObject.tag;
            if (enemy == "Enemy"){
                Debug.Log("1 enemy took a hit");
            }
        }
        Debug.Log("destroying");
        Destroy(gameObject);
    }
}
