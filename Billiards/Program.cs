using System;
using System.Collections.Generic;
using System.Drawing;
using BilliardFramework;
using OpenTK;

namespace Billiards
{
	public partial class Billiards : GraphicsProgram
	{
		public List<Ball> balls = new List<Ball>();

		public override void InitGame()
		{
			// Hier begint het programma: alle code in deze functie wordt 1 keer opgeroepen als het programma start.

			// Handige dingen:
			RenderWindow.Instance.ClientSize = new Size(500, 300); // Verandert de grootte van het spel
			RenderWindow.Instance.WindowBorder = WindowBorder.Fixed; // Zorgt ervoor dat het spel niet van grootte veranderd kan worden
			Console.Write("Hello "); // Stuurt tekst naar de Console
			Console.WriteLine("world!"); // " en begint een nieuwe regel

			// Voorbeeld: een witte en een zwarte bal worden aangemaakt.
			// De volgorde is belangrijk: De witte bal is nu balls[0] en de zwarte bal is balls[1].
			balls.Add(new Ball(new Vector2(100f, 150f), 0));
			balls.Add(new Ball(new Vector2(200f, 150f), 8));

			// Syntax: balls.Add(new Ball( *positievector*, *balnummer* ));
			// Syntax: *positievector* = new Vector2( *x-coord*, *y-coord* ); (x,y zijn kommagetallen)
			// Het balnummer is het getal dat op een biljartbal staat, 0 is de witte bal.
		}

		public override void Update(float timeSinceLastUpdate)
		{
			// Deze code wordt bij elke update opgeroepen. Op een snelle computer gebeurt dit elke 1/1000 seconde, op
			// een langzame computer elke 1/60 seconde. Deze tijd is opgenomen met timeSinceLastUpdate.

			// Voorbeeld 1: alle ballen bewegen met 10 px/s naar rechts.
			foreach(var ball in balls)
			{
				ball.position.X += 10f * timeSinceLastUpdate; // ds = v * dt
			}

			// Voorbeeld 2: De witte bal gaat omlaag met 3.14 px/s en de zwarte omhoog met 3.14 px/s.
			balls[0].position.Y -= 3.14f * timeSinceLastUpdate;
			balls[1].position.Y += 3.14f * timeSinceLastUpdate;

			// Merk op: 1) Hogere y-waarde betekent naar beneden, hogere x-waarde betekent naar rechts (het nulpunt ligt linksboven)
			//          2) Kommagetallen zoals 3,14 worden geschreven als 3.14f . Op balnummers na zijn bijna alle getallen kommagetallen.
		}
	}
}
