using Godot;
using System;

public partial class ZombieShip : Enemy{

    private float scaleFactor = 0.25f;
    private float movSpd = 0.5f;

	public ZombieShip() : base(Rarity.NORMAL, 2){}
	public ZombieShip(Rarity r) : base(r, 2){	
		SetScript(ResourceLoader.Load("res://src/ZombieShip.cs"));
		coll = new CollisionShape3D();
		mesh = new MeshInstance3D();
		AddChild(coll);
		mesh.Mesh = ((Mesh)ResourceLoader.Load("res://assets/Zombieship/Zombieship1.res"));
		coll.Shape = new CapsuleShape3D();
		Scale *=  scaleFactor;
		AddChild(mesh);
	}

	public override void _Ready(){
        base._Ready();
    }

	public  void _Init(){}

	public override void _PhysicsProcess(double delta){ 
		var dir = GetDirection();
		MoveAndCollide((float)delta * dir * movSpd);
	}
}
