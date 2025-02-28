using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private string sceneNext;    // Add your Audi Clip Here;

    public void startLevel()
    {
        SceneManager.LoadScene(sceneNext);

    }

}
