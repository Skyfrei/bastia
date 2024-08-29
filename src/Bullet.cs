using Godot;

enum BulletType{
    NORMAL,
};

public partial class Bullet : RigidBody3D{
    
    private BulletType type;
    private bool flying = false;

    private CharacterBody3D origin;
    private RigidBody3D bullet;
    private CollisionShape3D bulletShape;
    private MeshInstance3D bulletMesh;

    public Bullet(CharacterBody3D o){
        origin = o;
    }
    public  void _init(CharacterBody3D o){
    }

    public override void _Ready(){

    }

    public override void _PhysicsProcess(double delta){

    }

    public bool IsFlying(){
        return flying;
    }

    public void Fly(){
        InitBulletGraphics();
        flying = true;
    }

    private void InitBulletGraphics(){
        if (bullet == null){
            
        }
        if (bulletShape == null){

        }

        if (bulletMesh == null){

        }

    }

    private void ProcBulletEffect(){

    }

}
