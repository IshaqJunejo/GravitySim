using System.Numerics;
using Raylib_cs;

namespace GravitySim
{
    public class Body
    {
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 force;
        public float mass;
        public float radie;
        private Color shade;

        public Body(float posX, float posY, float velX, float velY, float density, float radius, Color color)
        {
            this.pos = new Vector2(posX, posY);
            this.vel = new Vector2(velX, velY);

            this.force = new Vector2(0, 0);

            //this.mass = density * radius * radius / 100;
            this.mass = density;
            this.radie = radius;
            
            this.shade = color;
        }

        public void Update()
        {
            this.vel += this.force / this.mass;
            this.pos += this.vel;
            this.vel /= 1.0f;
            this.force = new Vector2(0, 0);
        }

        public void Draw()
        {
            Raylib.DrawCircle((int)pos.X, (int)pos.Y, this.radie, this.shade);
        }

        public static bool CheckCollision(Body body1, Body body2)
        {
            if (body1.pos != body2.pos)
            {
                if ((body1.pos.X - body2.pos.X) * (body1.pos.X - body2.pos.X) + (body1.pos.Y - body2.pos.Y) * (body1.pos.Y - body2.pos.Y) <= (body1.radie + body2.radie) * (body1.radie + body2.radie))
                {
                    return true;
                }
            }
            return false;
        }
    }
}