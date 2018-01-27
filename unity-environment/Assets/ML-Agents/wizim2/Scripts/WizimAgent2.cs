using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizimAgent2 : Agent
{
    [Header("Specific to ball")]
    public GameObject ball;
	private Vector3 oldPos = Vector3.zero;


    public override List<float> CollectState()
    {
        List<float> state = new List<float>();

        state.Add((ball.transform.position.x - gameObject.transform.position.x));
        state.Add((ball.transform.position.z - gameObject.transform.position.z));
		state.Add(gameObject.transform.GetComponent<Rigidbody>().velocity.x);
		state.Add(gameObject.transform.GetComponent<Rigidbody>().velocity.z);

        return state;
    }

    public override void AgentStep(float[] act)
    {
		
		if (oldPos == Vector3.zero) {
			oldPos = gameObject.transform.position;
		}

        float action_x = act[1];
		action_x = Mathf.Clamp (action_x, -1f, 1f);


        float action_y = act[0];
		action_y = Mathf.Clamp (action_y, -1f, 1f);

		oldPos += new Vector3 (action_x, action_y, 0f) * 0.1f;

		gameObject.GetComponent<Rigidbody> ().MovePosition (oldPos);

		/*
		Vector3 dis = woman.transform.position - gameObject.transform.position;

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
		}
*/

    }

    // to be implemented by the developer
    public override void AgentReset()
    {
		//gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
		//gameObject.transform.position = oldPos;

    }
}
