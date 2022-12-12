using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    [SerializeField] private Toggle _exampleOneBtn;
    [SerializeField] private GameObject[] _exampleOneObjects;
    [SerializeField] private GameObject[] _exampleTwoObjects;

    private void Awake()
    {
        _exampleOneBtn.onValueChanged.AddListener((isOn) => { OnExampleClick(isOn); });
    }

    private void OnExampleClick(bool arg0)
    {
        for (int i = 0; i < _exampleOneObjects.Length; i++)
        {
            _exampleOneObjects[i].SetActive(arg0);
        }
        for (int i = 0; i < _exampleOneObjects.Length; i++)
        {
            _exampleTwoObjects[i].SetActive(!arg0);
        }
    }
}
