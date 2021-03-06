﻿using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class Into : MonoBehaviour {

	private int _state = 0;
	private bool _faceRight = false;
	public int _pacecount = 0;
	private AudioClip boomshakalaka;

	private float timeLeft = 7f;

	private Animator _anim;
	private GameObject _object1;
	private GameObject _object2;
	private GameObject _object3;
	private GameObject _object4;
	private GameObject _object5;
	private GameObject _object6;
	private Text _narrator;
	private bool boolboomshakalaka = true;


	// Use this for initialization
	void Start () {

		_anim = GetComponent<Animator> ();
		_object1 = GameObject.FindGameObjectWithTag ("Text");
		_object1.SetActive (false);

		_object2 = GameObject.FindGameObjectWithTag ("LightBulb");
		_object2.SetActive (false);

		_object3 = GameObject.FindGameObjectWithTag ("ComputerText");
		_object3.SetActive (false);

		_object4 = GameObject.FindGameObjectWithTag ("QuestionMark");
		_object4.SetActive (false);

		_object5 = GameObject.FindGameObjectWithTag ("Narrator");
		_narrator = _object5.GetComponent<Text>();

		_object6 = GameObject.FindGameObjectWithTag ("Computer");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		StateMachine();
	}

	void StateMachine()
	{

		Vector2 Destination = new Vector2();
		float move = 2.0f;
		int maxpace = 1;
		float waittime = 1f;

		switch (_state) 
		{
		// walk tot he chalk board.
		case 0:
			//Set the new destination
			Destination.x = 4.23f;

			//orient the player in the right direction
			if(_faceRight) Flip(1);
			move = 2f;

			//set the velocity
			rigidbody2D.velocity = new Vector2(move, rigidbody2D.velocity.y);

			//check to see if the player has arrived at destination and made the max number of paces
			if (rigidbody2D.position.x >= Destination.x && _pacecount <=maxpace)
			{
				_state=1;
			}
			else if(_pacecount>maxpace)
			{
				_state = 2;
			}
			break;

		// pace in front of the chalk board.
		case 1:
			move = -2f;

			//orient the character correctly
			if(!_faceRight) Flip(-1);

			//set the new destination
			Destination.x = -0.75f;

			//Set the new velocity
			rigidbody2D.velocity = new Vector2(move,rigidbody2D.velocity.y);

			//Check to see if the player has arrived at the destination
			if(rigidbody2D.position.x <= Destination.x)
			{
				_state=0;
				_pacecount++;
			}
			break;

		// turn the light bulb on
		case 2:
			//change the text of the narrator
			_narrator.text = "In the wrong hands, it would spell doom for the future of humanity." +
				"This equation gave Feynman Jr. ultimate knowledge of everything, except how to properly secure his system.";

			//transitin the player to the standing animation
			_anim.SetBool("test", true);

			//make the lightbulb visible
			_object1.SetActive(true);

			//make the chalk board text visible
			_object2.SetActive(true);

			//make the player velocity zero
			rigidbody2D.velocity = new Vector2(0f,0f);

			//stay in this state for some time
			if(countdown())
			{
				timeLeft = waittime*7;
			 	_state = 3;
			}
			break;

		// Computer gets hacked
		case 3:
			// play the boomshakalaka clip once.
			if(boolboomshakalaka) 
			{
				_object6.GetComponent<AudioSource> ().Play ();
				boolboomshakalaka = false;	
			}

			// change the text of the narrator text
			_narrator.text = "Just as he saved his information onto his computer, his computer indicated that it had been hacked.";

			//Show the text on the computer screen.
			_object3.SetActive(true);

			//Stay in this state for some time
			if(countdown()) 
			{
				timeLeft = waittime;
				_state = 4;
			}
			break;

		//question mark
		case 4:
			// make the light bulb invisible
			_object2.SetActive(false);

			//make the question mark visible
			_object4.SetActive(true);

			//Stay in thhis state for some time
			if(countdown())
			{
				timeLeft = waittime;
				_state = 5;
			}
			break;

		//Walk over to the computer
		case 5:
			//set the new destination by the computer
			Destination.x = 6.09f;
			move = 2f;

			//make the transition to the walk animation
			_anim.SetBool("test",false);

			//set the x velocity
			rigidbody2D.velocity = new Vector2(move,rigidbody2D.velocity.y);

			//check to see if the player has arrived at destination
			if(rigidbody2D.position.x >= Destination.x)
			{
				_state=6;
			}
			break;

		//stop in front of computer
		case 6:
			//change the text of the narrator text
			_narrator.text = "Feynman Junior must now get his data back by hacking the hacker.";

			//stop the player
			rigidbody2D.velocity = new Vector2(0f,0f);

			//make the transition to the standing animation
			_anim.SetBool("test",true);

			//stay in this state for some time
			if(countdown())
			{
				_state = 7;
				timeLeft = waittime;
			}
			break;

		//walk to the door to start first level
		case 7:
			move = -2f;
			//set the new destingatino
			Destination.x = -3.75f;

			//make the transition to the walk animation
			_anim.SetBool("test", false);

			//flip the player
			if(!_faceRight) Flip(-1);

			//set the velocity
			rigidbody2D.velocity = new Vector2(move,rigidbody2D.velocity.y);

			//check to see if the player has arrived at the destinatino
			if(rigidbody2D.position.x <= Destination.x)
			{
				//go to the next level
				Application.LoadLevel("Level 1");
			}
			break;
     	}
	}

	bool countdown()
	{
		timeLeft -= Time.deltaTime;

		if (timeLeft < 0) return true;
		else return false;
	}
	
	void Flip(int scalevalue)
	{
		_faceRight = !_faceRight;
		Vector3 theScale = GameObject.FindGameObjectWithTag ("Player").transform.localScale;
		theScale.x = scalevalue;
		GameObject.FindGameObjectWithTag ("Player").transform.localScale = theScale;


	}
}
