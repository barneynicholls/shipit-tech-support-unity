using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiial : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(wait());
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        EventManager.TriggerEvent("message", "Welcome To Tech Support");

    }
}
