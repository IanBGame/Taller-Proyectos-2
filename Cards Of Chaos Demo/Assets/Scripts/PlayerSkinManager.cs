using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{

    [SerializeField] private GameObject[] playerSkins;

    [SerializeField] private Transform visualContainer;

    [SerializeField] private float skinScale =  1.0f;

    private GameObject currentSkin;

    public void SetSkin(int skinID)
    {
        if (skinID < 0 || skinID >= playerSkins.Length)
        {
            return;
        }

        if (playerSkins[skinID] == null) 
        {
            return;
        }

        if (visualContainer == null) 
        {
            return;
        }

        if (currentSkin != null)
        {
            Destroy(currentSkin);
        }

        currentSkin = Instantiate(playerSkins[skinID], visualContainer);

        currentSkin.transform.localPosition = Vector3.zero;
        currentSkin.transform.localRotation = Quaternion.identity;
        currentSkin.transform.localScale = Vector3.one * skinScale;
    }

    public Animator GetAnimator()
    {
        if (currentSkin == null)
        {
            return null;
        }

        return currentSkin.GetComponentInChildren<Animator>();
    }

    public void SetVisible(bool visible)
    {
        if (visualContainer != null)
        {
            visualContainer.gameObject.SetActive(visible);
        }
    }
}