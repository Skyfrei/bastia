using Godot;

enum BulletType{
    NORMAL,
};

public partial class Bullet : RigidBody3D{
    
    private BulletType type;
    private bool flying = false;
    private Vector3 flyingDirection;
    private float bulletSpd = 1.0f;

    private RigidBody3D bullet;
    private CollisionShape3D bulletShape;
    private MeshInstance3D bulletMesh;
    private CharacterBody3D shooter;

    public Bullet(){
        GravityScale = 0.0f;

        SetScript(ResourceLoader.Load("res://src/Bullet.cs"));
        bulletShape = new CollisionShape3D();
        bulletMesh = new MeshInstance3D();
        this.AddChild(bulletShape);
        bulletMesh.Mesh = ((Mesh)ResourceLoader.Load("res://assets/bullet.res"));
        bulletShape.AddChild(bulletMesh);
        ContactMonitor = true; 
        MaxContactsReported = 2;
        
    }
    public override void _Ready(){

    }

    public override void _PhysicsProcess(double delta){
        //CheckCollision
        if (GetCollidingBodies().Count > 0){
            flying = false;
            GD.Print("collided");
        }
        if (IsFlying())
            Fly(delta);
        //unload meshes
    }
    
    public void StartFlying(Vector3 fldir, Vector3 shooter){
        if (!IsFlying()){
            Position = shooter;
            flyingDirection = fldir;
            flying = true;
        }
    }
    public bool IsFlying(){
        return flying;
    }

    private void Fly(double delta){
        Position = Position + flyingDirection * (float)delta * bulletSpd;
        bulletMesh.Position = Position;
        bulletShape.Position = Position;
    }

    private void ProcBulletEffect(){

    }

}
