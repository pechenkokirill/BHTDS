﻿using BHTDS.Engine;
using BHTDS.GameLogic.Components;

namespace BHTDS;

class BHTDSGame
{
    static void Main(string[] args)
    {
        using (BHTDSEngine engine = new BHTDSEngine())
        {
            var mainScene = engine.SceneManager.CreateScene("main");
            mainScene.CreateEntity().AddComponent<CreateSceneTestComponent>();
            engine.Run();
        }
    }
}