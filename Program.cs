using System.Numerics;
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

            // Adding Bodies
            List<Body> bodies = new List<Body>(0);

            for (int i = 0; i < 750; i++)
            {
                var newBody = new Body(Raylib.GetRandomValue(100, Width - 100), Raylib.GetRandomValue(100, Height - 100), 0, 0, 1, 4, Color.DARKGRAY);
                bodies.Add(newBody);
            }

            // Mainloop
            Raylib.SetTargetFPS(120);
            while (!Raylib.WindowShouldClose())
            {
                // Adding Gravitational Effect
                foreach (Body body in bodies)
                {
                    foreach (Body BODY in bodies)
                    {
                        if (body.pos != BODY.pos)
                        {
                            Vector2 directionVector = body.pos - BODY.pos;
                            float distance = (float) Math.Sqrt(directionVector.X * directionVector.X + directionVector.Y * directionVector.Y);
                            Vector2 directionNormal = directionVector / distance;
                            
                            BODY.force += (directionNormal) * G * BODY.mass * body.mass / (distance * distance);
                        }
                    }
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    G = 2.5f;
                }else
                {
                    G = 0.1f;
                }

                // Updating the Bodies
                foreach (Body body in bodies)
                {
                    body.Update();

                    foreach (Body BODY in bodies)
                    {
                        if (Body.CheckCollision(body, BODY))
                        {
                            // Calculating Penetration 
                            Vector2 distance = body.pos - BODY.pos;
                            float length = (float) Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                            Vector2 normal = distance / length;

                            float depth = (body.radie + BODY.radie) - length;
                            Vector2 pentrateResolve = normal * depth / 2;

                            // Executing Penetration
                            body.pos += pentrateResolve;
                            BODY.pos -= pentrateResolve;
                        }
                    }
                }

                // Drawing Section
                Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);

                    foreach (Body body in bodies)
                    {
                        body.Draw();
                    }
                Raylib.EndDrawing();
            }
        }
    }
}