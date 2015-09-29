using OpenTK;

namespace Billiards
{
	public class Ball
	{
		public Vector2 position;
		public int n;

		public Ball(Vector2 position, int n)
		{
			this.position = position;
			this.n = n;
		}

		public Ball(Vector2 position) : this(position, 0) { }
	}
}