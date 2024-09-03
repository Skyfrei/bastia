using Godot;
using System;


public partial class Env : Area3D{
    
    private CharacterBody3D pl;

    public override void _Ready(){
        pl = (CharacterBody3D)GetParent().GetNode("Player");
    }

    public override void _Process(double delta){
        Position = pl.Position;
    }
}
