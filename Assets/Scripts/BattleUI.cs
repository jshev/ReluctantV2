using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
	public Text battleText, playerHPText, enemyHPText;
	public Button atk1, atk2, atk3, atk4;
	public int input = -1;
	public string[,] attackText; // allText[moveNum][0 - name, 1 - description]

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

	public void PlayerMoves(string[,] allText)
	{
		attackText = allText;
		atk1.GetComponentInChildren<Text>().text = attackText[0,0];
		atk2.GetComponentInChildren<Text>().text = attackText[1,0];
		atk3.GetComponentInChildren<Text>().text = attackText[2,0];
		atk4.GetComponentInChildren<Text>().text = attackText[3,0];
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

	public void hover1()
	{
		battleText.text = attackText[0,1];
	}

	public void hover2()
	{
		battleText.text = attackText[1,1];
	}


	public void hover3()
	{
		battleText.text = attackText[2,1];
	}


	public void hover4()
	{
		battleText.text = attackText[3,1];
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
