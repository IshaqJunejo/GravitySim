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

            // delta time
            float deltaTime;

            Space galaxy = new Space();

            var centerBody = new Body(Width/2, Height/2, 0, 0, 100, 30, Color.GRAY);
            galaxy.bodies.Add(centerBody);

            for (int i = 0; i < 90; i++)
            {
                var newBody = new Body(Width/2 + (float)(Math.Cos(i * 4 * Math.PI / 180) * 180), Height/2 + (float)(Math.Sin(i * 4 * Math.PI / 180) * 180), (float)(Math.Cos((i * 4 - 90) * Math.PI / 180) * 0.5), (float)(Math.Sin((i * 4 - 90) * Math.PI / 180) * 0.5), 1, 3, Color.DARKGRAY);
                galaxy.bodies.Add(newBody);
            }

            for (int i = 0; i < 180; i++)
            {
                var newBody = new Body(Width/2 + (float)(Math.Cos(i * 2 * Math.PI / 180) * 360), Height/2 + (float)(Math.Sin(i * 2 * Math.PI / 180) * 360), (float)(Math.Cos((i * 2 - 90) * Math.PI / 180) * -0.6), (float)(Math.Sin((i * 2 - 90) * Math.PI / 180) * -0.6), 1, 3, Color.DARKGRAY);
                galaxy.bodies.Add(newBody);
            }

            // Mainloop
            Raylib.SetTargetFPS(120);
            while (!Raylib.WindowShouldClose())
            {
                deltaTime = Raylib.GetFrameTime();

                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                {
                    var newBody = new Body(Raylib.GetMouseX(), Raylib.GetMouseY(), Raylib.GetRandomValue(-5, 5) / 10.0f, Raylib.GetRandomValue(-5, 5) / 10.0f, 1, 3, Color.DARKGRAY);
                    galaxy.bodies.Add(newBody);
                }

                galaxy.Update(G, deltaTime);

                // Drawing Section
                Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);

                    galaxy.Draw();
                Raylib.EndDrawing();
            }
        }
    }
}