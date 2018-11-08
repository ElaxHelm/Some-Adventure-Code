using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EH_ColliderSceneControl : MonoBehaviour {

    public GameObject player;

	public GameObject sceneFade;
	public Animator sceneAnim;

	public Vector2 nextPos;

	public float timer;
	// Use this for initialization
	void Start ()
    {
		sceneAnim = sceneFade.GetComponent<Animator> ();
        sceneFade.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        player = GameObject.Find("Player Character");
        
	}


    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            sceneFade.SetActive(true);
            sceneAnim.SetBool ("Fading", true);
			StartCoroutine (FadeDelay (timer));
			player.transform.position = nextPos;

        }
    }

	public IEnumerator FadeDelay(float f){
		yield return new WaitForSeconds (f);

		sceneAnim.SetBool ("Fading", false);
        sceneFade.SetActive(false);
    }
}
