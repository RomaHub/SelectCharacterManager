using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	private int i = 0;

	public static event EventHandler SelectCharacter;
	public static event EventHandler DeselectCharacter;

	public void OnSelectCharacter()
	{
		if(SelectCharacter!=null)
			SelectCharacter(this, EventArgs.Empty);
	}

	public void OnDeselectCharacter()
	{
		if(DeselectCharacter!=null)
			DeselectCharacter(this, EventArgs.Empty);
	}

	private static GameController _instance;
	public static GameController Instance
	{
		get{ return _instance; }
	}

	private Character selectedCharacter;

	public Character SelectedCharacter
	{
		get
		{
			return selectedCharacter;
		}
		set
		{
			if(selectedCharacter!=null) selectedCharacter.SetColor(defaultColor);
			if(selectedCharacter == value)
			{
				selectedCharacter = null;
				OnDeselectCharacter();
				return;
			}
			selectedCharacter = value;
			defaultColor = selectedCharacter.GetColor();
			selectedCharacter.SetColor(selectedColor);
			OnSelectCharacter();
		}
	}

	private Color defaultColor;
	public Color selectedColor;

	public CharacterUI characterUI;

	private List<Character> characters = new List<Character>();

	//Iterator?
	public void NextCharacter()
	{
		i = characters.IndexOf(selectedCharacter);
		i++;
		if(i>=characters.Count) i=0;
		SelectedCharacter = characters[i];


	}


	public void AddCharacter(Character c)
	{
		characters.Add(c);
	}

	//remove character

	//clear list

	void Awake()
	{
		if(_instance != null && _instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}

	}

	void Start()
	{
		characterUI.gameObject.SetActive(false);


	}


	void Update()
	{
//		if(Input.GetKeyDown(KeyCode.I))
//		{
//			characterUI.gameObject.SetActive(!characterUI.gameObject.activeSelf);
//		}

		if(Input.GetKeyDown(KeyCode.I))
		{
			if(characterUI.Active())
			{
				characterUI.Hide();
			}
			else if(SelectedCharacter!= null)
			{

				characterUI.Show(SelectedCharacter);
				//OnSelectCharacter();
			}
		}
	}

}
