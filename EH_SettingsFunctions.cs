using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




public class EH_SettingsFunctions : MonoBehaviour
{
    public AudioMixer audioM;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public EH_SaveData load;

    public readonly string SAVE_FILE = "/SAVEGAME";

    //our own file extension TACO!!
    private string FILE_EXTENSION = ".TACO";

    public GameObject continueButton;

    public GameObject playButton;

    public GameObject newGameButton;

    public GameObject fakeContinueBUtton;



    void Start()
    {


        newGameButton = GameObject.Find("NewGameButton");
        fakeContinueBUtton = GameObject.Find("FakeContinueButton");

        resolutions = Screen.resolutions;

        //Clears all options in the resolution dropdown
        resolutionDropdown.ClearOptions();

        //Creates a list of strings call options
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        //Loops through each element of the array and for each we create strings to display our resolution options 
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //adds our options from our each loop to our dropdown list
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {
        if (File.Exists(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION))
        {
            continueButton.SetActive(true);
            playButton.SetActive(true);

            newGameButton.SetActive(false);
            fakeContinueBUtton.SetActive(false);

            return;

        }
        else
        {
            continueButton.SetActive(false);
            playButton.SetActive(false);

            newGameButton.SetActive(true);
            fakeContinueBUtton.SetActive(true);
            return;
        }
    }
    public void VolumeControl(float volume)
    {

        audioM.SetFloat("volume", volume);
    }

    public void SetQuality(int qindex)
    {
        QualitySettings.SetQualityLevel(qindex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResoluton(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Load()
    {
        load = GameObject.FindObjectOfType<EH_SaveData>();

        load.Load();

    }

    public void Delete()
    {
        //If there is a file with this name and extension
        if (File.Exists(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION))
        {

            //Delete this save file
            File.Delete(Application.persistentDataPath + SAVE_FILE + FILE_EXTENSION);

            //RefreshEditorProjectWindow();

        }
    }

   // public void RefreshEditorProjectWindow()
   // {
     //   UnityEditor.AssetDatabase.Refresh();
    //}



}

