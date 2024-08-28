using System;
using Godot;
using System.Collections.Generic;

enum EnemyType{
	A,
	B,
	C,
	D
}

public enum Rarity{
	NORMAL,
	MAGIC,
	RARE,
	UNIQUE
}


public partial  class Spawner : Node{
	
	private List<CharacterBody3D> enemies;
	private double sec = 1; 
	private Node mainScene;

	public override void _Ready(){
		mainScene = GetParent();
	}

	public override void _Process(double delta){
		
		if (sec <= 0){
		   CharacterBody3D node = new CharacterBody3D();
		   mainScene.AddChild(node);
		   EnemyType etype = GenerateType();
		   if (etype == EnemyType.A){
			   node.SetScript(ResourceLoader.Load("res://src/ZombieShip.cs")); 
		   }
		   sec = 5;
		}else{
			sec -= delta;
		}
	}

	public Spawner(){}
	

	Enemy SpawnEnemy(EnemyType t){
		Enemy en;
		Rarity r = GenerateRarity();
		switch(t){
			case EnemyType.A:
				en = new ZombieShip(r);
				break;

			case EnemyType.B:
				en = new ZombieShip(r);
				break;

			case EnemyType.C:
				en = new ZombieShip(r);
				break;

			case EnemyType.D:
				en = new ZombieShip(r);
				break;

		}
		return new ZombieShip(r);
	}

	EnemyType GenerateType(){
		var rand = new Random();
		int generated = rand.Next(4);

		if (generated == 0){
			return EnemyType.A;
		}

		return EnemyType.A;
	}

	Rarity GenerateRarity(){
		var rand = new Random();
		int generated = rand.Next(1001);
		if (generated <= 700)
			return Rarity.NORMAL;
		else if (generated <= 900)
			return Rarity.MAGIC;
		else if (generated <= 999)
			return Rarity.RARE;
		else 
			return Rarity.UNIQUE;
	}

	
}
