using System;
using GXPEngine;

public class Ball : EasyDraw
{
	public int radius {
		get {
			return _radius;
		}
	}

	public Vec2 velocity;
	public Vec2 position;

	int _radius;
	float _speed;

	public Ball (int pRadius, Vec2 pPosition, float pSpeed=5) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		_radius = pRadius;
		position = pPosition;
		_speed = pSpeed;
        velocity = new Vec2(0, 10);
		UpdateScreenPosition ();
		SetOrigin (_radius, _radius);

		Draw (255, 255, 255);
	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (_radius, _radius, 2*_radius, 2*_radius);
	}

	void FollowMouse() {
		position.SetXY (Input.mouseX, Input.mouseY);
	}

	void UpdateScreenPosition() {
		x = position.x;
		y = position.y;
	}

	public void Step () {
		//FollowMouse ();
		position.Add (velocity);
		UpdateScreenPosition ();
	}
}
