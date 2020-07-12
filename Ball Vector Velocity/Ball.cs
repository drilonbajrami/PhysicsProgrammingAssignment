using System;
using GXPEngine;

public class Ball : EasyDraw
{
	public Vec2 position {
		get {
			return _position;
		}
	}
	public Vec2 velocity {
		get {
			return _velocity;
		}
		set {
			_velocity = value;
		}
	}

	int _radius;
	Vec2 _position;
	Vec2 _velocity;
	float _speed;
    float diff;

	public Ball (int pRadius, Vec2 pPosition, float pSpeed=5) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		_radius = pRadius;
		_position = pPosition;
		_speed = pSpeed;

		UpdateScreenPosition ();
		SetOrigin (_radius, _radius);

		Draw (150, 0, 255);
	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (_radius, _radius, 2*_radius, 2*_radius);
	}

	void KeyControls() {
		_velocity.x = 0;
		_velocity.y = 0;

		if (Input.GetKey (Key.RIGHT)) {
			_velocity.x += _speed;
		} else if (Input.GetKey (Key.LEFT)) {
			_velocity.x -= _speed;
		} 

		if (Input.GetKey (Key.UP)) {
			_velocity.y -= _speed;
		} else if (Input.GetKey (Key.DOWN)) {
			_velocity.y += _speed;
		} 
	}

    public void MoveTowardsMouse()
    {
        diff += 0.1f;
        Vec2 delta = new Vec2(Input.mouseX - _position.x, Input.mouseY - _position.y);
        if(delta.Length() < _radius)
        {
            Console.WriteLine("YOU LOSE");
        }
        delta.Normalize();
        _position += delta * diff;
    }

	void UpdateScreenPosition() {
		x = _position.x;
		y = _position.y;
	}

	public void Step () {
        MoveTowardsMouse();
		UpdateScreenPosition ();
	}
}
