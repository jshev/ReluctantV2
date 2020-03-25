using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleManager : MonoBehaviour
{
	MoveList moveList = new MoveList();
	Combatant monster, player;
	int userInput = -1;
    public BattleUI ui = new BattleUI();

    void Start()
    {
        spawnMonster();
        spawnPlayer();
        startRound();
    }

    void spawnMonster() {
    	// import monster type and stats
    	monster = new Combatant("Demon", new int[] {1, 2}, 50);
    }

    void spawnPlayer() {
    	player = new  Combatant("Andrew", new int[] {0,3,4,5}, 50);
    	ui.PlayerMoves(player.attackList[0].name, player.attackList[1].name, player.attackList[2].name, player.attackList[3].name);
    }

    void startRound() {
    	playerTurn();
    	
    }

    void playerTurn() {
    	player.immune = false;
    	// Prompt player
    	// Show buttons
    	// Accept Input
    	ui.resetInput();
    	if (player.stunned > 0) {
    		performAttack(monster, player, moveList.moveList[6]);
    	}
    	else if (player.charging > 0) {
    		performAttack(monster, player, moveList.moveList[6]);
    	}
    	else 
    	{
    		StartCoroutine(waitForInput());
    	}
    	//performAttack(monster,player,player.attackList[userInput]);
    	// Let Monster turn start
    	
    }

    void monsterTurn() {
    	if (monster.stunned > 0) {
    		performAttack(player, monster, moveList.moveList[6]);
    	}
    	else if (monster.charging > 0) {
    		performAttack(player, monster, moveList.moveList[6]);
    	}
		else {
    		int randomAttack = (int) Mathf.Floor(((float)monster.attackList.Length * Random.Range(0f,1f)));
    		performAttack(player, monster, monster.attackList[randomAttack]);
		}
		StartCoroutine(LiterallyJustWait(false));
    }

    void performAttack(Combatant target, Combatant user, Attack attack) {
    	switch (attack.effect) {
    		case 1:
    			// charge attack
    			user.charging = attack.secondaryValue;
    			user.storedDmg = attack.damage;
    			break;
    		case 2:
    			user.immune = true;
    			target.takeDmg(attack.damage);
    			break;
    		case 3:
    			user.stunned = attack.secondaryValue;
    			target.takeDmg(attack.damage);
    			break;
    		case 4:
    			target.takeDmg(attack.damage);
    			user.takeDmg(attack.secondaryValue);
    			break;
    		case 5:
    			user.takeDmg(-1 * attack.secondaryValue);
    			target.takeDmg(attack.damage);
    			break;
    		case 6:
    			target.takeDmg(attack.damage);
    			target.stunned = attack.secondaryValue;
    			target.charging = 0;
    			target.storedDmg = 0;
    			break;
    		default:
    			target.takeDmg(attack.damage);
    			if (user.charging == 1) target.takeDmg(user.storedDmg);
    			break;
    	}
    	updateText(user, attack);
    }

    IEnumerator waitForInput() {
		updateText(null, null);
    	ui.showButtons();
		while (ui.input == -1)
		{
			yield return null;
		}
		userInput = ui.input;
		performAttack(monster,player,player.attackList[userInput]);
		ui.hideButtons();
		StartCoroutine(LiterallyJustWait(true));
    }

    void updateText(Combatant user, Attack attack)
    {
    	if (user == null)
    	{
    		ui.updateText("Choose your Attack");
    	}
    	else if (attack.name != "skip") {
    		ui.updateText(user.name + " used " + attack.name + " and " + attack.gameText);
    	}
    	else
    	{
    		if (user.stunned > 0)
    		{
    			user.stunned -=1;
    			ui.updateText(user.name + " is exhausted and skipped this turn");
    		}
    		else 
    		{
    			if (user.charging == 1)
    			{
    				ui.updateText(user.name + " released a charged blast and dealt " + user.storedDmg + " damage.");
    				user.charging = 0;
    				user.storedDmg = 0;
    			}
    			else 
    			{
    				ui.updateText(user.name + " is storing energy.");
    				user.charging -= 1;
    			}
    		}
    	}
    	ui.printHealth(player.health, player.healthMax, monster.health, monster.healthMax);
    }

    IEnumerator LiterallyJustWait(bool turn) {
    	yield return new WaitForSeconds(3);
    	if (turn) 
    	{
    		monsterTurn();
    	}
    	else
    	{
    		startRound();
    	}
    }
}

public class Attack
{
	public int damage, effect, secondaryValue;
	public string name, gameText;

	public Attack(string moveName, int dmg, int eff, int SecVal, string outText)
	{
		name = moveName;
		damage = dmg;
		effect = eff;
		secondaryValue = SecVal;
		gameText = outText;
	}
}

public class MoveList
{
	public Attack[] moveList = new Attack[] {
		// Remember arrays start at 0
		new Attack("scream", 5, 3, 1, "dealt 5 damage but is exhausted and must recharge."),
		new Attack("laugh", 5, 0, 0, "dealt 5 damage"),
		new Attack("eye beam", 20, 1, 2, "began charging an attack"),
		new Attack("isolate", 0, 2, 1, "will be protected this turn"),
		new Attack("lash out", 10, 4, 5, "dealt 10 damage but also recieved 5"),
		new Attack("rest", 0, 5, 15, "recovered 15 health"),
		// ^^^5^^^
		new Attack("skip", 0, 0, 0, "is charging"),
	};

/*
	Secondary Effect List:
	#: Description, 	Secondary Value Description
	0: No Effect
	1: charge, 			number of turns
	2: Protect, 		number of turns
	3: recharge, 		number of turns
	4: recoil, 			self dmg
	5: heal, 			heal amount
	6: stun,			number of turns
*/
}

public class Combatant
{
	MoveList moveList = new MoveList();
	public int health, currEffect, healthMax, stunned, charging, storedDmg;
	public bool immune;
	public Attack[] attackList;
	public string name;

	public Combatant(string character, int[] moves, int hpMax)
	{
		name = character;
		healthMax = hpMax;
		health = hpMax;
		attackList = new Attack[moves.Length];

		int i = 0;
		foreach (int moveId in moves) {
			attackList[i] = moveList.moveList[moveId];
			i++;
		}
	}

	public void takeDmg(int dmg)
	{
		if (!immune) {
			health -= dmg;
		} 
		if (health > healthMax) health = healthMax;
	}
}