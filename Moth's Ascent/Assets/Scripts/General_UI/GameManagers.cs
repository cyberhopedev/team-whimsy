using UnityEngine;
using System.Collections;

public class GameManagers : MonoBehaviour
{
    private static GameManagers _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            StartCoroutine(MakePersistent());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator MakePersistent()
    {
        yield return null; // wait one frame for scene to fully initialize
        DontDestroyOnLoad(gameObject);
    }
}