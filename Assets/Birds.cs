using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Birds : MonoBehaviour
{
    private Vector3 _initialPosition;
    //Serializer is added for game designers not for programmers
   [SerializeField] private float _launchPower = 300;
                    private float _timeSittingAround;
    //Add a boolean variable to see if the bird is launched or not(always false->default)
    private bool _birdLaunched;
    

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //update line renderer
        // index one is our starting postion 
        //index zero is our bird current poistion also fixes the arrow
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);


        //check if the body if the body is sitting with no velocity after launching.
        //Time.deltaTime is the time since the frame is launched (reme--> update is called once each frame)
        if (_birdLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1) {
            _timeSittingAround += Time.deltaTime;
            //time  = 1 / the number of frames per second
        }
        
        
        //To fix the boundaries reset the game every time the bird flies out of the frame at a certain value
        if (transform.position.y < -10 ||
            transform.position.y > 10 ||
            transform.position.x < -10 ||
            transform.position.x > 10 ||
            _timeSittingAround > 3) {
            string CurrentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(CurrentSceneName);
        }

    }
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //turn the line renderer off after the bird is launched
       // GetComponent<LineRenderer>().enabled = false;
    }
    private void OnMouseUp()
    {
         Vector2 DirectionToInitialPosition = _initialPosition - transform.position;
         GetComponent<SpriteRenderer>().color = Color.blue;
        //To calculate the diatance of the throw off.
        GetComponent<Rigidbody2D>().AddForce(DirectionToInitialPosition * _launchPower);
        //set gravity back to 1 so make the bird fall on the ground after falling
        GetComponent<Rigidbody2D>().gravityScale = 1;
        //when the scalegarvitychanges from 0 to 1 the bird is launched
        _birdLaunched = true;
        //Line renderer must show if the bird is not launched
       GetComponent<LineRenderer>().enabled = false;

    }
    private void OnMouseDrag() {

        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(newPosition.x, newPosition.y);
        GetComponent<LineRenderer>().enabled = true;

    }
   

}
