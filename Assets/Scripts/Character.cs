using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class Character : MonoBehaviour, IPointerClickHandler{

	public static event EventHandler ChangeHealth;


	public CharacterData data;

	public Sprite avatar;

	private bool isSelected;



	void Start()
	{
		avatar = GetComponent<SpriteRenderer>().sprite;

		GameController.Instance.AddCharacter(this);
	}

	void OnEnable()
	{
		EventManager.DamageEvent += Hit;
	}

	void OnDIsable()
	{
		EventManager.DamageEvent -= Hit;
	}

	public void Hit(int amount)
	{
		data.health -= amount;
		if(ChangeHealth!= null) ChangeHealth(this, EventArgs.Empty);
	}

	public void SetColor(Color color)
	{
		GetComponent<SpriteRenderer>().color = color;
	}

	public Color GetColor()
	{
		return GetComponent<SpriteRenderer>().color;
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		GameController.Instance.SelectedCharacter = this;
		//isSelected = true;
	}
	#endregion
}

[Serializable]
public class CharacterData
{
	public string name;
	public int health;

	public CharacterData()
	{
		name = "NoName";
		health = 10;
	}

	public CharacterData(string name, int health)
	{
		this.name = name;
		this.health = health;
	}


}
