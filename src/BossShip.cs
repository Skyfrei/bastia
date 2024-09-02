using Godot;
using System;

public partial class BossShip : Enemy{


	private float scaleFactor = 0.25f;
	private float movSpd = 0.12f;

	public BossShip() : base(Rarity.NORMAL, 2){}
	public BossShip(Rarity r) : base(r, 2){	
		SetScript(ResourceLoader.Load("res://src/BossShip.cs"));
		coll = new CollisionShape3D();
        var sc = (PackedScene)GD.Load("res://assets/boss/boss.tscn");
        var ins = sc.Instantiate();
        AddChild(ins);
		AddChild(coll);
		coll.Shape = new CapsuleShape3D();
		Scale *=  scaleFactor;
	}

	public override void _Ready(){
		base._Ready();
	}

	public  void _Init(){}

	public override void _PhysicsProcess(double delta){ 
		var dir = GetDirection();
        base._PhysicsProcess(delta);
		MoveAndCollide((float)delta * dir * movSpd);
	}
}
