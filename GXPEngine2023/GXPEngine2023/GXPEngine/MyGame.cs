using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
//using System.Drawing.Drawing2D;

public class MyGame : Game {
	public MyGame() : base(900, 500, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		// Draw some things on a canvas:
		EasyDraw canvas = new EasyDraw(900, 500);
		canvas.Fill(50);
        canvas.Ellipse(width / 2, height / 2, 200, 200);
        canvas.Rect(450, 470, 1000, 30);


        // Add the canvas to the engine to display it:
        Console.WriteLine("MyGame initialized");
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		
	}


    //GXPEngine.EasyDraw(string filename, bool addCollider = true ); //maybemaybemaybe

    static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}

}