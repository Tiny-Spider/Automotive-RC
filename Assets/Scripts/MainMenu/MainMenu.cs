using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//private Rect mainMenuBox;
	//private Rect mainMenuGroup;

	//private Rect playButton;
	//private Rect optionsButton;
	//private Rect creditsButton;
	//private Rect exitButton;

	public Animator animator;

	public MainMenu()
	{

	}

	public void ToggleAnimator(string trigger)
	{
		if (!animator.GetBool(trigger))
			animator.SetTrigger(trigger);
		else
			animator.ResetTrigger(trigger);
	}

	//// Use this for initialization
	//void Start () {
	
	//}
	
	//// Update is called once per frame
	//void Update () {
	
	//}

	public void PlayButton()
	{
		
		//Application.LoadLevel("Dev");
		//Debug.Log("Play");
	}

	public void OptionsButton()
	{

	}

	public void Credits()
	{

	}

	public void ExitButton()
	{
		Application.Quit();
		Debug.Log("Exit");
	}
}
