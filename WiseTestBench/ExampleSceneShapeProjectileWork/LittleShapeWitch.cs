﻿using Microsoft.Xna.Framework;
using WiseEngine.MonogamePart;
using WiseEngine;
using WiseTestBench.ExampleSceneTriggerWork;

namespace WiseTestBench.ExampleSceneShapeProjectileWork;

public class LittleShapeWitch : ShapeWitch
{
    public LittleShapeWitch(Vector2 initPos) : base(initPos)
    {
        Sprites[0].Scale = Vector2.One * 2;
        int width = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Width * Sprites[0].Scale.X);
        int height = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Height * Sprites[0].Scale.Y);
        Collider = new RectangleCollider(Vector2.Zero, width, height);
    }
}