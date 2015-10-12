using System;
using BilliardFramework;
using OpenTK;

namespace Billiards
{
	public class Ball
	{
		public bool touching = false;
		public float friction = .5f;
		public static float gravity = 500f;
		public float Radius = 10f;
		public Vector2 position;
		public Vector2 velocity;
		public int n;

		public Ball(Vector2 position, Vector2 velocity, int n)
		{
			this.position = position;
			this.velocity = velocity;
			this.n = n;
		}

		public Ball(Vector2 position, int n) : this(position, Vector2.Zero, n) { }

		public Ball(Vector2 position) : this(position, 0) { }

		public Ball(int n) : this(Vector2.Zero, n) { }

		public void Update(float timeSinceLastUpdate)
		{
			if(!touching)
			{
				bool right = position.X >= RenderWindow.Instance.Width / 2;
				bool bottom = position.Y >= RenderWindow.Instance.Height / 2;

				if(!right && bottom)
				{
					velocity.X += gravity * timeSinceLastUpdate;
				}
				if(right && !bottom)
				{
					velocity.X -= gravity * timeSinceLastUpdate;
				}
				if(!right && !bottom)
				{
					velocity.Y += gravity * timeSinceLastUpdate;
				}
				if(right && bottom)
				{
					velocity.Y -= gravity * timeSinceLastUpdate;
				}
			}
			else
			{
				touching = false;
			}
			velocity *= (float)Math.Pow(friction, timeSinceLastUpdate);
			position += velocity * timeSinceLastUpdate;
		}
	}
}