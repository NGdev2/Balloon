using UnityEngine;
using UnityEngine.UI;
using System;


public class Balloon : MonoBehaviour
{
	[SerializeField]
	private float _breath_power;
	public Image bar;
	public Image balloon;
	private RectTransform _ballonSize;
	private RectTransform _breathSize;
	private Vector3 _ballonDefaultSize;
	private Vector3 _barDefaultSize;
	[SerializeField]
	private Color _bar_color;
	[SerializeField]
	private Color _lowBarColor;
	private int _start;
	private int _breath_mult;
	[SerializeField] private GameObject winScreen;
	[SerializeField] private GameObject loseScreen;
 
	private const float _mult = 0.98f; // deflation index

	private float _colorstep;

	void Start()
	{
		// _breathSize = bar.GetComponent<RectTransform>();
		_colorstep = 0f;
		_breath_power = 1.6f;
		_barDefaultSize = bar.GetComponent<RectTransform>().localScale;

		_ballonSize = balloon.GetComponent<RectTransform>();
		_ballonDefaultSize = new Vector3(1, 1, 1);
		_start = 0;
		_bar_color = bar.color;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) == true)
		{
			Debug.Log("Exit");
			Application.Quit();
		}
		_colorstep += 0.25f;
		if (Input.GetKeyDown(KeyCode.Space) && bar.fillAmount >= 0.13f)
		{
			_breath_mult = 1;
			_start = 1;
			if (_ballonSize.localScale.x < 1f)
			{
				_ballonSize.localScale = _ballonDefaultSize;
			}
			_ballonSize.localScale = new Vector3(_ballonSize.localScale.x * _breath_power, _ballonSize.localScale.y * _breath_power, _ballonSize.localScale.x);
			
			float color_g = 0.1f;
			if (bar.color.g > color_g)
			{
				bar.color = new Vector4(bar.color.r, bar.color.g - color_g, bar.color.b, bar.color.a);
			}
			bar.fillAmount -= 0.13f;
		}
		if (bar.fillAmount < 1f)
		{
			Debug.Log(0.003f * _breath_mult);
			bar.fillAmount = bar.fillAmount + (0.003f + 0.0001f * _breath_mult);
			_breath_mult += 1;
		}
		
		if (_start != 1 ){ return;}
		
		if (_ballonSize.localScale.x > 1 && _ballonSize.localScale.x < 30)
		{
			_ballonSize.localScale = new Vector3(_ballonSize.localScale.x * _mult, _ballonSize.localScale.y * _mult, _ballonSize.localScale.x);
		}
		else if (_ballonSize.localScale.x > 0.5f && _ballonSize.localScale.x < 30)
		{
			_ballonSize.localScale = new Vector3(_ballonSize.localScale.x * _mult, _ballonSize.localScale.y, _ballonSize.localScale.x);
		}
		else
		{
			if (_ballonSize.localScale.x >= 30)
			{
				winScreen.SetActive(true);
				Debug.Log("You Win");
			}
			else
			{
				loseScreen.SetActive(true);
				Debug.Log("You lose");
			}
			Destroy(balloon);
			Destroy(this);
		}
	}
}
