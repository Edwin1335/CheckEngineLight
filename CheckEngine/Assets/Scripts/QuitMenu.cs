using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void quit()
    {
        Application.Quit();
        Debug.Log("Game Over");
    }
}
