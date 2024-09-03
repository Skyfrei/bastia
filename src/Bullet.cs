using Godot;

public enum BulletType{
	PLAYER,
	ENEMY
};

public partial class Bullet : CharacterBody3D{
	
	public BulletType type;
	private bool flying = false;
	private Vector3 flyingDirection;
	private float bulletSpd = 1.0f;
	private float scaleFactor = 0.1f;

	private CollisionShape3D bulletShape;
	private MeshInstance3D bulletMesh;
	private CharacterBody3D shooter;
	private float damage;

	

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
		AddCollisionExceptionWith(GetParent().GetNode("Env"));
	}

	public override void _PhysicsProcess(double delta){
		if (IsFlying())
			Fly(delta);
	}
	
	public void StartFlying(Vector3 fldir, Vector3 shooter){
		if (!IsFlying()){
			Position = shooter;
			fldir.Y = shooter.Y;
			flyingDirection = fldir;
			flying = true;
			Visible = true;
		}
	}

	public void SetDamage(float dmg){
	   damage = dmg;
	}

	public bool IsFlying(){
		return flying;
	}

	private void Fly(double delta){
		var collision = MoveAndCollide(flyingDirection * (float)delta * bulletSpd);
		if (collision != null){
			if (collision.GetCollisionCount() > 0){
				var obj = collision.GetCollider(0) as CharacterBody3D;
				if (obj.Name == "Env") return;
				if ((obj.Name == "Player" && type == BulletType.PLAYER)||
					(obj.Name != "Player" && type == BulletType.ENEMY)){
					return;
				}else{
					if (type == BulletType.PLAYER){
						Enemy b = (Enemy)obj;
						b.health -= damage;
						if (b.health <= 0){
							b.DropLoot();
							b.QueueFree();
						}
					}else{
						Player b = (Player)obj;
						b.health -= damage;
						if (b.health <=0)
							GD.Print("dead");
					}
					flying = false;
					Visible = false;
				}
			}
		}
		
///        Position = Position + flyingDirection * (float)delta * bulletSpd;
	}

	private void ProcBulletEffect(){

	}

}
