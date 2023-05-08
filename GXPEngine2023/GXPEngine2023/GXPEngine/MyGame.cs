using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	public MyGame() : base(900, 500, false)     
	{
        Console.WriteLine("MyGame initialized");
	}

	
	void Update() {
		
	}


    static void Main()                          
	{
		new MyGame().Start();                   
	}

}