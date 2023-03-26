using Raylib_cs;

namespace GravitySim
{
    class Program
    {
        static void Main()
        {
            // Setting up the Window
            const int Width = 950;
            const int Height = 725;
            Raylib.InitWindow(Width, Height, "Gravity Simulation");

            // Universal Gravitational Constant
            float G = 0.1f;

            Space galaxy = new Space();

            for (int i = 0; i < 750; i++)
            {
                var newBody = new Body(Raylib.GetRandomValue(100, Width - 100), Raylib.GetRandomValue(100, Height - 100), 0, 0, 1, 4, Color.DARKGRAY);
                galaxy.bodies.Add(newBody);
            }

            // Mainloop
            Raylib.SetTargetFPS(120);
            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    G = 2.5f;
                }else
                {
                    G = 0.1f;
                }

                galaxy.Update(G);

                // Drawing Section
                Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);

                    galaxy.Draw();
                Raylib.EndDrawing();
            }
        }
    }
}