using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace BilliardFramework
{
	public class RenderWindow : GameWindow
	{
		#region Singleton

		public GraphicsProgram program;

		private static RenderWindow instance;

		public static RenderWindow Instance => instance ?? (instance = new RenderWindow());

		#endregion

		public bool escapeOnEscape = true;

		public RenderWindow(string windowName, int width, int height) : base(width, height, GraphicsMode.Default, windowName
#if FULLSCREEN
			, GameWindowFlags.Fullscreen
#endif
			)
		{
			VSync = VSyncMode.Off;
		}

		public RenderWindow(string windowName)
			: this(windowName, 800, 600)
		{ }

		public RenderWindow()
			: this("Default render window")
		{ }

		protected override void OnLoad(EventArgs e)
		{
			Console.WriteLine("Initializing OpenGL...");

			WindowBorder = WindowBorder.Resizable;
			GL.ClearColor(Color.Black);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			Console.WriteLine("Initializing textures...");

			TextureManager.AddTexture("default", @"Content/textures/defaultTexture.png", TextureMinFilter.Linear, TextureMagFilter.Nearest);
			TextureManager.AddTexture("ball", @"Content/textures/ball2.png", TextureMinFilter.Linear, TextureMagFilter.Nearest);
			TextureManager.AddTexture("background", @"Content/textures/background.png", TextureMinFilter.Linear, TextureMagFilter.Linear);

			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.CullFace);
			GL.Disable(EnableCap.DepthTest);
			GL.Enable(EnableCap.Texture2D);
			GL.DepthMask(false);

			program.LoadResources();
			program.InitGame();
		}

		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(ClientRectangle);


			program.Resize(ClientRectangle);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			GL.ClearColor(Color4.DarkGreen);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit | ClearBufferMask.DepthBufferBit);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0.0, ClientRectangle.Width, ClientRectangle.Height, 0.0, -1.0, 1.0);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();

			GL.BindTexture(TextureTarget.Texture2D, TextureManager.GetTexture("background"));
			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0f, Height / 256f); GL.Vertex2(Vector2.Zero);
			GL.TexCoord2(0f, 0f); GL.Vertex2(new Vector2(0, Height));
			GL.TexCoord2(Width / 256f, 0f); GL.Vertex2(new Vector2(Width, Height));
			GL.TexCoord2(Width / 256f, Height / 256f); GL.Vertex2(new Vector2(Width, 0));
			GL.End();

			program.Render();

			SwapBuffers();
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			program.Update((float)e.Time);
		}
	}
}