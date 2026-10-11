using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{

    [SerializeField] private GameObject[] playerSkins;

    [SerializeField] private Transform visualContainer;

    private GameObject currentSkin;

    public void SetSkin(int playerID)
    {
        if (visualContainer == null) 
        {
            return;
        }
        if (playerID < 0 || playerID >= playerSkins.Length)
        {
            return;
        }

        if (playerSkins[skinID] == null) 
        {
            return;
        }

        if (currentSkin != null)
        {
            Destroy(currentSkin);
        }

        currentSkin = Instantiate(playerSkins[playerID], visualContainer);

        currentSkin.transform.localPosition = Vector3.zero;
        currentSkin.transform.localRotation = Quaternion.identity;
        currentSkin.transform.localScale = Vector3.one;
    }
}