using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void DamageHandler(int i);
	public static event DamageHandler DamageEvent;

	public void OnDamageEvent()
	{
		if(DamageEvent!=null)
			DamageEvent(1);
	}


	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.E))
		{
			OnDamageEvent();
		}
	}

}
