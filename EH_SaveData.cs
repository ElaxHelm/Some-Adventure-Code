using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;


public class EH_SaveData : MonoBehaviour
{
    // Read-only save file name
    public readonly string SAVE_FILE = "/SAVEGAME";

    //our own file extension TACO!!
    public string FILE_EXTENSION = ".TACO";

   
    // Player variables to be converted after serialization that will be loaded when we reload the game from the continue menu
    public float health;
    public string savedScene;
 
    public float timer = 0f;
    public Vector2 savedPos;
    public Vector3 camPos;
 

    public GameObject player;
    public GameObject loadPlayer;
    public GameObject cam;
    public GameObject loadCam;

    


    void Update()
    {
        player = GameObject.Find("Player Character");
        cam = GameObject.Find("Main Camera");

    }

    //Singleton control to make sure only one instance of this can ever exist so we don't have duplication issues.
    public static EH_SaveData control;


    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != null)
        {
            Destroy(gameObject);
        }
        

    }


    public void Save()
    {
        //Create and open a file stream that will create us a save to our save file name and extension
        FileStream stream = File.Create(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION);
        
        BinaryFormatter bf = new BinaryFormatter();

        //**************List of information we want to save as variables***************
        // A serialized class list of variables that will record our player and game information we want to save
        PlayerData data = new PlayerData();

   
        //List of variables from Justin's progression that has the player data.
        data.health = player.GetComponent<RJ_Health>().currentHealth;

        data.savedScene = SceneManager.GetActiveScene().name;

        data.pos_x = player.transform.position.x; 
        data.pos_y = player.transform.position.y;
        data.camPos_x = cam.transform.position.x;
        data.camPos_y = cam.transform.position.y;
        data.camPos_z = cam.transform.position.z;




        //************End of variable list******************




        //Serialize the data
        bf.Serialize(stream, data);

        //Close the stream
        stream.Close();

        
        
        Debug.Log("Current Saved scene is: " + data.savedScene);
        Debug.Log("Current Health is: " + data.health);
        Debug.Log(" Current position is:" + data.pos_x);
        Debug.Log(" Current Y position is:" + data.pos_y);

        Debug.Log("Saved to: " + Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION);
    }

    public void Load()
    {
        //If we have this file name at this extension then....
        if (File.Exists(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION))
        {
            BinaryFormatter bf = new BinaryFormatter();
            
            //Open our file stream and open the file we saved earlier under this name and extension, mode selected is to open
            FileStream stream = File.Open(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION, FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(stream);

            
            //Now that our info has been deserialized to readable information, close the stream!
            stream.Close();

            //Our variable from earlier that will record the saved data information and make it it's own

            health = data.health;
            savedScene = data.savedScene;
            savedPos = new Vector2(data.pos_x, data.pos_y);
            camPos = new Vector3(data.camPos_x, data.camPos_y, data.camPos_z);
              


            //Load all of our data and make these things happen, Find an object that is tagged as the Player and set our transform to the saved location and get the health we had when we saved.
           // RefreshEditorProjectWindow();
            

            

            Debug.Log("Current Loaded Scene is: " + savedScene);
            Debug.Log("Current Loaded Health is: " + health);

            Debug.Log("Loaded saved game from: " + Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION);

           

            SceneManager.LoadScene(savedScene);

            
            StartCoroutine(DataLoad(timer));



        }
        
    }

    IEnumerator DataLoad(float f)
    {
        
        yield return new WaitForSeconds(0f);

        loadPlayer = GameObject.Find("Player Character");
        loadCam = GameObject.Find("Main Camera");
        Debug.Log(loadPlayer);
        loadCam.transform.position = camPos;
        loadPlayer.transform.position = savedPos;
        loadPlayer.GetComponent<RJ_Health>().currentHealth = health;
    }

    


    public void Delete()
    {
        //If there is a file with this name and extension
        if (File.Exists(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION))
        {

            //Delete this save file
            File.Delete(SAVE_FILE);
        }
    }
   // public void RefreshEditorProjectWindow()
    //{
     //   UnityEditor.AssetDatabase.Refresh();
   // }
    // We have a serializable (breakdown) class that will record data to be broken down and saved to a file
    [System.Serializable]
public class PlayerData
    {
        public float health;
        public string savedScene;
        public float pos_x;
        public float pos_y;
        public float camPos_x;
        public float camPos_y;
        public float camPos_z;
        
    }

}
