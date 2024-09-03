using Godot;
using System;

public partial class AkiraShip : Enemy{


	private float scaleFactor = 0.25f;
	private float movSpd = 0.23f;

	public AkiraShip() : base(Rarity.NORMAL, 2){}
	public AkiraShip(Rarity r) : base(r, 2){	
		SetScript(ResourceLoader.Load("res://src/AkiraShip.cs"));
		coll = new CollisionShape3D();
		var sc = (PackedScene)GD.Load("res://assets/Starp/akira.tscn");
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
		var random = new Random();
		double r = (random.NextDouble() * 5.0) - 1.0;
		float g = (float)r;
		var ndir = new Vector3(dir.X + g, 1, dir.Z + g);
		base._PhysicsProcess(delta);
		MoveAndCollide((float)delta * ndir *movSpd);
    }
}
