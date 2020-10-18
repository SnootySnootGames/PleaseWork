using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    private static bool created = false;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
