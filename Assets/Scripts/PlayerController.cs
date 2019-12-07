using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject m_Car;
	public GameObject m_Human;

    enum PlayerMode
	{
        Car,
        Human,
	};
	private PlayerMode m_PlayerMode;

	public Vector3 m_Velocity;
	public Vector3 m_AngularVelocity;

	// Start is called before the first frame update
	void Start()
    {
		this.SetPlayerMode(PlayerMode.Car,true);
	}

	void FixedUpdate()
    {
		this.SaveRigidbody();

		if (Input.GetKey(KeyCode.Alpha1))
		{
			this.SetPlayerMode(PlayerMode.Car);
		}
		if (Input.GetKey(KeyCode.Alpha2))
		{
			this.SetPlayerMode(PlayerMode.Human);
		}
        
	}

	void SetPlayerMode(PlayerMode nextPlayerMode,bool isForced=false)
	{
		PlayerMode previousPlayerMode = this.m_PlayerMode;
		if (previousPlayerMode!=nextPlayerMode || isForced)
		{
			this.m_PlayerMode = nextPlayerMode;
			this.m_Car.SetActive(this.m_PlayerMode==PlayerMode.Car);
			this.m_Human.SetActive(this.m_PlayerMode == PlayerMode.Human);
			this.LoadRigidbody();
		}
	}

	void SaveRigidbody()
	{
		GameObject activeGameObject = this.getActiveGameObject();
		if (activeGameObject != null)
		{
			Rigidbody rigidbody = activeGameObject.GetComponent<Rigidbody>();
			this.m_Velocity = rigidbody.velocity;
			this.m_AngularVelocity = rigidbody.angularVelocity;
			this.transform.SetPositionAndRotation(activeGameObject.transform.position, activeGameObject.transform.rotation);
		}
	}

	void LoadRigidbody()
	{
		GameObject activeGameObject = this.getActiveGameObject();
		if (activeGameObject != null)
		{
			Rigidbody rigidbody = activeGameObject.GetComponent<Rigidbody>();
			rigidbody.velocity = this.m_Velocity;
			rigidbody.angularVelocity=this.m_AngularVelocity;
			activeGameObject.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
		}

	}
	GameObject getActiveGameObject()
	{
		switch (this.m_PlayerMode)
		{
			case PlayerMode.Car:
				return this.m_Car;
			case PlayerMode.Human:
				return this.m_Human;
			default:
				return null;
		}

	}
}
