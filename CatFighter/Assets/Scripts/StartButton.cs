using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButton : MonoBehaviour
{
    public void StartButtonClick()
    {
        Application.LoadLevel("Level01");
    }
}
