using UnityEngine;

public class MapTile : MonoBehaviour
{
    public void Selected()
    {
        Debug.Log(name);
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
