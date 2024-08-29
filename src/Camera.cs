using Godot;

public partial class Camera : Camera3D{
   
    private CharacterBody3D player;
    private Camera3D playerCam;
    private float cameraMovSpeed = 10;
    private float cameraDistance = 5;

    public override void _Ready(){
        player = GetParent().GetNode<CharacterBody3D>("Player");
        playerCam = GetParent().GetNode<Camera3D>("PlayerCamera");
    }

    public override void _PhysicsProcess(double delta){
        Vector3 playerPos = player.Position;
        //Vector3 playerRot = player.Rotation;
        playerCam.Position = new Vector3(playerPos.X, playerPos.Y + cameraDistance, playerPos.Z);
    }

};
