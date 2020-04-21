using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleManager : MonoBehaviour
{
	public SceneSwap sceneMng;
	MoveList moveList = new MoveList();
	Combatant monster, player;
	int userInput = -1;
    public BattleUI ui = new BattleUI();
    bool turn = true; // true = player

    void Start()
    {
        spawnMonster();
        spawnPlayer();
        startRound();
    }

    void spawnMonster() {
    	// import monster type and stats
    	monster = new Combatant("Demon", new int[] {1, 2, 10}, 50);
    }

    void spawnPlayer() {
    	player = new  Combatant("Andrew", new int[] {4,5,8,7}, 50);
        string[,] movesText = new string[4,2] {{player.attackList[0].name, player.attackList[0].description},
                                                    {player.attackList[1].name, player.attackList[1].description},
                                                    {player.attackList[2].name, player.attackList[2].description},
                                                    {player.attackList[3].name, player.attackList[3].description}};
    	ui.PlayerMoves(movesText);
    }

    void startRound() {
    	playerTurn();
    	
    }

    void playerTurn() {
    	player.immune = false;
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
    }

    void performAttack(Combatant target, Combatant user, Attack attack) {
    	int damage = (int)(attack.damage * user.getDmgMult()) / 100;
    	switch (attack.effect) {
    		case 1:
    			// charge attack
    			user.charging = attack.secondaryValue;
    			user.storedDmg = attack.damage;
    			break;
    		case 2:
    			user.immune = true;
    			target.takeDmg(damage);
    			break;
    		case 3:
    			user.stunned = attack.secondaryValue;
    			target.takeDmg(damage);
    			break;
    		case 4:
    			target.takeDmg(damage);
    			user.takeDmg(attack.secondaryValue);
    			break;
    		case 5:
    			user.takeDmg(-1 * attack.secondaryValue);
    			target.takeDmg(damage);
    			break;
    		case 6:
    			target.takeDmg(damage);
    			target.stunned = attack.secondaryValue;
    			target.charging = 0;
    			target.storedDmg = 0;
    			break;
    		case 7:
    			target.takeDmg(damage);
    			user.setDmgMult(attack.secondaryValue);
    			break;
    		case 8:
    			target.takeDmg(damage);
    			user.setDefMult(-attack.secondaryValue);
    			break;
    		case 9:
    			target.takeDmg(damage);
    			target.setDmgMult(-attack.secondaryValue);
    			break;
    		case 10:
    			target.takeDmg(damage);
    			target.setDefMult(attack.secondaryValue);
    			break;
    		default:
    			target.takeDmg(damage);
    			if (user.charging == 1) target.takeDmg((int)(user.storedDmg * user.getDmgMult()) / 100);
    			break;
    	}
    	updateText(user, attack);
    	turn = !turn;
    	StartCoroutine(LiterallyJustWait(turn));
    }

    IEnumerator waitForInput() {
		updateText(null, null);
    	ui.showButtons();
		while (ui.input == -1)
		{
			yield return null;
		}
		userInput = ui.input;
		ui.hideButtons();
		performAttack(monster,player,player.attackList[userInput]);
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

    	if (player.health <= 0) {
    		battleLoss();
    	}
    	else if (monster.health <= 0) {
    		battleWin();
    	}
    	else if (turn) 
    	{
    		startRound();
    	}
    	else
    	{
    		monsterTurn();
    	}
    }

    public void battleLoss()
    {
    	sceneMng.SceneLoad();
    }

    public void battleWin()
    {
    	sceneMng.SceneLoad();
    }
}

public class Attack
{
	public int damage, effect, secondaryValue;
	public string name, gameText, description;

	public Attack(string moveName, int dmg, int eff, int SecVal, string outText, string hoverText)
	{
		name = moveName;
		damage = dmg;
		effect = eff;
		secondaryValue = SecVal;
		gameText = outText;
        description = hoverText;
	}
}

public class MoveList
{
	public Attack[] moveList = new Attack[] {
		// Remember arrays start at 0
		new Attack("scream", 5, 3, 1, "dealt 5 damage but is exhausted and must recharge.", "An attack that deals some damage and has a recharge time"),
		new Attack("laugh", 5, 0, 0, "dealt 5 damage", "strait damage attack, weak"),
		new Attack("eye beam", 20, 1, 2, "began charging an attack", "a powerful move with a large wind up"),
		new Attack("isolate", 0, 2, 1, "will be protected this turn", "defensive move that will protect you for this round"),
		new Attack("lash out", 10, 4, 5, "dealt 10 damage but also recieved 5", "a hard hitting move that has recoil"),
		new Attack("rest", 0, 5, 15, "recovered 15 health", "rest, heal"),
		// ^^^5^^^
		new Attack("skip", 0, 0, 0, "is charging", "if you are reading this I am a bad programmer"),
		new Attack("focus", 0, 7, 25, "will attack harder", "focus your thoughts and strike harder"),
		new Attack("breathe", 0, 8, 25, "steeled their will", "steel your will against the enemy and take less damage"),
		new Attack("dissuade", 5, 9, 25, "decreased the demon's attack", "reason with your fears, make them less dangerous"),
		new Attack("strike fear", 3, 10, 25, "made Andrew more vulnerable", "make them worried and less defensive")
		// ^^^10^^^
	};

/*
	Secondary Effect List:
	#: Description, 		Secondary Value Description
	0: No Effect	
	1: charge, 				number of turns
	2: Protect, 			number of turns
	3: recharge, 			number of turns
	4: recoil, 				self dmg
	5: heal, 				heal amount
	6: stun,				number of turns
	7: attack buff,			percent increase
	8: defence buff, 		percent increase
	9: attack debuff,		percent decrease
	10: defence debuff,		percent decrease
*/
}

public class Combatant
{
	MoveList moveList = new MoveList();
	public int health, currEffect, healthMax, stunned, charging, storedDmg, dmgMult, defMult;
	public bool immune;
	public Attack[] attackList;
	public string name;

	public Combatant(string character, int[] moves, int hpMax)
	{
		name = character;
		healthMax = hpMax;
		health = hpMax;
		attackList = new Attack[moves.Length];
		defMult = 100;
		dmgMult = 100;

		int i = 0;
		foreach (int moveId in moves) {
			attackList[i] = moveList.moveList[moveId];
			i++;
		}
	}

	public void takeDmg(int dmg)
	{
		if (!immune) {
			health -= (int)(dmg * defMult) / 100;
		} 
	}

	public void setDmgMult(int change)
	{
		dmgMult += change;
		if (dmgMult < 25) dmgMult = 25;
		if (dmgMult > 200) dmgMult = 200;
	}

	public void setDefMult(int change)
	{
		defMult += change;
		if (defMult < 25) defMult = 25;
		if (defMult > 200) defMult = 200;
	}

	public int getDmgMult()
	{
		return dmgMult;
	}

	public void heal(int heal)
	{
		health += heal;
		if (health > healthMax) health = healthMax;
	}
}