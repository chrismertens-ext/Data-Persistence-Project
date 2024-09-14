using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameEntryField;

    public void StartGame()
    {
        nameEntryField.GetComponent<TMP_InputField>();
        SceneManager.LoadScene(1);
        MainManager.instance.playerName = nameEntryField.text;

        if(MainManager.instance.newPlayer)
        {
            MainManager.instance.RestartGame();
        }
    }
}
 