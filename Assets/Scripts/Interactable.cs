using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string _displayedText;
    [SerializeField]
    private GameObject _canvasPrefab;
    [SerializeField]
    private UnityEvent _actions;

    private float _timer;

    private Image _image;
    private GameObject _canvas;

    private bool _doingAction;
    private bool _canInput;

    private void Start()
    {
        _canvas = Instantiate(_canvasPrefab, transform);
        _canvas.GetComponentInChildren<TMP_Text>().text = _displayedText;
        _image = _canvas.GetComponentInChildren<Image>();
        _canvas.SetActive(false);

        _timer = 0;
        _canInput = false;
        _doingAction = false;
        _image.fillAmount = _timer;
        DayNightManager.Instance.EventDay += OnDay;
    }

    private void OnDay(bool IsDay)
    {
        _canvas.GetComponentInChildren<TMP_Text>().color = IsDay ? Color.black : Color.white;
    }

    private void Update()
    {
        if (!_canvas.activeSelf) { return; }

        if (Input.GetKey(KeyCode.E) && _timer < 1f && !_canInput)
        {
            _timer += Time.deltaTime;

            if (_timer >= 1f) { StartCoroutine(doAction()); }
        }

        if (Input.GetKeyUp(KeyCode.E) && !_doingAction)
        {
            _timer = 0;
            _canInput = false;
        }

        _image.fillAmount = _timer;
    }

    IEnumerator doAction()
    {
        _doingAction = true;
        _actions.Invoke();
        _canInput = true;
        yield return new WaitForSeconds(0.5f);
        _timer = 0f;
        _doingAction = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _canvas.SetActive(false);
    }
}
