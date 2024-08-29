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
    private int numOfEnemies = 100;
    private float enemyScale = 1.0f;
    private double maxSpawnTime = 5;
	private double sec; 
    private Node mainScene;
	
    public Spawner(){}

	public override void _Ready(){
        enemies = new List<CharacterBody3D>();
        sec = maxSpawnTime;
        mainScene = GetParent();
	}

	public override void _Process(double delta){
        sec -= delta;   
        LoadEnemyResources();
	}

    private void LoadEnemyResources(){
    	if (sec <= 0){
		    EnemyType etype = GenerateType();
            if (etype == EnemyType.A){
                CharacterBody3D body = new CharacterBody3D();
                CollisionShape3D coll = new CollisionShape3D();
                MeshInstance3D mesh = new MeshInstance3D();

                mainScene.AddChild(body);
                body.AddChild(coll);
                mesh.Mesh = ((Mesh)ResourceLoader.Load("res://assets/Zombieship/Zombieship1.res"));
                coll.AddChild(mesh);
                body.SetScript(ResourceLoader.Load("res://src/ZombieShip.cs"));


		   }
		   sec = maxSpawnTime;
        }
    }

    private void ScaleSpawnedEnemies(){
        
    }

    private void DestroyEnemy(){

    }


	

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
