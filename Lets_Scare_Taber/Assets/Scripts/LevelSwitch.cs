using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{


    public void changeToScene(int changeTheScene)
    {
        SceneManager.LoadScene(changeTheScene);
    }
}
	
