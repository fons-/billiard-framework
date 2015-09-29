using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace BilliardFramework
{
	public class GraphicsProgram : IDisposable
	{
		public float ballRadius = 10f;

		public GraphicsProgram(string[] arguments)
		{
			RenderWindow.Instance.program = this;
			Debug.WriteLine("---------------");
			Console.WriteLine("Billiard Framework");
			Console.WriteLine("v" + Assembly.GetExecutingAssembly().GetName().Version);
			Console.WriteLine("---------------");
			Console.WriteLine("Created by:");
			Console.WriteLine("Fons van der Plas (fons-), fonsvdplas@gmail.com");
			Console.WriteLine("---------------");
			Console.WriteLine("Program launched at " + DateTime.Now);
		}
		public void Run()
		{
			RenderWindow.Instance.Run();
		}

		public virtual void Dispose()
		{
			RenderWindow.Instance.Dispose();
			//throw new NotImplementedException(); //TODO
		}

		public virtual void LoadResources()
		{

		}

		public virtual void InitGame()
		{

		}

		public virtual void Update(float timeSinceLastUpdate)
		{

		}

		public virtual void Resize(Rectangle newDimensions)
		{

		}

		public virtual void Render()
		{

		}

		public void DrawImage(Vector2 position, float width, float height, string textureName)
		{
			GL.BindTexture(TextureTarget.Texture2D, TextureManager.GetTexture(textureName));
			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0, 1); GL.Vertex2(position);
			GL.TexCoord2(0, 0); GL.Vertex2(position + new Vector2(0, height));
			GL.TexCoord2(1, 0); GL.Vertex2(position + new Vector2(width, height));
			GL.TexCoord2(1, 1); GL.Vertex2(position + new Vector2(width, 0));
			GL.End();
		}

		public void DrawBall(Vector2 position, int n)
		{
			GL.Color4(BallColor(n));
			DrawImage(position - .5f * new Vector2(ballRadius, ballRadius), ballRadius, ballRadius, "ball");
			GL.Color4(Color4.White);
		}

		private Color4 BallColor(int n)
		{
			Color4[] colors = new Color4[]
			{
				Color4.White,
				Color4.Yellow,
				Color4.Blue, 
				Color4.Red, 
				Color4.Purple, 
				Color4.Orange, 
				Color4.Green, 
				Color4.Brown, 
				Color4.Black
			};
			return colors[n];
		}
	}
}
