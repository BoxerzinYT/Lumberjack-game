using UnityEngine;

public class Characters_LevelManager : MonoBehaviour
{
    public int hector_level;

    public int GetCharactersLevel(int charId)
    {
        if(charId == 0)
        {
            return hector_level;
        }

        return 1;
    }
}
