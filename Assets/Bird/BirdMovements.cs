using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovements : MonoBehaviour
{
    Animator animator;
    public GameObject platform; 

    public float max_velocity = 0.00001f;
    public float max_force = 0.03f;
    public float mass = 15; 
    public float platform_y_offset = 0.32f; 

    private Vector3 velocity;
    private Vector3 steering;
    private Vector3 target_position;
    private Vector3 curr_position;
    private float time_cycle;
    private float target_X; 

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        target_position = transform.position;
        curr_position = transform.position;
        animator = GetComponent<Animator>();        
        time_cycle=0f;
        target_X=-3; 
    }

    // Update is called once per frame
    void Update()
    {
        //set animation timer
        time_cycle = Time.time%60; 
        animator.SetFloat("timeFrame", time_cycle); 

        if(time_cycle >40){
            animator.SetBool("explore", true);
            animator.SetBool("settle", false);
        }
        else{
            animator.SetBool("settle", true);
            animator.SetBool("explore", false);
        }

        //seeking behaviour
        transform.position = transform.position + velocity; 
        curr_position = new Vector3(transform.position.x,transform.position.y,transform.position.z); 

        if(animator.GetBool("explore")){
            animator.SetBool("contactPlatform", false);
            //In explore mode, target location toggles betweeen 3 & -3 on x location 
            if(transform.position.x > target_X-0.1 && target_X==3){
                target_X=-3; 
                transform.rotation = Quaternion.Euler(new Vector3(0,-90,0));

            }
            else if(transform.position.x < target_X+0.1 && target_X==-3){
                target_X=3; 
                transform.rotation = Quaternion.Euler(new Vector3(0,90,0));

            }
            target_position = new Vector3(target_X,1,-10); 
        }

        if(animator.GetBool("settle")){
            if (animator.GetBool("contactPlatform"))
            {
                animator.SetBool("contactWater", false);
                //Y axis to follow platform height so bird stays on surface of platform
                target_position = new Vector3(platform.transform.position.x,platform.transform.position.y+platform_y_offset,platform.transform.position.z); 
            }
            else if(animator.GetBool("contactWater"))
            {
                //don't change Y axis, stay on surface of water
                target_position = new Vector3(platform.transform.position.x,0,platform.transform.position.z); 
                transform.rotation *= Quaternion.Euler(0, 0.1f, 0);
            }
            else{
                transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
                target_position = new Vector3(platform.transform.position.x,platform.transform.position.y,platform.transform.position.z); 
            }
        }

        // velocity = (target_position - curr_position).normalized * max_velocity;
        var desired_velocity = (target_position - curr_position).normalized * max_velocity;
        steering = desired_velocity - velocity;
        steering = Vector3.ClampMagnitude(steering, max_force); 
        steering = steering / mass; 
        velocity = Vector3.ClampMagnitude(velocity + steering, max_velocity); 
        transform.position = transform.position + velocity; 
    }

   private void OnTriggerEnter(Collider other){
       if(other.gameObject.tag=="sea"){ 
           // https://docs.unity3d.com/ScriptReference/Animator.SetBool.html
        //    
           animator.SetBool("contactWater", true);
           Debug.Log("Contact sea");
       }
       if(other.gameObject.tag=="platform"){
            // https://docs.unity3d.com/ScriptReference/Animator.SetBool.html
           animator.SetBool("contactPlatform", true);
           Debug.Log("Contact platform");
       }
   }

}
