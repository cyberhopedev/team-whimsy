using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Set map selection and go to the map
    public void OnMapSelection(int mapNum)
    {
        MapSelection.SelectedMap = mapNum;
        SceneManager.LoadScene(1);
    }
}
