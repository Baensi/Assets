using UnityEngine;
using System.Collections;

using Engine.Objects;

public class SwordObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {

		ObjectDestroyed obj = other.gameObject.GetComponent<ObjectDestroyed>();

        if (obj != null) {
            obj.addDamage(20.0f);
        }

    }

}
