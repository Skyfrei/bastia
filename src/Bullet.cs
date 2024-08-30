using Godot;

enum BulletType{
    NORMAL,
};

public partial class Bullet : RigidBody3D{
    
    private BulletType type;
    private bool flying = false;
    private Vector3 flyingDirection;
    private float bulletSpd = 10f;

    private RigidBody3D bullet;
    private CollisionShape3D bulletShape;
    private MeshInstance3D bulletMesh;
    private CharacterBody3D shooter;

    public Bullet(ref CharacterBody3D o){
        GravityScale = 0.0f;

        shooter = o;
        SetScript(ResourceLoader.Load("res://src/Bullet.cs"));
        this.Position = shooter.Position;
        bulletShape = new CollisionShape3D();
        bulletMesh = new MeshInstance3D();
        this.AddChild(bulletShape);
        bulletMesh.Mesh = ((Mesh)ResourceLoader.Load("res://assets/bullet.res"));
        bulletShape.AddChild(bulletMesh);
        ContactMonitor = true; 
        MaxContactsReported = 2;
        
    }
    public  void _init(CharacterBody3D o){
    }

    public override void _Ready(){

    }

    public override void _PhysicsProcess(double delta){
        //CheckCollision
        if (GetCollidingBodies().Count > 0){
            flying = false;
            GD.Print("A");
        }
        Fly(delta);
        //unload meshes
    }
    
    public void StartFlying(Vector3 fldir){
        if (!IsFlying()){
            this.Position = shooter.Position;
            flyingDirection = fldir;
            flying = true;
        }
    }
    public bool IsFlying(){
        return flying;
    }

    private void Fly(double delta){
        Vector3 newV = new Vector3(this.Position.X + flyingDirection.X,
               this.Position.Y + flyingDirection.Y,
               this.Position.Z + flyingDirection.Z) * (float)delta * bulletSpd;

        this.Position = newV;
        bulletMesh.Position = newV;
        bulletShape.Position = newV;
        GD.Print(bulletMesh.Position);
    }

    private void ProcBulletEffect(){

    }

}
