using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
	public TextMeshProUGUI TimeText;
	public TextMeshProUGUI CoinText;
	public TextMeshProUGUI SpeedText;
	public TextMeshProUGUI ScoreText;

	private Rigidbody playerRigidbody;

	private void Start()
	{
		playerRigidbody = GameObject.Find("PlayerCharacter").GetComponent<Rigidbody>();
	}

	void Update()
	{
		TimeText.text = GameInstance.instance.GamePlayTime.ToString();
		CoinText.text = GameInstance.instance.Coin.ToString();
		ScoreText.text = "Score : " + GameInstance.instance.Score.ToString();
		if (playerRigidbody != null)
		{
			float speed = playerRigidbody.velocity.magnitude;
			SpeedText.text = "Speed : " + speed.ToString();
		}
	}
}
