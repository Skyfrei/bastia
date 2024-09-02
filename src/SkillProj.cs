using Godot;

public enum Skill{
	S_CRITCHANCE,
	S_CRITMULT,
	S_HEALTH_PERC,
	S_ATTACKSPEED,

}

public partial class SkillProj : CharacterBody3D{
   
	private CharacterBody3D pl;
	private CollisionShape3D coll;
	private MeshInstance3D mesh;

	private Vector3 fallDir;
	private Vector3 origin;
	private float scaleFactor = 0.1f;

	public SkillProj(Vector3 o) {
		origin = o; 
		SetScript(ResourceLoader.Load("res://src/SkillProj.cs"));
		coll = new CollisionShape3D();
		mesh = new MeshInstance3D();
		AddChild(coll);
		mesh.Mesh = ((Mesh)ResourceLoader.Load("res://assets/bullet.res"));
		coll.Shape = new SphereShape3D();
		Scale *= scaleFactor;
		AddChild(mesh);
	}

	public override void _Ready(){
		pl = GetParent().GetNode("Player") as CharacterBody3D;
		fallDir = pl.Position - origin;
	}

	public override void _PhysicsProcess(double delta){
		
	}


}
