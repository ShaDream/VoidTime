﻿using System;
using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using VoidTime.Resources;

namespace VoidTime
{
    public class BattleEnemyDrawHelper : IDrawable
    {
        public Type GameObjectType { get; } = typeof(BattleEnemy);

        public void DrawObject(ObjectOnDisplay obj, OpenGL gl)
        {
            ShipTexturesHelper.GetShipTexture(gl, ((BattleEnemy) obj.GameObject).Data.ShipName).Bind(gl);
            DrawHelper.Draw(obj, gl, new Size(100,110), ((BattleEnemy)obj.GameObject).Angle);
        }

        public void Init(OpenGL gl)
        {
        }
    }
}