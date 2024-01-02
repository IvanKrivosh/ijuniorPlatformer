using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _smoothSlider;
    [SerializeField] private float _smoothStep = 0.01f;

    private float _currentValue;
    private IEnumerator _smoothCoroutine;

    public void OnHealthChanged(int maxValue, int currentValue)
    {
        _text.text = $"{currentValue}/{maxValue}";
        _currentValue = (float)currentValue / (float)maxValue;
        _slider.value = _currentValue;

        if (_smoothCoroutine == null)
        {
            _smoothCoroutine = ChangeSmoothly();
            StartCoroutine(_smoothCoroutine);
        }        
    }

    private IEnumerator ChangeSmoothly()
    {
        while (_smoothSlider.value != _currentValue)
        {
            _smoothSlider.value = Mathf.MoveTowards(_smoothSlider.value, _currentValue, _smoothStep);

            yield return null;
        }

        _smoothCoroutine = null;
    }

}
