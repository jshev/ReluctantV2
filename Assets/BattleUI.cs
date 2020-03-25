using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
	public Text battleText, playerHPText, enemyHPText;
	public Button atk1, atk2, atk3, atk4;
	public int input = -1;

	public void hideButtons()
	{
		atk1.gameObject.SetActive(false);
		atk2.gameObject.SetActive(false);
		atk3.gameObject.SetActive(false);
		atk4.gameObject.SetActive(false);
	}

	public void showButtons()
	{
		atk1.gameObject.SetActive(true);
		atk4.gameObject.SetActive(true);
		atk2.gameObject.SetActive(true);
		atk3.gameObject.SetActive(true);
	}

	public void PlayerMoves(string a1, string a2, string a3, string a4)
	{
		atk1.GetComponentInChildren<Text>().text = a1;
		atk2.GetComponentInChildren<Text>().text = a2;
		atk3.GetComponentInChildren<Text>().text = a3;
		atk4.GetComponentInChildren<Text>().text = a4;
	}

	public void resetInput()
	{
		input = -1;
	}

	public void press1()
	{
		input = 0;
	}

	public void press2()
	{
		input = 1;
	}	

	public void press3()
	{
		input = 2;
	}

	public void press4()
	{
		input = 3;
	}

	public void printHealth(int playerHP, int playerMax, int enemyHP, int enemyMax)
	{
		playerHPText.text = "Health: " + playerHP + "/" + playerMax;
		enemyHPText.text = "Enemy Health: " + enemyHP + "/" + enemyMax;
	}

	public void updateText(string update)
	{
		battleText.text = update;
	}
}
