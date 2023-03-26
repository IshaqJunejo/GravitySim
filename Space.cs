using System.Numerics;
using Raylib_cs;

namespace GravitySim
{
    public class Space
    {
        public List<Body> bodies;

        public Space()
        {
            bodies = new List<Body>(0);
        }

        public void Update(float GravitationalConst)
        {
            foreach (var body in this.bodies)
            {
                body.Update();
            }

            this.SimulateGravity(GravitationalConst);
            this.CollisionRespose();
        }

        public void SimulateGravity(float GravitationalConst)
        {
            foreach (Body body01 in this.bodies)
            {
                foreach (Body body02 in this.bodies)
                {
                    if (body01.pos != body02.pos)
                    {
                        Vector2 directionVector = body01.pos - body02.pos;
                        float distance = (float) Math.Sqrt(directionVector.X * directionVector.X + directionVector.Y * directionVector.Y);
                        Vector2 directionNormal = directionVector / distance;
                            
                        body02.force += (directionNormal) * GravitationalConst * body02.mass * body01.mass / (distance);
                    }
                }
            }
        }

        public void CollisionRespose()
        {
            foreach (var body01 in this.bodies)
            {
                foreach (Body body02 in bodies)
                {
                    if (this.CheckCollision(body01, body02))
                    {
                        // Calculating Penetration 
                        Vector2 distance = body01.pos - body02.pos;
                        float length = (float) Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                        Vector2 normal = distance / length;

                        float depth = (body01.radie + body02.radie) - length;
                        Vector2 pentrateResolve = normal * depth / 2;

                        // Executing Penetration
                        body01.pos += pentrateResolve;
                        body02.pos -= pentrateResolve;
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (var body in this.bodies)
            {
                body.Draw();
            }
        }

        private bool CheckCollision(Body body01, Body body02)
        {
            if (body01.pos != body02.pos)
            {
                if ((body01.pos.X - body02.pos.X) * (body01.pos.X - body02.pos.X) + (body01.pos.Y - body02.pos.Y) * (body01.pos.Y - body02.pos.Y) <= (body01.radie + body02.radie) * (body01.radie + body02.radie))
                {
                    return true;
                }
            }
            return false;
        }
    }
}