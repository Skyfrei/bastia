using Godot;

public enum BulletType{
    PLAYER,
    ENEMY
};

public partial class Bullet : CharacterBody3D{
	
	public BulletType type;
	private bool flying = false;
	private Vector3 flyingDirection;
	private float bulletSpd = 1.5f;
	private float scaleFactor = 0.1f;

	private CollisionShape3D bulletShape;
	private MeshInstance3D bulletMesh;
	private CharacterBody3D shooter;

    

	public Bullet(){
		Visible = false;

		SetScript(ResourceLoader.Load("res://src/Bullet.cs"));
		bulletShape = new CollisionShape3D();
		bulletMesh = new MeshInstance3D();
		AddChild(bulletShape);
		bulletMesh.Mesh = ((Mesh)ResourceLoader.Load("res://assets/bullet.res"));
        bulletShape.Shape = new SphereShape3D();
        Scale *= scaleFactor;
        AddChild(bulletMesh);
	}
	public override void _Ready(){

	}

	public override void _PhysicsProcess(double delta){
		if (IsFlying())
			Fly(delta);
	}
	
	public void StartFlying(Vector3 fldir, Vector3 shooter){
		if (!IsFlying()){
			Position = shooter;
			flyingDirection = fldir;
			flying = true;
			Visible = true;
		}
	}
	public bool IsFlying(){
		return flying;
	}

	private void Fly(double delta){
		var collision = MoveAndCollide(flyingDirection * (float)delta * bulletSpd);
        if (collision != null){
            if (collision.GetCollisionCount() > 0){
                var obj = collision.GetCollider(0) as Node3D;
                if ((obj.Name == "Player" && type == BulletType.PLAYER)||
                    (obj.Name != "Player" && type == BulletType.ENEMY)){
                    return;
                }else{
                    flying = false;
		            Visible = false;
                    GD.Print("bomba");
                }
            }
        }
        
///        Position = Position + flyingDirection * (float)delta * bulletSpd;
	}

	private void ProcBulletEffect(){

	}

}
