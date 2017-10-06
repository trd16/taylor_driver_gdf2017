using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour {

	public float speed;				//Floating point variable to store the player's movement speed.
	public Text MeteorsText;                                        //Store a reference to the UI Text component which will display the number of pickups collected.
	public Text winText;
    public Text loseText;
    public Text StarsText;
    public Text ScoreText;                                        //Store a reference to the UI Text component which will display the 'You win' message.

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int Meteors;
    private int Stars;
    private int MeteorsScore;
    private int StarsScore;
    private int scorevalue;

    
    
    //Integer to store the number of pickups collected so far.

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		//Initialize count to zero.
		Meteors = 0;
        Stars = 0;
        MeteorsScore = 0;
        StarsScore = 0;
    
        scorevalue = StarsScore + MeteorsScore;

		//Initialze winText to a blank string since we haven't won yet at beginning.
		winText.text = "";
        loseText.text = "";
        
		//Call our SetCountText function which will update the text with the current value for count.
		SetStarsText ();
        SetMeteorsText();
        SetScoreText();
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rb2d.AddForce (movement * speed);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("Meteors")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);
			
			//Add one to the current value of our count variable.
			Meteors = Meteors + 1;
            MeteorsScore = Meteors * - 2;
            scorevalue =  MeteorsScore;
			
			//Update the currently displayed count by calling the SetCountText function.
			SetMeteorsText ();
            SetScoreText();
		}
        if (other.gameObject.CompareTag ("Stars"))
        {
            other.gameObject.SetActive(false);

            Stars = Stars + 1;
            StarsScore = Stars * 2;
            scorevalue = MeteorsScore + StarsScore;

            SetStarsText();
            SetScoreText();
        }

		

	}

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	void SetMeteorsText()
	{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		MeteorsText.text = "Meteors: " + Meteors.ToString ();
        

      
	}
    void SetStarsText()
    {
        StarsText.text = "Stars: " + Stars.ToString();

    }
    void SetScoreText ()
    {
        ScoreText.text = "Score: " + scorevalue.ToString();

        //Check if we've collected all 12 pickups. If we have...
        if (scorevalue == 20)
        { //... then set the text property of our winText object to "You win!"
            winText.text = "You win!";
            speed = 0f;
        }
        else if (scorevalue == -20)
        {
            loseText.text = "You lose :(";
            speed = 0f;
        }

        
    }
}
