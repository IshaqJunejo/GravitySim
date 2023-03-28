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

            var centerBody = new Body(Width/2, Height/2, 0, 0, 25, 30, Color.GREEN);
            galaxy.bodies.Add(centerBody);

            for (int i = 0; i < 90; i++)
            {
                var newBody = new Body(Width/2 + (float)(Math.Cos(i * 4 * Math.PI / 180) * 240), Height/2 + (float)(Math.Sin(i * 4 * Math.PI / 180) * 240), (float)(Math.Cos((i * 4 - 90) * Math.PI / 180) * 0.2), (float)(Math.Sin((i * 4 - 90) * Math.PI / 180) * 0.2), 0.2f, 5, Color.DARKGRAY);
                galaxy.bodies.Add(newBody);
            }

            // Mainloop
            Raylib.SetTargetFPS(120);
            while (!Raylib.WindowShouldClose())
            {
                deltaTime = Raylib.GetFrameTime();

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