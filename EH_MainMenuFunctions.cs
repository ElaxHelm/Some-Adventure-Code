using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class EH_MainMenuFunctions : MonoBehaviour
{
    public EH_SaveData starter;
    public float timer = 0f;
    public JLH_Sounds sound;
    public EH_SaveData player;

    public readonly string SAVE_FILE = "/SAVEGAME";

    //our own file extension TACO!!
    private string FILE_EXTENSION = ".TACO";

	public EH_SettingsFunctions settings;

	public JLH_ProgressionLogic xp;


	void Start(){
		xp = FindObjectOfType<JLH_ProgressionLogic> ();
	}


    //Method called: StartGame that can be called to execute the below functions
    public void NewGame()
    {
		PlayerPrefs.SetFloat ("CurrentXP", 0);

		PlayerPrefs.SetFloat ("MaxXP", 100);

		PlayerPrefs.SetInt ("Rank", 0);


        //Loads the next scene by checking the current scene and increasing the index by 1.  This will load the next scene or the build settings list.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        if(File.Exists(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION))
        {
            //Delete this save file
            File.Delete(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION);

            //RefreshEditorProjectWindow();
        }
    }

    //Method called QuitGame that can be called to exit out of the game.
    public void QuitGame()
    {
        Debug.Log(" I'm Leeeaaaviiinnn, never looking back again!");
        if (File.Exists(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION))
        {
            //Delete this save file
            File.Delete(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION);

           
        }
        Application.Quit();
    }

 //   public void LoadOpening()
 //   {
	//	settings.Delete ();
 //       SceneManager.LoadScene(2);
 //   }
 //   public void LoadPuzzle()
 //   {
	//	settings.Delete ();
	//	SceneManager.LoadScene(5);
 //       StartCoroutine(starter.GetComponent<EH_SaveData>().SetLocation(timer));
 //       starter.GetComponent<EH_SaveData>().loadPlayer.transform.position = new Vector2(270, 647);
 //   }
 //   public void LoadTutorial()
 //   {
	//	settings.Delete ();
	//	SceneManager.LoadScene(5);
 //       StartCoroutine(starter.GetComponent<EH_SaveData>().SetLocation(timer));
 //       starter.GetComponent<EH_SaveData>().loadPlayer.transform.position = new Vector2(-1510, 23);

 //   }
 //   public void LoadBomb()
 //   {
	//	settings.Delete ();
	//	SceneManager.LoadScene(5);
 //       StartCoroutine(starter.GetComponent<EH_SaveData>().SetLocation(timer));
 //       starter.GetComponent<EH_SaveData>().loadPlayer.transform.position = new Vector2(1, 1);
 //   }
 //   public void LoadLevel1()
 //   {
	//	settings.Delete ();
	//	SceneManager.LoadScene(5);
 //       StartCoroutine(starter.GetComponent<EH_SaveData>().SetLocation(timer));
 //       starter.GetComponent<EH_SaveData>().loadPlayer.transform.position = new Vector2(-40, 13);
 //   }
 //   public void LoadLevel2()
 //   {
	//	settings.Delete ();
	//	SceneManager.LoadScene(5);
 //       StartCoroutine(starter.GetComponent<EH_SaveData>().SetLocation(timer));
 //       starter.GetComponent<EH_SaveData>().loadPlayer.transform.position = new Vector2(-1097, 658);
 //   }
	//public void LoadLevel3()
	//{
	//	settings.Delete ();
	//	SceneManager.LoadScene(5);
 //       StartCoroutine(starter.GetComponent<EH_SaveData>().SetLocation(timer));
 //       starter.GetComponent<EH_SaveData>().loadPlayer.transform.position = new Vector2(-76, 169);
 //   }
	//public void LoadShopping()
	//{
	//	settings.Delete ();
	//	SceneManager.LoadScene(5);
 //       StartCoroutine(starter.GetComponent<EH_SaveData>().SetLocation(timer));
 //       starter.GetComponent<EH_SaveData>().loadPlayer.transform.position = new Vector2(-908, -436);
 //   }

 //   public void LoadHub()
 //   {
 //       settings.Delete();
 //       SceneManager.LoadScene(5);
 //       StartCoroutine(starter.GetComponent<EH_SaveData>().SetLocation(timer));
 //       starter.GetComponent<EH_SaveData>().loadPlayer.transform.position = new Vector2(-779, 54);
 //   }

    public void ResetRank ()
	{
		xp.curXP = 0;
	}

 

    // public void RefreshEditorProjectWindow()
    // {
    //    UnityEditor.AssetDatabase.Refresh();
    // }
}