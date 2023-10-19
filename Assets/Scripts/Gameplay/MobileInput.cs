using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private void Awake()
    {
#if !(UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR)
        gameObject.SetActive(false);
#endif
    }
}