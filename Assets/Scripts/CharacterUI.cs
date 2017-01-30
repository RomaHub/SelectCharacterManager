using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class CharacterUI : MonoBehaviour {

	public Image avatar;
	public Text textName;
	public Text textHP;
	public Button buttonAction;
	public Button buttonNext;

	private Character character;


	void OnEnable()
	{
		GameController.SelectCharacter+=ChangeCharacter;
		GameController.DeselectCharacter+=UnselectCharacter;
		Character.ChangeHealth+=RefreshHP;

		buttonNext.onClick.AddListener(GameController.Instance.NextCharacter);
	}

	void OnDisable()
	{
		GameController.SelectCharacter-=ChangeCharacter;
		GameController.DeselectCharacter-=UnselectCharacter;
		Character.ChangeHealth-=RefreshHP;

		buttonNext.onClick.RemoveListener(GameController.Instance.NextCharacter);
	}

	public bool Active()
	{
		return gameObject.activeSelf;
	}

	public void Show(Character character)
	{
		this.character = character;

		//subscribe
		buttonAction.onClick.AddListener(Damage);

		Refresh();

		gameObject.SetActive(true);
	}

	private void ChangeCharacter(object sender, EventArgs e)
	{
		buttonAction.onClick.RemoveAllListeners();

		character = (sender as GameController).SelectedCharacter;

		Show (character);
	}

	private void UnselectCharacter(object sender, EventArgs e)
	{
		character = null;
		
		Hide ();
	}


	public void Hide()
	{
		//unsubscribe
		buttonAction.onClick.RemoveAllListeners();

		gameObject.SetActive(false);
	}

	private void RefreshHP(object sender, EventArgs e)
	{
		textHP.text = character.data.health.ToString();
	}

	private void Refresh()
	{
		//check if character is null
		avatar.sprite = character.avatar;
		textName.text = character.data.name;
		textHP.text = character.data.health.ToString();


	}

	private void Damage()
	{
		character.Hit (4);
	}

}
