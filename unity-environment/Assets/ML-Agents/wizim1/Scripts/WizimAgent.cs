using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizimAgent : Agent
{
    [Header("Specific to woman")]
    public GameObject woman;

	[Header("Specific to ground")]
	public GameObject ground;

	[Header("Specific to killer")]
	public GameObject killer;

	public static float DIS_LIMIT_TO_WOMAN = 1.1f;

	public long tick = 0;

    public override List<float> CollectState()
    {
        List<float> state = new List<float>();
		//state.Add(gameObject.transform.position.x - ground.transform.position.x);
		//state.Add(gameObject.transform.position.z - ground.transform.position.z);
        state.Add((woman.transform.position.x - gameObject.transform.position.x));
        state.Add((woman.transform.position.z - gameObject.transform.position.z));
		state.Add((killer.transform.position.x - gameObject.transform.position.x));
		state.Add((killer.transform.position.z - gameObject.transform.position.z));


		//state.Add((killer.transform.position.x - woman.transform.position.x));
		//state.Add((killer.transform.position.z - woman.transform.position.z));

		/*
		float sin = (Mathf.Sin(tick / 100f));
		state.Add(sin);

		state.Add(tick / 1000f);
*/
		/*
		float sin2 = (Mathf.Sin(tick / 50f));
		state.Add(sin2);
		*/

		/*
		Vector2 v = new Vector2 (woman.transform.position.x - gameObject.transform.position.x, woman.transform.position.y - gameObject.transform.position.y);
		Vector2 v2 = new Vector2 (killer.transform.position.x - gameObject.transform.position.x, killer.transform.position.y - gameObject.transform.position.y);
		state.Add(Vector2.Angle (v, v2));
		*/


		state.Add(gameObject.transform.GetComponent<Rigidbody>().velocity.x);
		state.Add(gameObject.transform.GetComponent<Rigidbody>().velocity.z);



        return state;
    }

	private Vector3 lastDis = Vector3.zero;
	private Vector3 lastKillDis = Vector3.zero;

    // to be implemented by the developer
    public override void AgentStep(float[] act)
    {
		
		tick++;


        float action_z = act[0];
		action_z = Mathf.Clamp (action_z, -1f, 1f);
		//action_z *= 1f;
		gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.forward * action_z);


		//gameObject.transform.position += (Vector3.forward * action_z);

        float action_x = act[1];
		action_x = Mathf.Clamp (action_x, -1f, 1f);
		//action_x *= 1f;
		gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.right * action_x);


		//gameObject.transform.position += (Vector3.right * action_x);


			
		Vector3 dis = woman.transform.position - gameObject.transform.position;
		dis.y = 0;

		Vector3 disKiller = killer.transform.position - gameObject.transform.position;
		disKiller.y = 0;

		/*
		if (tick > 10 * 60) {
			reward = -100f;
			done = true;
		}
		else 
		*/
		/*
		if (tick > 10 * 60) {
			reward = -0.5f;
			done = true;
		}
		else 
		*/

		reward = -0.001f;

		if (gameObject.transform.position.y - ground.transform.position.y < -2f) {
			//reward = -0.5f;
			done = true;
		}
		else if (disKiller.magnitude < WizimAcademy.scale) {
			reward = -0.5f;
			done = true;
		} 
		else if (dis.magnitude < DIS_LIMIT_TO_WOMAN) {
			reward = 1f;
			done = true;
		} else {

			/*
			if (lastDis != Vector3.zero && lastKillDis != Vector3.zero) {
				if (dis.magnitude < lastDis.magnitude) {
					reward += (lastDis.magnitude - dis.magnitude) * 0.01f;
				}

				if (disKiller.magnitude < 3f) {
					reward -= (lastKillDis.magnitude - disKiller.magnitude) * 0.01f;
				}
			}
			*/



			/*
			if (disKiller.magnitude < 2f) {
				reward -= 0.01f;
			}
			else if (dis.magnitude < 2f) {
				reward += 0.01f;
			}
			*/
			/*
			reward -= dis.magnitude / 1000f;
			reward += disKiller.magnitude / 1000f;
			*/
		}

		lastDis = dis;
		lastKillDis = disKiller;

		//Monitor.Log ("reward", reward, MonitorType.hist, transform);
    }

    // to be implemented by the developer
    public override void AgentReset()
    {
		tick = 0;

		lastDis = Vector3.zero;
		lastKillDis = Vector3.zero;
			
		killer.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
		killer.transform.position = new Vector3 (Random.Range (-5f, 5f), 1f, Random.Range (-5f, 5f)) + ground.transform.position;
		killer.transform.localScale = Vector3.one * WizimAcademy.scale;

		//killer.transform.position = new Vector3 (8f, 1f, 8f) + ground.transform.position;

		Vector3 dis;
		Vector3 dis2;
		do {
			woman.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
			woman.transform.position = new Vector3(Random.Range(-8, 8f), 1f, Random.Range(-8f, 8f)) + ground.transform.position;

			dis = woman.transform.position - killer.transform.position;
			dis.y = 0;
		} while(dis.magnitude < 2f);

		do{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);	
			gameObject.transform.position = new Vector3(Random.Range(-8f, 8f), 1f, Random.Range(-8f, 8f)) + ground.transform.position;
			dis = gameObject.transform.position - killer.transform.position;
			dis.y = 0;

			dis2 = gameObject.transform.position - woman.transform.position;
			dis2.y = 0;
		}while(dis.magnitude < 2f || dis2.magnitude < 1f);

		if (gameObject.transform.Find ("Particle System") != null) {
			gameObject.SetActive (false);
			gameObject.SetActive (true);
		}
    }
}
