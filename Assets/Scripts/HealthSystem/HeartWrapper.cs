using UnityEngine.UI;
using UnityEngine;

public class HeartWrapper : MonoBehaviour
{
    [SerializeField] private Image image;

    private Heart _heart;

    private void Awake()
    {
        _heart = new Heart(image);
    }

    public Heart GetHeart()
    {
        return _heart;
    }
}