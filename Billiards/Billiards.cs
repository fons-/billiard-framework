using System;
using BilliardFramework;

namespace Billiards
{
	public partial class Billiards : GraphicsProgram
	{
		public override void Render()
		{
			foreach(Ball ball in balls)
			{
				DrawBall(ball.position, ball.n);
			}
		}

		public Billiards(string[] arguments) : base(arguments) { }

		[STAThread]
		static void Main(string[] args)
		{
			using(Billiards billiards = new Billiards(args))
			{
				billiards.Run();
			}
		}
	}
}