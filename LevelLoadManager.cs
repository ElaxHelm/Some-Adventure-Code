using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelLoadManager : MonoBehaviour {


    public float timer = 1f;
    public GameObject setPlayer;
	// Use this for initialization
	void Awake ()
    {
        DontDestroyOnLoad(gameObject);
    }
	

    public void LoadOpening()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadPuzzle()
    {
        
        SceneManager.LoadScene(5);
        StartCoroutine(SetLocation(timer));
        StartCoroutine(GoLocationPuzzle(timer));

    }
    public void LoadTutorial()
    {
       
        SceneManager.LoadScene(5);
        StartCoroutine(SetLocation(timer));
        StartCoroutine(GoLocationTutorial(timer));

    }
    public void LoadBomb()
    {
      
        SceneManager.LoadScene(5);
        StartCoroutine(SetLocation(timer));
        StartCoroutine(GoLocationBomb(timer));
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(5);
        StartCoroutine(SetLocation(timer));
        StartCoroutine(GoLocationLevel1(timer));

    }
    public void LoadLevel2()
    {
      
        SceneManager.LoadScene(5);
        StartCoroutine(SetLocation(timer));
        StartCoroutine(GoLocationLevel2(timer));

    }
    public void LoadLevel3()
    {
     
        SceneManager.LoadScene(5);
        StartCoroutine(SetLocation(timer));
        StartCoroutine(GoLocationLevel3(timer));

    }
    public void LoadShopping()
    {
       
        SceneManager.LoadScene(5);
        StartCoroutine(SetLocation(timer));
        StartCoroutine(GoLocationShopping(timer));

    }

    public void LoadHub()
    {
       
        SceneManager.LoadScene(5);
        StartCoroutine(SetLocation(timer));
        StartCoroutine(GoLocationHub(timer));

    }

    public IEnumerator SetLocation(float f)
    {
        yield return new WaitForSeconds(0f);

        setPlayer = GameObject.Find("Player Character");

    }
    public IEnumerator GoLocationPuzzle(float f)
    {
        yield return new WaitForSeconds(0f);
        setPlayer.transform.position = new Vector2(270, 647);
    }
    public IEnumerator GoLocationTutorial(float f)
    {
        yield return new WaitForSeconds(0f);
        setPlayer.transform.position = new Vector2(-1510, 23);
    }
    public IEnumerator GoLocationBomb(float f)
    {
        yield return new WaitForSeconds(0f);
        setPlayer.transform.position = new Vector2(-1096, 659);
    }
    public IEnumerator GoLocationLevel1(float f)
    {
        yield return new WaitForSeconds(0f);
        setPlayer.transform.position = new Vector2(-40, 13);
    }
    public IEnumerator GoLocationLevel2(float f)
    {
        yield return new WaitForSeconds(0f);
        setPlayer.transform.position = new Vector2(-1097, 658);
    }
    public IEnumerator GoLocationLevel3(float f)
    {
        yield return new WaitForSeconds(0f);
        setPlayer.transform.position = new Vector2(-76, 169);
    }
    public IEnumerator GoLocationShopping(float f)
    {
        yield return new WaitForSeconds(0f);
        setPlayer.transform.position = new Vector2(-908, -436);
    }
    public IEnumerator GoLocationHub(float f)
    {
        yield return new WaitForSeconds(0f);
        setPlayer.transform.position = new Vector2(-779, 54);
    }


}
