using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameEntryField;

    public void StartGame()
    {
        nameEntryField.GetComponent<TMP_InputField>();
        SceneManager.LoadScene(1);
        MainManager.instance.playerName = nameEntryField.text;
    }
}
 