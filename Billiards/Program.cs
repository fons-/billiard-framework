using System;
using System.Collections.Generic;
using System.Drawing;
using BilliardFramework;
using OpenTK;
using OpenTK.Input;

namespace Billiards
{
	public partial class Billiards : GraphicsProgram
	{
		public List<Ball> balls = new List<Ball>();
		public float ballRestitution = .92f;
		public float wallRestitution = .9f;

		private int frameCount = 0;
		private float frameTimer = 0f;

		public override void InitGame()
		{
			RenderWindow.Instance.ClientSize = new Size(1016, 508);
			//RenderWindow.Instance.WindowBorder = WindowBorder.Fixed;

			/*balls.Add(new Ball(new Vector2(100f, 150f), new Vector2(140f, 0f), 0));
			balls.Add(new Ball(new Vector2(200f, 169f), 8));*/

			for(int i = 0; i < 1000; i++)
			{
				balls.Add(new Ball(i % 8 + 1));
			}
			RandomizeBalls(100f, 1, 8);
		}

		public override void Update(float timeSinceLastUpdate)
		{
			MouseState state = Mouse.GetCursorState();
			Point screenPoint = RenderWindow.Instance.PointToScreen(new Point(-state.X, -state.Y));
			//Ball.blackHolePosition = new Vector2(-screenPoint.X, -screenPoint.Y);

			TestBallCollisions();
			TestWallCollisions();

			foreach(var ball in balls)
			{
				ball.Update(timeSinceLastUpdate);
			}


			Clock(timeSinceLastUpdate);
		}

		public void RandomizeBalls(float maxVelocity, int minRadius, int maxRadius)
		{
			Random rnd = new Random();
			foreach(Ball ball in balls)
			{
				ball.position.X = rnd.Next(RenderWindow.Instance.Width);
				ball.position.Y = rnd.Next(RenderWindow.Instance.Height);
				ball.velocity = new Vector2((float)(rnd.NextDouble() - .5), (float)(rnd.NextDouble() - .5)).Normalized() * maxVelocity;
				ball.Radius = rnd.Next(minRadius, maxRadius);
			}
		}

		public void TestWallCollisions()
		{
			foreach(Ball ball in balls)
			{
				if(ball.position.X < ball.Radius && ball.velocity.X < 0f)
				{
					ball.velocity.X = -ball.velocity.X;
					ball.velocity *= wallRestitution;
					ball.position.X = ball.Radius;
					ball.touching = true;
				}
				if(ball.position.X > RenderWindow.Instance.Width - ball.Radius && ball.velocity.X > 0f)
				{
					ball.velocity.X = -ball.velocity.X;
					ball.velocity *= wallRestitution;
					ball.position.X = RenderWindow.Instance.Width - ball.Radius;
					ball.touching = true;
				}
				if(ball.position.Y < ball.Radius && ball.velocity.Y < 0f)
				{
					ball.velocity.Y = -ball.velocity.Y;
					ball.velocity *= wallRestitution;
					ball.position.Y = ball.Radius;
					ball.touching = true;
				}
				if(ball.position.Y > RenderWindow.Instance.Height - ball.Radius && ball.velocity.Y > 0f)
				{
					ball.velocity.Y = -ball.velocity.Y;
					ball.velocity *= wallRestitution;
					ball.position.Y = RenderWindow.Instance.Height - ball.Radius;
					ball.touching = true;
				}
			}
		}

		public void TestBallCollisions()
		{
			for(int i = 0; i < balls.Count; i++)
			{
				for(int j = i + 1; j < balls.Count; j++)
				{
					Ball a = balls[i];
					Ball b = balls[j];
					if((b.position - a.position).LengthSquared <= (a.Radius + b.Radius)*(a.Radius + b.Radius))
					{
						//Console.WriteLine("Collision between {0} and {1}: {2} intersection", a.n, b.n, 2f * ballRadius - (a.position - b.position).Length);
						Vector2 collisionDir = (b.position - a.position).Normalized();
						if(Vector2.Dot(a.velocity - b.velocity, collisionDir) > 0f)
						{
							Vector2 collisionVelA = Vector2.Dot(a.velocity, collisionDir) * collisionDir;
							collisionDir *= -1f;
							Vector2 collisionVelB = Vector2.Dot(b.velocity, collisionDir) * collisionDir;
							
							a.velocity = a.velocity - collisionVelA + collisionVelB * ballRestitution;
							b.velocity = b.velocity - collisionVelB + collisionVelA * ballRestitution;
						}
						collisionDir /= 2f;
						a.position -= collisionDir;
						b.position += collisionDir;
						a.touching = true;
						b.touching = true;
					}
				}
			}
		}

		private void Clock(float timeSinceLastUpdate)
		{
			frameCount++;
			frameTimer += timeSinceLastUpdate;
			if(frameTimer >= 1f)
			{
				Console.WriteLine(frameCount);
				frameCount = 0;
				frameTimer = 0f;
			}
		}
	}
}